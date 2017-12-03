using AutoMapper;
using ExampleWebApi.Models;
using ExampleWebApi.Views;

namespace ExampleWebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserView>();
            CreateMap<UserGroup, UserGroupView>();
        }
    }
}