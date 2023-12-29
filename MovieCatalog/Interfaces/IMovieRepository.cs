using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovie(int id);
        Movie GetMovie(string name);
        bool MovieExist(int movieId);
    }
}
