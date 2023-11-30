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
        public string State { get; set; }
        public string ObtainedThrough { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }        
        public DateTime CreatedOn { get; set; }
    }
}
