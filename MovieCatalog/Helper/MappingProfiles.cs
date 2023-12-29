using AutoMapper;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;

namespace MovieCatalog.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<Actor, ActorDto>();
        }
    }
}
