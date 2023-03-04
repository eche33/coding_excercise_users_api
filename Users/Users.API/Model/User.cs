using System.Net.Mail;
using Users.API.Exceptions;

namespace Users.API.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public User(string firstName, string lastName, DateTime dateofBirth, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateofBirth;
            Email = email;
        }
    }
}