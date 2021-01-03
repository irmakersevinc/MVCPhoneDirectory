using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCPhoneDirectoryProject.Models
{
    public class ReportModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("UUID")]
        public string UUID { get; set; }

        [BsonElement("Location")]
        public string Location { get; set; }

        [BsonElement("Date")]
        public string Date { get; set; }

        [BsonElement("Situation")]
        public string Situation { get; set; }

        [BsonElement("NumberOfPeople")]
        public string NumberOfPeople { get; set; }

    }
}