using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using razansmadi.Models;

namespace razansmadi.Controllers
{
    public class AspNetUsersController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }
        public ActionResult Owners()
        {
            var usersWithRoleId2 = from user in db.AspNetUsers
                                   join userRole in db.AspNetUserRoles
                                   on user.Id equals userRole.UserId
                                   where userRole.RoleId == "2"
                                   select user;

            return View(usersWithRoleId2.ToList());
        }

        public ActionResult OwnersProfile(string id)
        {

            //var id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AspNetUser user = db.AspNetUsers.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult EditOwnersProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwnersProfile([Bind(Include = "Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName, Customer_Img")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }
        public ActionResult Customer()
        {
            var usersWithRoleId2 = from user in db.AspNetUsers
            join userRole in db.AspNetUserRoles
                                   on user.Id equals userRole.UserId
                                   where userRole.RoleId == "3"
                                   select user;

            return View(usersWithRoleId2.ToList());
        }

        //public ActionResult ProfileOwners(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    AspNetUser user = db.AspNetUsers.Find(id);

        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var chalets = db.Chalets.Include(c => c.AspNetUser).Include(c => c.Feature).Include(c => c.Image).Where(c => c.userid == user.Id).ToList();

        //    var images = (from c in chalets
        //                  join i in db.Images on c.Images_ID equals i.Images_ID
        //                  select i).ToList();

        //    var features = (from c in chalets
        //                    join i in db.Features on c.Features_ID equals i.Features_ID
        //                    select i).ToList();


        //    if (chalets == null)
        //    {
        //        // Handle the case where the user does not have an associated chalet.
        //    }
        //    var data = Tuple.Create(user,chalets, images, features);



        //    return View(data);
        //}

        public ActionResult UserProfile(string id = "0dfcd31a-12ff-430c-a1a0-a24170377bc9")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AspNetUser user = db.AspNetUsers.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            // Retrieve the booking record(s) for the user
            List<Booking> bookings = db.Bookings.Where(b => b.id == id).ToList();

            // Retrieve the corresponding chalet(s), features, and images for each booking
            List<Chalet> chalets = new List<Chalet>();
            List<Feature> features = new List<Feature>();
            List<Image> images = new List<Image>();
            foreach (Booking booking in bookings)
            {
                Chalet chalet = db.Chalets.Find(booking.ChaletID);
                if (chalet != null)
                {
                    chalets.Add(chalet);
                    Feature feature = db.Features.Find(chalet.Features_ID);
                    if (feature != null)
                    {
                        features.Add(feature);
                    }
                    Image image = db.Images.Find(chalet.Images_ID);
                    if (image != null)
                    {
                        images.Add(image);
                    }
                }
            }

            var Data = Tuple.Create(user, bookings, features, images);

            return View(Data);
        }


        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Customer_Img")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Customer_Img")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OwnersProfile");
            }
            return View(aspNetUser);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editowner([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Customer_Img")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }


        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
