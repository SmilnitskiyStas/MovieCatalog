using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.ReviewerId == reviewerId).FirstOrDefault();
        }

        public Reviewer GetReviewer(string name)
        {
            return _context.Reviewers.Where(r => $"{r.FirstName} {r.LastName}".Trim().ToUpper() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public bool GetReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.ReviewerId == reviewerId);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewOfAReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.ReviewerId == reviewerId).Select(r => r.Reviews).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
