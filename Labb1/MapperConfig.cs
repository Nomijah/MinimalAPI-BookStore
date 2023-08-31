using AutoMapper;
using Labb1.DTOs;
using Labb1.Models;

namespace Labb1
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, Book>();
            CreateMap<Book, BookCreateDTO>().ReverseMap();
        }
    }
}
