using Microsoft.AspNetCore.Http.HttpResults;
using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly DataContext _context;

        public ProducerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            return Save();
        }

        public ICollection<Movie> GetMovieOfAProducer(int producerId)
        {
            return _context.MovieProducers.Where(p => p.ProducerId == producerId).Select(m => m.Movie).ToList();
        }

        public Producer GetProducer(int producerId)
        {
            return _context.Producers.Where(p => p.ProducerId == producerId).FirstOrDefault();
        }

        public Producer GetProducer(string name)
        {
            return _context.Producers.Where(p => $"{p.FirstName} {p.LastName}".Trim().ToUpper() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public ICollection<Producer> GetProducerByMovie(int movieId)
        {
            return _context.MovieProducers.Where(m => m.MovieId == movieId).Select(p => p.Producer).ToList();
        }

        public bool GetProducerExists(int producerId)
        {
            return _context.Producers.Any(p => p.ProducerId == producerId);
        }

        public ICollection<Producer> GetProducers()
        {
            return _context.Producers.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
