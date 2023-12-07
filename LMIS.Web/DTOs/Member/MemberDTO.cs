namespace LMIS.Web.DTOs.Member
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string Member_Code { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int userId { get; set; }
        public int MemberTypeId { get; set; }
    }
}
