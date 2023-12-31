using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        Reviewer GetReviewer(string name);
        ICollection<Review> GetReviewOfAReviewer(int reviewerId);
        bool GetReviewerExists(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
