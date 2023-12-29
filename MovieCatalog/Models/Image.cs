using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageURL { get; set; }
        public Movie Movie { get; set; }
    }
}
