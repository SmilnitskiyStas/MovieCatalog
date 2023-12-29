using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Dto
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
