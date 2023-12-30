using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
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
    }
}
