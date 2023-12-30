using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Dto
{
    public class ProducerDto
    {
        public int ProducerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
