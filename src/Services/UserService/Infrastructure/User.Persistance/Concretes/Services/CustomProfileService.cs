using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions.Repositories;

namespace User.Persistance.Concretes.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public CustomProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Username),

                new Claim("fullname", $"{user.FirstName} {user.LastName}"),
                new Claim("industry",$"{user.Industry}"),
                new Claim("wage",$"{user.Wage}"),
                new Claim("position",$"{user.Position}"),
                new Claim("authority",$"{user.Authority}"),
                new Claim("workingat",$"{user.WorkingAt}"),
                new Claim("email",$"{user.Email}"),
                new Claim("role",$"{user.Role}"),
                new Claim("birthdate",$"{user.BirthDate}")
            };

            context.AddRequestedClaims(claims);
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            context.IsActive = user != null;
        }
    }
}
