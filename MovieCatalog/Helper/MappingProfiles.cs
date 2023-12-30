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
            CreateMap<MovieDto, Movie>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Actor, ActorDto>();
            CreateMap<ActorDto, Actor>();
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();
            CreateMap<Producer, ProducerDto>();
            CreateMap<ProducerDto, Producer>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
