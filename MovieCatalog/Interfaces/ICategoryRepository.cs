using MovieCatalog.Models;

namespace MovieCatalog.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Category> GetCategoryOfAMovie(int movieId);
        ICollection<Movie> GetMovieByCategory(int categoryId);
        bool CategoryExist(int categoryId);
        bool CreateCategory(Category category);
        bool Save();
    }
}
