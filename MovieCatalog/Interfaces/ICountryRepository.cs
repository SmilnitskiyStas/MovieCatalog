using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int countryId);
        ICollection<Movie> GetMovieByCountry(int countryId);
        bool CountryExist(int countryId);
        bool CreateCounty(Country country);
        bool UpdateCounty(Country country);
        bool DeleteCounty(Country country);
        bool Save();
    }
}
