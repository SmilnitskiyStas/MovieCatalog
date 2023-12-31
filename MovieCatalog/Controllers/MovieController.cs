using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;
using MovieCatalog.Repository;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper,
            IActorRepository actorRepository, ICountryRepository countryRepository,
            IProducerRepository producerRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _actorRepository = actorRepository;
            _countryRepository = countryRepository;
            _producerRepository = producerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(movies);
        }

        [HttpGet("{movieId:int}")]
        [ProducesResponseType(200, Type=typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(movieId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromQuery] int actorId, [FromQuery] int categoryId,
            [FromQuery] int countryId, [FromQuery] int producerId, [FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
            {
                return BadRequest(ModelState);
            }

            var movie = _movieRepository.GetMovies()
                .Where(m => m.Title.Trim().ToUpper() == movieCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }

            var movieMap = _mapper.Map<Movie>(movieCreate);

            if (!_movieRepository.CreateMovie(actorId, categoryId, countryId, producerId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{movieId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int movieId, [FromBody] MovieDto movieUpdate, [FromQuery] int actorId, [FromQuery] int categoryId,
            [FromQuery] int countryId, [FromQuery] int producerId)
        {
            if (movieUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (movieId != movieUpdate.MovieId)
            {
                return BadRequest(ModelState);
            }

            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieMap = _mapper.Map<Movie>(movieUpdate);

            if (!_movieRepository.UpdateMovie(actorId, categoryId, countryId, producerId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (movieId == 0)
            {
                return BadRequest();
            }

            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            var movieMap = _mapper.Map<Movie>(_movieRepository.GetMovie(movieId));

            if (!_movieRepository.DeleteMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong deleting movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
