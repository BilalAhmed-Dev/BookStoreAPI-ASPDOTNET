using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.User;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<NewBookDto, Book>().ReverseMap();
            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}