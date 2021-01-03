using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using MVCPhoneDirectoryProject.App_Start;
using MongoDB.Driver;
using MVCPhoneDirectoryProject.Models;

namespace MVCPhoneDirectoryProject.Controllers
{
    public class PhoneController : Controller
    {

        IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");

        public PhoneController()
        {


        }
        // GET: Phone
        public ActionResult Index()
        {
            var database = mongoClient.GetDatabase("phonedirectoryDB");
            var collection = database.GetCollection<PhoneModel>("phonedirectory");
            var phones = collection.Find<PhoneModel>(a => true).ToList();
           //List<PhoneModel> phones = phoneCollection.AsQueryable<PhoneModel>().ToList();
            return View(phones);
        }

        // GET: Phone/Details/5
        public ActionResult Details(string id)
        {
            var phoneId = new ObjectId(id);
            var database = mongoClient.GetDatabase("phonedirectoryDB");
            var collection = database.GetCollection<PhoneModel>("phonedirectory");
            var phones = collection.AsQueryable<PhoneModel>().SingleOrDefault(x => x.Id == phoneId);
            return View(phones);

        }

        // GET: Phone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phone/Create
        [HttpPost]
        public ActionResult Create(PhoneModel phone)
        {
            try
            {
                var database = mongoClient.GetDatabase("phonedirectoryDB");
                var collection = database.GetCollection<PhoneModel>("phonedirectory");
                collection.InsertOne(phone);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Phone/Edit/5
        public ActionResult Edit(string id)
        {
            var database = mongoClient.GetDatabase("phonedirectoryDB");
            var collection = database.GetCollection<PhoneModel>("phonedirectory");
            var phoneId = new ObjectId(id);
            var phone = collection.AsQueryable<PhoneModel>().SingleOrDefault(x => x.Id == phoneId);
             return View(phone);

        }

        // POST: Phone/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, PhoneModel phone)
        {
            try
            {
                var database = mongoClient.GetDatabase("phonedirectoryDB");
                var collection = database.GetCollection<PhoneModel>("phonedirectory");
                var filter = Builders<PhoneModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<PhoneModel>.Update
                    .Set("UUID", phone.UUID)
                    .Set("Name", phone.Name)
                    .Set("Surname", phone.Surname)
                    .Set("Company", phone.Company)
                    .Set("PhoneNumber", phone.PhoneNumber)
                    .Set("Email", phone.Email)
                    .Set("Location", phone.Location);
                var result = collection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Phone/Delete/5
        public ActionResult Delete(string id)
        {
            var database = mongoClient.GetDatabase("phonedirectoryDB");
            var collection = database.GetCollection<PhoneModel>("phonedirectory");
            var phoneId = new ObjectId(id);
            var phone = collection.AsQueryable<PhoneModel>().SingleOrDefault(x => x.Id == phoneId);
            return View(phone);

        }

        // POST: Phone/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, PhoneModel phone)
        {
            try
            {
                var database = mongoClient.GetDatabase("phonedirectoryDB");
                var collection = database.GetCollection<PhoneModel>("phonedirectory");
                collection.DeleteOne(Builders<PhoneModel>.Filter.Eq("_id",ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Report()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Report(string id)
        {
            try
            {
                var phoneId = new ObjectId(id);
                var database = mongoClient.GetDatabase("phonedirectoryDB");
                var collection = database.GetCollection<PhoneModel>("phonedirectory");
                var phones = collection.Find<PhoneModel>(a => true).ToList();
                var phone = collection.AsQueryable<PhoneModel>().SingleOrDefault(x => x.Id == phoneId);


                var database2 = mongoClient.GetDatabase("reportsDB");
                var collection2 = database.GetCollection<ReportModel>("reports");

                var countOfPeople=0;
                foreach(var item in phones)
                {
                    if (item.Location == phone.Location) ;
                    countOfPeople++;
                }
                var stringcountOfPeople = countOfPeople.ToString();
                collection2.InsertOneAsync(new ReportModel { UUID = phone.UUID, Location = phone.Location, NumberOfPeople= stringcountOfPeople });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListOfReports()
        {


            var database = mongoClient.GetDatabase("phonedirectoryDB");
            var collection = database.GetCollection<ReportModel>("reports");
            var reports = collection.Find<ReportModel>(a => true).ToList();

            return View(reports);
        }
       
    }
}
