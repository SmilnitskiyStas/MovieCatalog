using MovieCatalog.Models;
using MovieCatalog.Models.Dto;

namespace MovieCatalog.Interfaces
{
    public interface IImageRepository
    {
        ICollection<Image> GetImages();
        Image GetImage(int imageId);
        ICollection<Image> GetImageByMovie(int  movieId);
        bool GetImageExists(int imageId);
        bool CreateImage(Image imageCreate);
        bool Save();
    }
}
