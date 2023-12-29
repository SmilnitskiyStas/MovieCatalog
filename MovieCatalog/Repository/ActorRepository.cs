﻿using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _context;
        public ActorRepository(DataContext context)
        {
            _context = context;
        }
        public Actor GetActor(int actorId)
        {
            return _context.Actors.OrderBy(a => a.ActorId == actorId).FirstOrDefault();
        }

        public Actor GetActor(string actorName)
        {
            return _context.Actors.OrderBy(a => $"{a.FirstName} {a.LastName}" == actorName).FirstOrDefault();
        }

        public bool GetActorExist(int actorId)
        {
            return _context.Actors.Any(a => a.ActorId == actorId);
        }

        public ICollection<Actor> GetActors()
        {
            return _context.Actors.OrderBy(a => a.ActorId).ToList();
        }

        public ICollection<Actor> GetActorsOfAMovie(int movieId)
        {
            return _context.MovieActors.Where(m => m.MovieId == movieId).Select(a => a.Actor).ToList();
        }

        public ICollection<Movie> GetMovieByActor(int actorId)
        {
            return _context.MovieActors.Where(a => a.ActorId == actorId).Select(m => m.Movie).ToList();
        }
    }
}
