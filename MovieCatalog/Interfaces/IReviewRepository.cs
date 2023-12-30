using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewOfAMovie(int movieId);
        bool GetReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();

    }
}
