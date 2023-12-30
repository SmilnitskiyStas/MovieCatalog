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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateActor([FromBody]ActorDto actorCreate)
        {
            if (actorCreate == null)
            {
                return BadRequest(ModelState);
            }

            var actor = _actorRepository.GetActors()
                .Where(a => $"{a.FirstName} {a.LastName}".Trim().ToUpper() == $"{actorCreate.FirstName} {actorCreate.LastName}".Trim().ToUpper())
                .FirstOrDefault();

            if (actor != null)
            {
                ModelState.AddModelError("", "Actor already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actorMap = _mapper.Map<Actor>(actorCreate);

            if (!_actorRepository.CreateActor(actorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }
    }
}
