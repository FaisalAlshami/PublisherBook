using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PublisherBooks.Models
{
    
    public class User
    {
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("Username")]
        public string Username { get; set; }

        [Required]
        [BsonElement("Password")]
        public string Password { get; set; }


    }
}