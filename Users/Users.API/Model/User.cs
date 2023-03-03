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

        private const int MINIMUM_AGE = 18;

        public User(string firstName, string lastName, DateTime dateofBirth, string email)
        {
            ValidateEmail(email);
            ValidateAge(dateofBirth);
            FirstName = firstName;
            LastName = lastName;
        }

        private void ValidateAge(DateTime dateofBirth)
        {
            if (CalculateAge(dateofBirth) < MINIMUM_AGE)
                throw new UserIsNotOldEnoughException($"User must be {MINIMUM_AGE} years old or older");
            else
                DateOfBirth = dateofBirth;
        }

        private int CalculateAge(DateTime dateofBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateofBirth.Year;

            if (dateofBirth.Date > today.AddYears(-age)) age--;

            return age;
        }

        private void ValidateEmail(string email)
        {
            if (IsValidEmail(email))
                Email = email;
            else
                throw new InvalidEmailException();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}