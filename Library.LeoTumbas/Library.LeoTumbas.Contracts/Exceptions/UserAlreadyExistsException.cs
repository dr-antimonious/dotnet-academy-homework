namespace Library.LeoTumbas.Contracts.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string Email { get; set; }
        public UserAlreadyExistsException(string email) : base()
        {
            Email = email;
        }
    }
}
