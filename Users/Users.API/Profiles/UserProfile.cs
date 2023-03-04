using AutoMapper;
using System.Reflection;
using Users.API.Model;
using Users.API.Model.DTOs;

namespace Users.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        { 
            CreateMap<User, UserDTO>();
            CreateMap<UserForCreationDTO, User>();
        }
    }
}
