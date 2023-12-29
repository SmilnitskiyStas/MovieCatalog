using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;

namespace MovieCatalog.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CategoryExist(int categoryId)
        {
            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryId).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
        }

        public ICollection<Category> GetCategoryOfAMovie(int movieId)
        {
            return _context.MovieCategories.Where(m => m.MovieId == movieId).Select(c => c.Category).ToList();
        }

        public ICollection<Movie> GetMovieByCategory(int categoryId)
        {
            return _context.MovieCategories.Where(c => c.CategoryId == categoryId).Select(m => m.Movie).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
