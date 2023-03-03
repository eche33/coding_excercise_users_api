namespace Users.API.Exceptions
{
    public class UserIsNotOldEnoughException : Exception
    {
        public UserIsNotOldEnoughException() { }

        public UserIsNotOldEnoughException(string message)
            : base(message) { }

        public UserIsNotOldEnoughException(string message, Exception inner)
            : base(message, inner) { }
    }
}
