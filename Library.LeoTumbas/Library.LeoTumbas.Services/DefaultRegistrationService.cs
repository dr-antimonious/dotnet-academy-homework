using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Library.LeoTumbas.Services
{
    public class DefaultRegistrationService : IRegistrationService
    {
        private readonly UserManager<Person> _userManager;

        public DefaultRegistrationService(UserManager<Person> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(RegistrationRequestDto registrationRequest)
        {
            Person? user = await _userManager.FindByEmailAsync(registrationRequest.EmailAddress);
            if (user is not null)
            {
                throw new UserAlreadyExistsException(user.Email);
            }

            user = new Person(
                registrationRequest.FirstName,
                registrationRequest.LastName,
                registrationRequest.EmailAddress,
                new Address(
                    registrationRequest.Address.Street,
                    registrationRequest.Address.City,
                    registrationRequest.Address.Country));

            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (!result.Succeeded)
            {
                string error = string.Join(",", result.Errors.SelectMany(error => error.Description));
                throw new GenericUserRegistrationException(error);
            }
        }
    }
}
