using Users.API.Exceptions;
using Users.API.Model;

namespace Users.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_NameLastNameDateOfBirthEmail_UserIsCreatedSuccessfully()
        {
            var user = new User("Rodrigo", "Etchegaray", DateTime.Parse("1995-02-08"), "test@test.com");

            Assert.Equal("Rodrigo", user.FirstName);
            Assert.Equal("Etchegaray", user.LastName);
            Assert.Equal(DateTime.Parse("1995-02-08"), user.DateOfBirth);
            Assert.Equal("test@test.com", user.Email);
        }
    }
}