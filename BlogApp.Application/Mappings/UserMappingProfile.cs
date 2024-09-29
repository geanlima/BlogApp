using AutoMapper;
using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>()
                .ConstructUsing(u => new User(u.Username, u.Email, u.Password)).ReverseMap();

            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
