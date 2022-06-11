using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Exceptions;
using Library.LeoTumbas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.LeoTumbas.API.Controllers
{
    [ApiController]
    [Route("host/api/login")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IExtendedAuthenticationService _authenticationService;

        public AuthenticationController(IExtendedAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            ModelState.ClearValidationState(nameof(loginRequest));
            if (TryValidateModel(loginRequest, nameof(loginRequest)))
            {
                TokenDto token;

                try
                {
                    token = await _authenticationService.Login(loginRequest);
                }
                catch (UserDoesNotExistException exception)
                {
                    return NotFound(exception.Email);
                }
                catch (InvalidPasswordException exception)
                {
                    return Unauthorized(exception.Password);
                }

                return Ok(token);
            }

            return ValidationProblem();
        }
    }
}
