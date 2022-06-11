using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Exceptions;
using Library.LeoTumbas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.LeoTumbas.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("host/api/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequest)
        {
            ModelState.ClearValidationState(nameof(registrationRequest));
            if (TryValidateModel(registrationRequest, nameof(registrationRequest)))
            {
                try
                {
                    await _registrationService.Register(registrationRequest);
                }
                catch (UserAlreadyExistsException exception)
                {
                    return Conflict(exception.Email);
                }
                catch (GenericUserRegistrationException exception)
                {
                    return Problem(exception.Message, null, 500, "User registration problem", null);
                }

                return Ok();
            }

            return ValidationProblem();
        }
    }
}
