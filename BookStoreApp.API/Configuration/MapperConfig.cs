using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<NewBookDto, Book>().ReverseMap();
        }
    }
}