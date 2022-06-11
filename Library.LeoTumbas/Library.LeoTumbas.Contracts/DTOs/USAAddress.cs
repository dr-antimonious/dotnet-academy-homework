using System.ComponentModel.DataAnnotations;

namespace Library.LeoTumbas.Contracts.DTOs
{
    public class UsaAddress : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            AddressValidator? address = value as AddressValidator;
            return address is not null
                   && (address.Country.Equals("USA", StringComparison.OrdinalIgnoreCase)
                       || address.Country.Equals("United States of America", StringComparison.OrdinalIgnoreCase));
        }
    }
}
