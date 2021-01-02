using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCPhoneDirectoryProject.Models
{
    public class PhoneModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("UUID")]
        public string UUID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Surname")]
        public string Surname { get; set; }

        [BsonElement("Company")]
        public string Company { get; set; }

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Location")]
        public string Location { get; set; }

    }
}