using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovie(int movieId);
        Movie GetMovie(string name);
        bool MovieExists(int movieId);
        bool CreateMovie(int actorId, int categoryId, int countryId, int producerId, Movie movieCreate);
        bool UpdateMovie(int actorId, int categoryId, int countryId, int producerId, Movie movieUpdate);
        bool DeleteMovie(Movie movieDelete);
        bool Save();
    }
}
