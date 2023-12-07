using System.ComponentModel.DataAnnotations;

namespace LMIS.Web.DTOs.Genre
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int MaximumBooksAllowed { get; set; }
    }
}
