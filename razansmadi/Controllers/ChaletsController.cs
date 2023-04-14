using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using razansmadi.Models;

namespace razansmadi.Controllers
{
    public class ChaletsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: Chalets
        public ActionResult Index()
        {
            var chalets = db.Chalets.Include(c => c.AspNetUser).Include(c => c.Feature).Include(c => c.Image).Where(x => x.isAccepted != true);
            return View(chalets.ToList());
        }
        public ActionResult Home()
        {
            var cate = db.Categories.ToList();
            var chalets = db.Chalets.Include(c => c.AspNetUser).Include(c => c.Feature).Include(c => c.Image).Where(x => x.super == true).ToList();
            var images = (from c in chalets
                          join i in db.Images on c.Images_ID equals i.Images_ID
                          select i).ToList();

            var features = (from c in chalets
                          join i in db.Features on c.Features_ID equals i.Features_ID
                          select i).ToList();

            var data = Tuple.Create(chalets, cate, images, features);

            return View(data);
        }

        public ActionResult AllChalet(string sortOrder, string address, string category)
        {
            var chalets = db.Chalets.Include(c => c.AspNetUser).Include(c => c.Feature).Include(c => c.Image);

            // Filter by address
            if (!string.IsNullOrEmpty(address))
            {
                chalets = chalets.Where(c => c.Chalet_Address == address);
            }

            // Filter by category
            if (!string.IsNullOrEmpty(category))
            {
                chalets = chalets.Where(c => c.CategoriesChalets.Any(cc => cc.Category.Category_Name == category));
            }

            // Sort by price
            if (sortOrder == "priceDesc")
            {
                chalets = chalets.OrderByDescending(c => c.PricePerNight);
            }
            else if (sortOrder == "priceAsc")
            {
                chalets = chalets.OrderBy(c => c.PricePerNight);
            }

            var categories = db.Categories.ToList();

            // Get all unique addresses and categories
            var uniqueAddresses = chalets.Select(c => c.Chalet_Address).Distinct().ToList();
            var uniqueCategories = categories.Select(c => c.Category_Name).Distinct().ToList();

            ViewBag.AddressList = db.Chalets.Where(c => c.Chalet_Address != null)
                                             .Select(c => c.Chalet_Address)
                                             .Distinct()
                                             .ToList();
            ViewBag.CategoryList = categories.Select(cat => cat.Category_Name).Distinct().ToList();

            var data = Tuple.Create(chalets.ToList(), categories, uniqueAddresses, uniqueCategories);
            return View(data);

        }



        public ActionResult ChaletDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }


            var images = db.Images.Where(i => i.Images_ID == chalet.Images_ID).ToList();
            var features = db.Features.Where(f => f.Features_ID == chalet.Features_ID).ToList();
            var user = db.AspNetUsers.FirstOrDefault(f => f.Id == chalet.userid);
            var data = Tuple.Create(user, chalet, features, images);
            return View(data);
        }





        public ActionResult acceptedChalet()
        {
            var chalets = db.Chalets.Include(c => c.AspNetUser).Include(c => c.Feature).Include(c => c.Image).Where(x=>x.isAccepted==true);
            return View(chalets.ToList());

        }

        public ActionResult acceptedDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chalet chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }
            return View(chalet);
        }

  

        // GET: Chalets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chalet chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }
            return View(chalet);
        }

        // GET: Chalets/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID");
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage");
            return View();
        }

        // POST: Chalets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChaletID,userid,Chalet_Name,Chalet_Description,Chalet_Address,AvailabilityFlag,NnmberOfAdult,NnmberOfkid,PricePerNight,Images_ID,Features_ID,isAccepted,bedroom,bathroom,area")] Chalet chalet)
        {
            if (ModelState.IsValid)
            {
                db.Chalets.Add(chalet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);
        }




        // GET: Chalets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chalet chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);
        }

        // POST: Chalets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "ChaletID,userid,Chalet_Name,Chalet_Description,Chalet_Address,AvailabilityFlag,NnmberOfAdult,NnmberOfkid,PricePerNight,Images_ID,Features_ID,isAccepted,bedroom,bathroom,area")] Chalet chalet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chalet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);
        }
       
        // GET: Chalets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chalet chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }
            return View(chalet);
        }

        // POST: Chalets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chalet chalet = db.Chalets.Find(id);
            db.Chalets.Remove(chalet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
