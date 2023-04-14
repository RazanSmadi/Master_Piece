using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using razansmadi.Models;

namespace razansmadi.Controllers
{
    public class ImagesController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }
        public ActionResult AllImages(int id )
        {

            Image img = db.Images.Find(id);
            List<Image> imges = db.Images.Where(f => f.Images_ID == id).ToList();
            return View(imges);

        }


        public ActionResult ImgFORchalet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image img = db.Images.Find(id);
            List<Image> Images = db.Images.Where(f => f.Images_ID == id).ToList();
            return View(Images);
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                Session["Images_ID"] = image.Images_ID;
                return RedirectToAction("Create", "SubAdminChalets");

            }

            return View(image);
        }


        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OwnerAllImages(int id)
        {

            Image img = db.Images.Find(id);
            List<Image> imges = db.Images.Where(f => f.Images_ID == id).ToList();
            return View(imges);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwnerAllImages([Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }










        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SubAdminChalets");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
