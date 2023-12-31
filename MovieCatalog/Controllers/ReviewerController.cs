using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Interfaces;
using MovieCatalog.Models.Dto;
using MovieCatalog.Models;
using MovieCatalog.Repository;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewes()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId:int}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetActor(int reviewerId)
        {
            if (!_reviewerRepository.GetReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId:int}/Review")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAReviewer(int reviewerId)
        {
            if (!_reviewerRepository.GetReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewOfAReviewer(reviewerId));

            if (reviews == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => $"{r.FirstName} {r.LastName}".Trim().ToUpper() == $"{reviewerCreate.FirstName} {reviewerCreate.LastName}".Trim().ToUpper())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{reviewerId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto reviewerUpdate)
        {
            if (reviewerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (reviewerId != reviewerUpdate.ReviewerId)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.GetReviewerExists(reviewerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewerUpdate);

            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewerId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (reviewerId == 0)
            {
                return BadRequest();
            }

            if (!_reviewerRepository.GetReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewerMap = _mapper.Map<Reviewer>(_reviewerRepository.GetReviewer(reviewerId));

            if (!_reviewerRepository.DeleteReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong deleting producer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
