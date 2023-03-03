using Users.API.Model;

namespace Users.API.Services
{
    public interface IUsersService
    {
        public User FetchUser(string email);
        public IEnumerable<User> FetchAllUsers();
        public void AddUser(User user);
        public void DeleteUser(string email);
    }
}
