using System.ComponentModel.DataAnnotations;

namespace Library.LeoTumbas.Contracts.DTOs
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage = "Address is required")]
        [UsaAddress]
        public AddressValidator Address { get; set; } = default!;
    }
}
