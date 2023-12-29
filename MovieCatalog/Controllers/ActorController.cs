using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorController(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public IActionResult GetActors()
        {
            var actors = _mapper.Map<List<ActorDto>>(_actorRepository.GetActors());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(actors);
        }

        [HttpGet("{actorId:int}")]
        [ProducesResponseType(200, Type = typeof(Actor))]
        [ProducesResponseType(400)]
        public IActionResult GetActor(int actorId)
        {
            if (!_actorRepository.GetActorExist(actorId))
            {
                return NotFound();
            }

            var actor = _mapper.Map<ActorDto>(_actorRepository.GetActor(actorId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(actor);
        }

        [HttpGet("{actorId:int}/movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByActor(int actorId)
        {
            if (!_actorRepository.GetActorExist(actorId))
            {
                return NotFound();
            }

            var movies = _mapper.Map<List<MovieDto>>(_actorRepository.GetMovieByActor(actorId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(movies);
        }

        [HttpGet("{movieId:int}/actors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        [ProducesResponseType(400)]
        public IActionResult GetActoryOfAMovie(int movieId)
        {
            if (!_actorRepository.GetActorExist(movieId))
            {
                return NotFound();
            }

            var actors = _mapper.Map<List<ActorDto>>(_actorRepository.GetActorsOfAMovie(movieId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(actors);
        }
    }
}
