namespace Library.LeoTumbas.Contracts.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public string Email { get; set; } = default!;

        public UserDoesNotExistException(string email) : base()
        {
            Email = email;
        }
    }
}
