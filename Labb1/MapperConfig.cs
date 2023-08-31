using AutoMapper;
using Labb1.Models;
using Labb1.Models.DTOs;

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
