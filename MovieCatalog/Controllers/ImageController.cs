using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;
using MovieCatalog.Repository;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public ImageController(IImageRepository imageRepository, IMapper mapper, IMovieRepository movieRepository)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Image>))]
        public IActionResult GetImages()
        {
            var images = _mapper.Map<List<ImageDto>>(_imageRepository.GetImages());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(images);
        }

        [HttpGet("{imageId:int}")]
        [ProducesResponseType(200, Type = typeof(Image))]
        [ProducesResponseType(400)]
        public IActionResult GetImage(int imageId)
        {
            if (!_imageRepository.GetImageExists(imageId))
            {
                return NotFound();
            }

            var image = _mapper.Map<ImageDto>(_imageRepository.GetImage(imageId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(image);
        }

        [HttpGet("{movieId:int}/Images")]
        [ProducesResponseType(200, Type = typeof(ICollection<Image>))]
        [ProducesResponseType(400)]
        public IActionResult GetImageOfAMovie(int movieId)
        {
            var images = _imageRepository.GetImageByMovie(movieId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(images);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateImage([FromQuery] int movieId, [FromBody] ImageDto imageCreate)
        {
            if (imageCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageMap = _mapper.Map<Image>(imageCreate);

            imageMap.Movie = _movieRepository.GetMovie(movieId);

            if (!_imageRepository.CreateImage(imageMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{imageId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateImage(int imageId, [FromBody] ImageDto imageUpdate)
        {
            if (imageUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (imageId != imageUpdate.ImageId)
            {
                return BadRequest(ModelState);
            }

            if (!_imageRepository.GetImageExists(imageId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var imageMap = _mapper.Map<Image>(imageUpdate);

            if (!_imageRepository.UpdateImage(imageMap))
            {
                ModelState.AddModelError("", "Something went wrong updating image");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{imageId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteImage(int imageId)
        {
            if (imageId == 0)
            {
                return BadRequest();
            }

            if (!_imageRepository.GetImageExists(imageId))
            {
                return NotFound();
            }

            var imageMap = _mapper.Map<Image>(_imageRepository.GetImage(imageId));

            if (!_imageRepository.DeleteImage(imageMap))
            {
                ModelState.AddModelError("", "Something went wrong deleting image");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
