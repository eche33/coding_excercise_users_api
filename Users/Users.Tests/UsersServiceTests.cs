﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.API.Exceptions;
using Users.API.Model;
using Users.API.Services;

namespace Users.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void FetchAllUsers_NoUsersInMemory_EmptyCollectionReturned()
        {
            var usersService = new UsersService();

            Assert.Empty(usersService.FetchAllUsers());
        }

        [Fact]
        public void AddUser_ValidUserPassed_UserIsAddedSuccessfullyAsync()
        {
            var user = new User("Rodrigo", "Etchegaray", DateTime.Parse("1995-02-08"), "test@test.com");
            var usersService = new UsersService();

            usersService.AddUser(user);

            var allUsers = usersService.FetchAllUsers();

            Assert.Single(allUsers);

            usersService.DeleteUser("test@test.com");
        }

        [Fact]
        public void AddUser_RepeatedUser_UserAlreadyExistsExceptionThrown()
        {
            var user = new User("Rodrigo", "Etchegaray", DateTime.Parse("1995-02-08"), "test@test.com");
            var userWithSameEmail = new User("Juan", "Perez", DateTime.Parse("1995-02-08"), "test@test.com");
            var usersService = new UsersService();

            usersService.AddUser(user);

            Assert.Throws<UserAlreadyExistsException>(() => usersService.AddUser(userWithSameEmail));

            usersService.DeleteUser("test@test.com");
        }

        [Fact]
        public void FetchUser_UserExists_UserIsReturned()
        {
            var user = new User("Rodrigo", "Etchegaray", DateTime.Parse("1995-02-08"), "test@test.com");
            var usersService = new UsersService();

            usersService.AddUser(user);

            Assert.Equal(user, usersService.FetchUser("test@test.com"));

            usersService.DeleteUser("test@test.com");
        }

        [Fact]
        public void FetchUser_UserDoesntExist_UserNotFoundExceptionThrown()
        {
            var usersService = new UsersService();

            Assert.Throws<UserNotFoundException>( () => usersService.FetchUser("test@test.com"));
        }

    }
}
