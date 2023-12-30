using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Dto
{
    public class ReviewerDto
    {
        public int ReviewerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
