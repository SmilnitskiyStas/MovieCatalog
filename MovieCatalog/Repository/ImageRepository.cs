using MovieCatalog.Data;
using MovieCatalog.Interfaces;
using MovieCatalog.Models;
using MovieCatalog.Models.Dto;

namespace MovieCatalog.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateImage(Image imageCreate)
        {
            _context.Images.Add(imageCreate);

            return Save();
        }

        public bool DeleteImage(Image imageDelete)
        {
            _context.Images.Remove(imageDelete);
            return Save();
        }

        public Image GetImage(int imageId)
        {
            return _context.Images.Where(i => i.ImageId == imageId).FirstOrDefault();
        }

        public ICollection<Image> GetImageByMovie(int movieId)
        {
            return _context.Images.Where(i => i.Movie.MovieId == movieId).ToList();
        }

        public bool GetImageExists(int imageId)
        {
            return _context.Images.Any(i => i.ImageId == imageId);
        }

        public ICollection<Image> GetImages()
        {
            return _context.Images.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateImage(Image imageUpdate)
        {
            _context.Images.Update(imageUpdate);
            return Save();
        }
    }
}
