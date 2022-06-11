namespace Library.LeoTumbas.Contracts.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Password { get; set; } = default!;

        public InvalidPasswordException(string password) : base()
        {
            Password = password;
        }
    }
}
