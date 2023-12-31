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

        public bool CreateMovie(int actorId, int categoryId, int countryId, int producerId, Movie movieCreate)
        {
            var movieActorEntity = _context.Actors.Where(a => a.ActorId == actorId).FirstOrDefault();
            var movieCategoryEntity = _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            var movieCountryEntity = _context.Countries.Where(c => c.CountryId == countryId).FirstOrDefault();
            var movieProducerEntity = _context.Producers.Where(p => p.ProducerId == producerId).FirstOrDefault();

            var movieActor = new MovieActor
            {
                Actor = movieActorEntity,
                Movie = movieCreate
            };

            _context.MovieActors.Add(movieActor);

            var movieCategory = new MovieCategory
            {
                Category = movieCategoryEntity,
                Movie = movieCreate
            };

            _context.MovieCategories.Add(movieCategory);

            var movieCountry = new MovieCountry
            {
                Country = movieCountryEntity,
                Movie = movieCreate
            };

            _context.MovieCountries.Add(movieCountry);

            var movieProducer = new MovieProducer
            {
                Producer = movieProducerEntity,
                Movie = movieCreate
            };

            _context.MovieProducers.Add(movieProducer);

            _context.Movies.Add(movieCreate);

            return Save();
        }

        public bool DeleteMovie(Movie movieDelete)
        {
            _context.Movies.Remove(movieDelete);
            return Save();
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

        public bool MovieExists(int movieId)
        {
            return _context.Movies.Any(m => m.MovieId == movieId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMovie(int actorId, int categoryId, int countryId, int producerId, Movie movieUpdate)
        {
            var movieActorEntity = _context.Actors.Where(a => a.ActorId == actorId).FirstOrDefault();
            var movieCategoryEntity = _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            var movieCountryEntity = _context.Countries.Where(c => c.CountryId == countryId).FirstOrDefault();
            var movieProducerEntity = _context.Producers.Where(p => p.ProducerId == producerId).FirstOrDefault();

            var movieActor = new MovieActor
            {
                Actor = movieActorEntity,
                Movie = movieUpdate
            };

            _context.MovieActors.Update(movieActor);

            var movieCategory = new MovieCategory
            {
                Category = movieCategoryEntity,
                Movie = movieUpdate
            };

            _context.MovieCategories.Update(movieCategory);

            var movieCountry = new MovieCountry
            {
                Country = movieCountryEntity,
                Movie = movieUpdate
            };

            _context.MovieCountries.Update(movieCountry);

            var movieProducer = new MovieProducer
            {
                Producer = movieProducerEntity,
                Movie = movieUpdate
            };

            _context.MovieProducers.Update(movieProducer);

            _context.Movies.Update(movieUpdate);

            return Save();
        }
    }
}
