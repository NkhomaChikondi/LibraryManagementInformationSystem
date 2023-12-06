namespace LMIS.Web.Models
{
    public class MemberType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
