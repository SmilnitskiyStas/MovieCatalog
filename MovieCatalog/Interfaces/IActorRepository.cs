using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IActorRepository
    {
        ICollection<Actor> GetActors();
        Actor GetActor(int actorId);
        Actor GetActor(string actorName);
        bool GetActorExist(int actorId);
        ICollection<Actor> GetActorsOfAMovie(int movieId);
        ICollection<Movie> GetMovieByActor(int actorId);
        bool CreateActor(Actor actor);
        bool Save();
    }
}
