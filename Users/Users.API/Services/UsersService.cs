using Users.API.Exceptions;
using Users.API.Model;

namespace Users.API.Services
{
    public class UsersService : IUsersService
    {
        private static List<User> _users = new List<User>();
        public void AddUser(User user)
        {
            if (_users.Exists(x => x.Email.Equals(user.Email)))
                throw new UserAlreadyExistsException($"User with email {user.Email} already exists");

            _users.Add(user);
        }

        public void DeleteUser(string email)
        {
            var userToDelete = FetchUser(email);

            if (userToDelete != null)
                _users.Remove(userToDelete);    
        }

        public void UpdateUser(User user)
        {
            var userToUpdate = FetchUser(user.Email);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.DateOfBirth = user.DateOfBirth;
            }
        }

        public IEnumerable<User> FetchAllUsers()
        {
            return _users;
        }

        public User FetchUser(string email)
        {
            var user = _users.FirstOrDefault(x => x.Email.Equals(email));

            if (user == null)
                throw new UserNotFoundException($"User with email {email} not found");
            else
                return user;
        }

        
    }
}
