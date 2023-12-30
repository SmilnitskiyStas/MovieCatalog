using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovie(int movieId);
        Movie GetMovie(string name);
        bool MovieExists(int movieId);
        bool CreateMovie(int actorId, int categoryId, int countryId, int ProducerId, Movie movieCreate);
        bool Save();
    }
}
