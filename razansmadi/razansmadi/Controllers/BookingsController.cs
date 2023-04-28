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
    public class BookingsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.AspNetUser).Include(b => b.Chalet);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid");
   

            ViewData["NumOfAdult"] = new List<SelectListItem>
    {
        new SelectListItem { Text = "Less than 20 adults", Value = "1" },
        new SelectListItem { Text = "more than 20 adults", Value = "2" }
    };
            ViewData["NumOfKids"] = new List<SelectListItem>
    {
        new SelectListItem { Text = "Less than 20 Kids", Value = "1" },
        new SelectListItem { Text = "more than 20 Kids", Value = "2" }
    };

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,ChaletID,id,CheckInDate,CheckOutDate,CheckInTime,TotalPrice,PaymentStatus,NumOfAdult,NumOfKids")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            ViewBag.id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid");
            ViewData["CheckInTime"] = new List<SelectListItem>
    {
        new SelectListItem { Text = "Duration", Value = "" },
        new SelectListItem { Text = "10AM-9PM", Value = "1" },
        new SelectListItem { Text = "10PM-9AM", Value = "2" },
        new SelectListItem { Text = "Full Day", Value = "3" }
    };

            ViewData["NumOfAdult"] = new List<SelectListItem>
    {
        new SelectListItem { Text = "Number Of Adult", Value = "" },
        new SelectListItem { Text = "Less than 20 adults", Value = "1" },
        new SelectListItem { Text = "more than 20 adults", Value = "2" }
    };
            ViewData["NumOfKids"] = new List<SelectListItem>
    {
        new SelectListItem { Text = "Number Of Kids", Value = "" },
        new SelectListItem { Text = "Less than 20 Kids", Value = "1" },
        new SelectListItem { Text = "more than 20 Kids", Value = "2" }
    };
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.AspNetUsers, "Id", "Email", booking.id);
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid", booking.ChaletID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,ChaletID,id,CheckInDate,CheckOutDate,CheckInTime,TotalPrice,PaymentStatus,NumOfAdult,NumOfKids")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.AspNetUsers, "Id", "Email", booking.id);
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid", booking.ChaletID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound("/Index");
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("/Index");
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
