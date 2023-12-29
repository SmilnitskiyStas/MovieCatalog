using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public Movie GetMovie(int movieId)
        {
            return _context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
        }

        public Movie GetMovie(string name)
        {
            return _context.Movies.Where(m => m.Title == name).FirstOrDefault();
        }

        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.OrderBy(m => m.MovieId).ToList();
        }

        public bool MovieExist(int movieId)
        {
            return _context.Movies.Any(m => m.MovieId == movieId);
        }
    }
}
