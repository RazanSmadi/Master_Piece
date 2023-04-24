using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using razansmadi.Models;
namespace razansmadi.Controllers
{
    public class FeaturesController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: Features
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            List<Feature> features = db.Features.Where(f => f.Features_ID == id).ToList();
            return View(features);
        }

        public ActionResult FeatureFORchalet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            List<Feature> features = db.Features.Where(f => f.Features_ID == id).ToList();
            return View(features);
        }


        // GET: Features/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // GET: Features/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Features_ID,Air_conditioning,Laundry,Refrigerator,Washer,Barbeque,Sauna,Wifi,indoorSwimmingPool,Microwave,SwimmingPool,YouthGames,KidsGames,OutdoorShower,Tv_Cable,Carpark,Features1,Features2")] Feature feature)
        {
           
            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                Session["Features_ID"] = feature.Features_ID;
                return RedirectToAction("Create", "SubAdminChalets", new { Features_ID = feature.Features_ID });
            }

            return View(feature);
        }
        public ActionResult Create2()
        {
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "Features_ID,Air_conditioning,Laundry,Refrigerator,Washer,Barbeque,Sauna,Wifi,indoorSwimmingPool,Microwave,SwimmingPool,YouthGames,KidsGames,OutdoorShower,Tv_Cable,Carpark,Features1,Features2")] Feature feature)
        {

            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                Session["Features_ID"] = feature.Features_ID;
                return RedirectToAction("index", "Features", new { Features_ID = feature.Features_ID });
            }

            return View(feature);
        }
        // GET: Features/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Features_ID,Air_conditioning,Laundry,Refrigerator,Washer,Barbeque,Sauna,Wifi,indoorSwimmingPool,Microwave,SwimmingPool,YouthGames,KidsGames,OutdoorShower,Tv_Cable,Carpark,Features1,Features2")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Features",new {id= feature.Features_ID });

            }
          

            return View(feature);
        }


        // GET: Features/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);
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
