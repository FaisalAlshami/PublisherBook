using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PublisherBooks.Models
{
    public class Book
    {
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Publisher")]
        public string Publisher { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Authors")]
        public List<string> Authors { get; set; }

        
    }

}