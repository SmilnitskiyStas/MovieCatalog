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
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public ProducerController(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
        public IActionResult GetProducers()
        {
            var producers = _mapper.Map<List<ProducerDto>>(_producerRepository.GetProducers());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(producers);
        }

        [HttpGet("{producerId:int}")]
        [ProducesResponseType(200, Type = typeof(Producer))]
        [ProducesResponseType(400)]
        public IActionResult GetProducer(int producerId)
        {
            if (!_producerRepository.GetProducerExists(producerId))
            {
                return NotFound();
            }

            var producer = _mapper.Map<ProducerDto>(_producerRepository.GetProducer(producerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(producer);
        }

        [HttpGet("{producerId:int}/Movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieOfAProducer(int producerId)
        {
            var movies = _mapper.Map<List<MovieDto>>(_producerRepository.GetMovieOfAProducer(producerId));

            if (movies == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(movies);
        }

        [HttpGet("{movieId:int}/Producers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducerByMovie(int movieId)
        {
            var producers = _mapper.Map<List<Producer>>(_producerRepository.GetProducerByMovie(movieId));

            if (producers == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(producers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateImage([FromBody] ProducerDto producerCreate)
        {
            if (producerCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producerMap = _mapper.Map<Producer>(producerCreate);

            if (!_producerRepository.CreateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{producerId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProducer(int producerId, [FromBody] ProducerDto producerUpdate)
        {
            if (producerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (producerId != producerUpdate.ProducerId)
            {
                return BadRequest(ModelState);
            }

            if (!_producerRepository.GetProducerExists(producerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var producerMap = _mapper.Map<Producer>(producerUpdate);

            if (!_producerRepository.UpdateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating producer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{producerId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProducer(int producerId)
        {
            if (producerId == 0)
            {
                return BadRequest();
            }

            if (!_producerRepository.GetProducerExists(producerId))
            {
                return NotFound();
            }

            var producerMap = _mapper.Map<Producer>(_producerRepository.GetProducer(producerId));

            if (!_producerRepository.DeleteProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong deleting producer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
