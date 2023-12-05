using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class Genre 
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int MaximumBooksAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public int userId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
