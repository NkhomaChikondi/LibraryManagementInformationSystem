using LMIS.Api.Core.Repository.IRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class Book 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ISBN { get; set; }
        public int userId { get; set; } 
        public string Title { get; set; }        
        public string Author { get; set; }   
        public int CopyNumber { get; set; }
        public string ObtainedThrough { get; set; }
        public string Publisher { get; set; }       
        public string Genre { get; set; }        
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
