using IdentityModel;
using IdentityServer4.Validation;
using User.Application.Abstractions.Repositories;
using User.Persistance.IdentityConfiguration;

namespace HomePages.Helpers
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var isValid = await _userRepository.ValidateAsync(context.UserName, context.Password);
            if (isValid)
            {
                var user = await _userRepository.GetByUserNameAsync(context.UserName);

                context.Result = new GrantValidationResult(user!.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
