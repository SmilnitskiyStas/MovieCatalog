using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Dto
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
