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
    public class transactionsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: transactions
        public ActionResult Index()
        {
            var transactions = db.transactions.Include(t => t.AspNetUser).Include(t => t.AspNetUser1);
            return View(transactions.ToList());
        }

        // GET: transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: transactions/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transactionId,userid,amount,totalpay,date")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // Add the new transaction to the database
                transaction.date = DateTime.Today;
                db.transactions.Add(new transaction { userid = transaction.userid, amount = transaction.amount, totalpay = 120.0, date = DateTime.Today });
                db.SaveChanges();

                // Update the "donePay" column for the user
                var user = db.AspNetUsers.FirstOrDefault(u => u.Id == transaction.userid);
                if (user != null)
                {
                    user.donePay = true;
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "SubAdminChalets");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            return View(transaction);
        }

        // GET: transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            return View(transaction);
        }

        // POST: transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transactionId,userid,amount,totalpay,date")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            return View(transaction);
        }

        // GET: transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transaction transaction = db.transactions.Find(id);
            db.transactions.Remove(transaction);
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
