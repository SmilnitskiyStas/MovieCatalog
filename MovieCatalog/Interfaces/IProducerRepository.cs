using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IProducerRepository
    {
        ICollection<Producer> GetProducers();
        Producer GetProducer(int producerId);
        Producer GetProducer(string name);
        ICollection<Movie> GetMovieOfAProducer(int producerId);
        ICollection<Producer> GetProducerByMovie(int movieId);
        bool GetProducerExists(int producerId);
        bool CreateProducer(Producer producer);
        bool Save();
    }
}
