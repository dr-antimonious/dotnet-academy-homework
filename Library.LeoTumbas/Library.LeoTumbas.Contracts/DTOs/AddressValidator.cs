using System.ComponentModel.DataAnnotations;

namespace Library.LeoTumbas.Contracts.DTOs
{
    public class AddressValidator
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;
    }
}
