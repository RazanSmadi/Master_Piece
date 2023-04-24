using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using razansmadi.Models;
namespace razansmadi.Controllers
{
    public class SubAdminChaletsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: SubAdminChalets
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser user = db.AspNetUsers.Find(id);

            var chalets = db.Chalets
                .Include(c => c.Feature)
                .Include(c => c.Image)
                .Where(c => c.userid == user.Id)
                .ToList();

            var ImgIds = chalets.Select(c => c.Images_ID).ToList();
            var mainImages = db.Images.Where(i => ImgIds.Contains(i.Images_ID)).ToList();
            var data = Tuple.Create(chalets, mainImages);
            return View(data);

        }

        public ActionResult Statistics()
        {
       
            return View();
       }
        //create images
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImg([Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }
     
        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Customer_Img")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }
     

        // GET: SubAdminChalets/Details/5
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

        // GET: SubAdminChalets/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID");
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage");
            return View();
        }

        // POST: SubAdminChalets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChaletID, userid, Features_ID, Images_ID, Chalet_Name, Chalet_Description, Chalet_Address, AvailabilityFlag, NnmberOfAdult, NnmberOfkid, PricePerNight, isAccepted, bedroom, bathroom, area, super, map, DateOfReg")] Chalet chalet)
        {
            if (ModelState.IsValid)
            {
                int? ChaletID = Session["ChaletID"] as int?;
                if (ChaletID != null)
                {
                    chalet.ChaletID = (int)ChaletID;
                }
                else
                {
                    // Handle the case where the featuresId value is null
                }

                int? Features_ID = Session["Features_ID"] as int?;

                if (Features_ID != null)
                {
                    chalet.Features_ID = Features_ID;
                }
                else
                {
                    // Handle the case where the featuresId value is null
                }

                int? Images_ID = Session["Images_ID"] as int?;

                if (Images_ID != null)
                {
                    chalet.Images_ID = Images_ID;

                }
                else
                {
                    // Handle the case where the featuresId value is null
                }

                db.Chalets.Add(chalet);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);


        }

        public ActionResult CreateDetails()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID");
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage");
            return View();
        }

        // POST: Chalets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetails([Bind(Include = "ChaletID,userid,Features_ID,Images_ID,Chalet_Name,Chalet_Description,Chalet_Address,AvailabilityFlag,NnmberOfAdult,NnmberOfkid,PricePerNight,isAccepted,bedroom,bathroom,area,super,map,DateOfReg")] Chalet chalet)
        {
            if (ModelState.IsValid)
            {
                db.Chalets.Add(chalet);
                db.SaveChanges();
                Session["ChaletID"] = chalet.ChaletID;
                return RedirectToAction("Create", new { ChaletID = chalet.ChaletID });
            }
            chalet.DateOfReg = DateTime.Today;
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            return View(chalet);
        }
        // GET: SubAdminChalets/Edit/5
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

        // POST: SubAdminChalets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChaletID,userid,Chalet_Name,Chalet_Description,Chalet_Address,AvailabilityFlag,NnmberOfAdult,NnmberOfkid,PricePerNight,Images_ID,Features_ID,isAccepted,bedroom,bathroom,area")] Chalet chalet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chalet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);
        }


        public ActionResult ADD(int id)
        {
            Chalet chalet = db.Chalets.Find(id);
            if (chalet == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                int? Features_ID = Session["Features_ID"] as int?;

                chalet.Features_ID = Features_ID;

                int? Images_ID = Session["Images_ID"] as int?;
                chalet.Images_ID = Images_ID;
                db.Entry(chalet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
           

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", chalet.userid);
            ViewBag.Features_ID = new SelectList(db.Features, "Features_ID", "Features_ID", chalet.Features_ID);
            ViewBag.Images_ID = new SelectList(db.Images, "Images_ID", "MainImage", chalet.Images_ID);
            return View(chalet);
        }







        // GET: SubAdminChalets/Delete/5
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

        // POST: SubAdminChalets/Delete/5
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
