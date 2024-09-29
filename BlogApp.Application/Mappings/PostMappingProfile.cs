using AutoMapper;
using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Mappings;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? src.Author.Username : string.Empty)); // Verifica se o Author não é nulo

        CreateMap<PostRequest, Post>();
    }
}
