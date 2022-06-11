using System.ComponentModel.DataAnnotations;

namespace Library.LeoTumbas.Contracts.DTOs
{
    public class PersonValidator
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = default!;
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;
    }
}
