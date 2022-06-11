using Library.LeoTumbas.Contracts.DTOs;

namespace Library.LeoTumbas.Services
{
    public interface IRegistrationService
    {
        Task Register(RegistrationRequestDto registrationRequest);
    }
}
