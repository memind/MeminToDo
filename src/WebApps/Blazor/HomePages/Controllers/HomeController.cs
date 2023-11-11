using HomePages.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace HomePages.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index() => View();


        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> TestPage()
        {
            Console.WriteLine("------------------------------------");

            var prop = (await HttpContext.AuthenticateAsync()).Properties.Items;
            foreach (var claim in User.Claims)
            {
                Console.WriteLine(claim);
            }

            Console.WriteLine("------------------------------------");

            foreach (var item in prop)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------------------------------------");
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AdminRolePage() => View();

        [Authorize(Roles = "Moderator")]
        public IActionResult ModRolePage() => View();

        [HttpGet]
        public IActionResult CustomLogin() => View();

        [HttpPost]
        public async Task<IActionResult> CustomLogin(UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();
                var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:8005");

                if (discoveryDocument.IsError) 
                    return View(model);

                var request = new PasswordTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    UserName = model.Username,
                    Password = model.Password,
                    ClientId = "MeminToDoHome",
                    ClientSecret = "memintodohome"
                };

                var response = await httpClient.RequestPasswordTokenAsync(request);

                if (response.IsError)  
                    return View(model); 

                var userInfoRequest = new UserInfoRequest
                {
                    Token = response.AccessToken,
                    Address = discoveryDocument.UserInfoEndpoint,
                };

                var userInfoResponse = await httpClient.GetUserInfoAsync(userInfoRequest);

                if (userInfoResponse.IsError) 
                    return View(model); 

                var identity = new ClaimsIdentity(userInfoResponse.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
                var principle = new ClaimsPrincipal(identity);

                var authenticationProperties = new AuthenticationProperties();

                authenticationProperties.StoreTokens(new List<AuthenticationToken>
                {
                    new() { Name = OpenIdConnectParameterNames.AccessToken, Value = response.AccessToken },
                    new() { Name = OpenIdConnectParameterNames.RefreshToken, Value = response.RefreshToken },
                    new() { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(response.ExpiresIn).ToString("o", CultureInfo.InvariantCulture) },
                });

                await HttpContext.SignInAsync("MeminToDoHomeCookie", principle, authenticationProperties);

                if (model.ReturnUrl is not null)
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("MeminToDoHomeCookie");
            await HttpContext.SignOutAsync("oidc");
        }

        public async Task<IActionResult> NewAccessTokenTest() 
        {
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync();
            IOrderedEnumerable<KeyValuePair<string, string>> properties = authenticateResult.Properties.Items.OrderBy(p => p.Key);
            return View(properties);
        }
        public async Task<IActionResult> NewAccessToken()
        {
            string refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            HttpClient httpClient = new HttpClient();

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest()
            {
                ClientId = "MeminToDoHome",
                ClientSecret = "memintodohome",
                RefreshToken = refreshToken,
                Address = (await httpClient.GetDiscoveryDocumentAsync("https://localhost:8005")).TokenEndpoint
            };

            TokenResponse tokenResponse = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
            AuthenticationProperties properties = (await HttpContext.AuthenticateAsync()).Properties;

            properties.StoreTokens(
                new List<AuthenticationToken> {
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.IdToken,
                        Value = tokenResponse.IdentityToken
                    },

                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.AccessToken,
                        Value = tokenResponse.AccessToken
                    },

                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.RefreshToken,
                        Value = tokenResponse.RefreshToken
                    },

                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.ExpiresIn,
                        Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("O")
                    }
                });

            await HttpContext.SignInAsync("MeminToDoHomeCookie", (await HttpContext.AuthenticateAsync()).Principal, properties);
            return RedirectToAction(nameof(HomeController.NewAccessTokenTest));
        }
    }
}