using System.ComponentModel.DataAnnotations;

namespace Library.LeoTumbas.Contracts.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
    }
}
