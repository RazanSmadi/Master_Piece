using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using razansmadi.Models;
using System.Net.Mail;

namespace razansmadi.Controllers
{
    public class transactionsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: transactions
        public ActionResult Index()
        {
            var transactions = db.transactions.Include(t => t.AspNetUser).Include(t => t.Chalet);
            return View(transactions.ToList());
        }

        [HttpPost, ActionName("Pay")]
        [ValidateAntiForgeryToken]
        //public ActionResult Pay([Bind(Include = "transactionId,userid,ChaletID,amount,totalpay")] transaction transaction)
        //{
        //    var user = User.Identity.GetUserName().ToString();
           
        //    string id = db.AspNetUsers.FirstOrDefault(d => d.Email == user).Id;

             
        //    transaction PY = new transaction
        //    {
        //        userid = id,
        //        amount = 
        //        totalpay = int.Parse(Session["totalpay"].ToString()),

        //    };

        //    float t = float.Parse(Session["totalpay"].ToString());
        //    float down= float.Parse(Session["downPayment"].ToString());
       
        //    if (ModelState.IsValid)
        //    {

        //        db.transactions.Add(PY);
        //        db.SaveChanges();
        //        string Email = db.AspNetUsers.Where(x => x.Id == id).Select(x=>x.Email).Single();
        //        MailMessage mail = new MailMessage();
        //        mail.To.Add(Email);
        //        mail.From = new MailAddress("RoyalChalets@gmail.com");
        //        mail.Subject = "booking chalets";
        //        mail.Body = $"Your Payment of {down} JD has been successfully done and your booking has been done";

        //        mail.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Port = 587; // 25 465
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Credentials = new System.Net.NetworkCredential("RoyalChalets@gmail.com", "mbuyaativxrfntjx\r\n");
        //        smtp.Send(mail);
        //        return RedirectToAction("Home","Chalets");

        //    }


        //    return View(PY);
        //}

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
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid");
            return View();
        }

        // POST: transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transactionId,userid,ChaletID,amount")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid", transaction.ChaletID);
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
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid", transaction.ChaletID);
            return View(transaction);
        }

        // POST: transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transactionId,userid,ChaletID,amount")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", transaction.userid);
            ViewBag.ChaletID = new SelectList(db.Chalets, "ChaletID", "userid", transaction.ChaletID);
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
