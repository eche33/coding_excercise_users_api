using System.ComponentModel.DataAnnotations;

namespace Users.API.Model.DTOs
{
    public class UserForCreationDTO
    {
        [Required(ErrorMessage = "firstName required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "lastName required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "dateOfBirth required")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "email required")]
        public string Email { get; set; }

        [Range(18, 120, ErrorMessage = "User must be 18 years or older")]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-age)) age--;

                return age;
            }
        }
    }
}
