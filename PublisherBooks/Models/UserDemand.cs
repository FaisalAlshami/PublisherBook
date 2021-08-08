using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PublisherBooks.Models
{
    public class UserDemand
    {
        // this class to maintain user demands
        public ObjectId Id { get; set; }
        // id to book


        public ObjectId UserId { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

       /* [BsonElement("BookTitle")]
        public string BookTitle { get; set; }

        [BsonElement("Publisher")]
        public string Publisher { get; set; }*/

       public virtual Book UserDemandBook {get; set;}

        public virtual User OwnUser { get; set; }

    }
}