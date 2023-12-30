using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.ReviewId == reviewId).FirstOrDefault();
        }

        public bool GetReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.ReviewId == reviewId);
        }

        public ICollection<Review> GetReviewOfAMovie(int movieId)
        {
            return _context.Movies.Where(m => m.MovieId == movieId).Select(r => r.Reviews).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
