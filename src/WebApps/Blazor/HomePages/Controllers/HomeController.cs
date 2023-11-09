using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;

namespace HomePages.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize]
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