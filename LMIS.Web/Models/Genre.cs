namespace LMIS.Web.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int MaximumBooksAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
      
    }
}
