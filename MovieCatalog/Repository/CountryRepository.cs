using AutoMapper;
using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CountryExist(int countryId)
        {
            return _context.Categories.Any(c => c.CategoryId == countryId);
        }

        public bool CreateCounty(Country country)
        {
            _context.Countries.Add(country);

            return Save();
        }

        public bool DeleteCounty(Country country)
        {
            _context.Countries.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c => c.CountryId).ToList();
        }

        public ICollection<Country> GetCountriesOfAMovie(int movieId)
        {
            return _context.MovieCountries.Where(m => m.MovieId == movieId).Select(c => c.Country).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _context.Countries.Where(c => c.CountryId == countryId).FirstOrDefault();
        }

        public ICollection<Movie> GetMovieByCountry(int countryId)
        {
            return _context.MovieCountries.Where(c => c.CountryId == countryId).Select(m => m.Movie).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateCounty(Country country)
        {
            _context.Countries.Update(country);
            return Save();
        }
    }
}
