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


        //public ActionResult pay2([Bind(Include = "Id,Money1,userID")] Money money)
        //{
        //    var z = Request["Money_Payment"];
        //    double cardAddMoney = Convert.ToDouble(z);
        //    var idSubscribe = Session["id"];
        //    int idSubscribe1 = Convert.ToInt32(Session["id"]);
        //    int count = db.Moneys.Count();

        //    if (ModelState.IsValid)
        //    {
        //        double? subscribeAmount = db.Subscribes.Find(idSubscribe1).Money_Ammount;
        //        var id = User.Identity.GetUserId();

        //        double? check = cardAddMoney - subscribeAmount;
        //        if (check > 0)
        //        {
        //            db.AspNetUsers.Find(id).Subscribe += subscribeAmount;

        //            db.AspNetUsers.Find(id).SubScribeID = idSubscribe1;
        //            money.userID = id;
        //            // money.Money1 = Convert.ToInt32(z);
        //            db.AspNetUsers.Find(id).CardMoney += check;
        //            money.Id = count++;
        //            db.Moneys.Add(money);
        //            db.SaveChanges();
        //            TempData["pay"] = "You pay Successfully";
        //            TempData["balance"] = db.AspNetUsers.Find(id).CardMoney;

        //            var model = new AspNetUserRole();
        //            model.UserId = id;
        //            model.RoleId = "3";
        //            db.AspNetUserRoles.Add(model);
        //            db.SaveChanges();
        //            return RedirectToAction("LoginSell", "Account");
        //        }
        //        else
        //        {
        //            TempData["payNotEnough"] = "Your Balance is not enough";
        //            return RedirectToAction("pay2", "Master");

        //        }
        //        // double? moneyAmount = db.Subscribes.Find(Session["id"]).Money_Ammount;
        //        // db.AspNetUsers.Find(id).Subscribe = Convert.ToString(moneyAmount);




        //        //var email = db.AspNetUsers.Find(id).Email;
        //        //MailMessage mail = new MailMessage();
        //        //mail.To.Add($"{email}".ToString().Trim());
        //        //mail.From = new MailAddress("iahmed.hamaideh@gmail.com");
        //        //mail.Subject = "Hello " + email;
        //        //mail.Body = " <!DOCTYPE html>\r\n<html lang=\"en\" >\r\n<head>\r\n  <meta charset=\"UTF-8\">\r\n  <title>CodePen - New Account Email Template</title>\r\n  \r\n\r\n</head>\r\n<body>\r\n<!-- partial:index.partial.html -->\r\n<!doctype html>\r\n<html lang=\"en-US\">\r\n\r\n<head>\r\n    <meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />\r\n    <title>New Account Email Template</title>\r\n    <meta name=\"description\" content=\"New Account Email Template.\">\r\n    <style type=\"text/css\">\r\n        a:hover {text-decoration: underline !important;}\r\n    </style>\r\n</head>\r\n\r\n<body marginheight=\"0\" topmargin=\"0\" marginwidth=\"0\" style=\"margin: 0px; background-color: #f2f3f8;\" leftmargin=\"0\">\r\n    <!-- 100% body table -->\r\n    <table cellspacing=\"0\" border=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#f2f3f8\"\r\n        style=\"@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;\">\r\n        <tr>\r\n            <td>\r\n                <table style=\"background-color: #f2f3f8; max-width:670px; margin:0 auto;\" width=\"100%\" border=\"0\"\r\n                    align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n                    <tr>\r\n                        <td style=\"height:80px;\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"text-align:center;\">\r\n                            <a href=\"https://rakeshmandal.com\" title=\"logo\" target=\"_blank\">\r\n                            <img width=\"60\" src=\"https://i.ibb.co/hL4XZp2/android-chrome-192x192.png\" title=\"logo\" alt=\"logo\">\r\n                          </a>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"height:20px;\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>\r\n                            <table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"\r\n                                style=\"max-width:670px; background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);\">\r\n                                <tr>\r\n                                    <td style=\"height:40px;\">&nbsp;</td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td style=\"padding:0 35px;\">\r\n                                        <h1 style=\"color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;\">Payment Done\r\n                                        </h1>\r\n                                        <p style=\"font-size:15px; color:#455056; margin:8px 0 0; line-height:24px;\">\r\n  The payment process has been completed successfully  . Now go to the university website to register the courses.\r\n <br><strong>  We wish you success\r\n    .</p>\r\n                                        <span\r\n                                            style=\"display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;\"></span>\r\n                                        <p\r\n                                            style=\"color:#455056; font-size:18px;line-height:20px; margin:0; font-weight: 500;\">\r\n                                            <strong\r\n                                                                      <strong\r\n                                                style=\"display: block; font-size: 13px; margin: 24px 0 4px 0; f\r\n\r\n                                        <a href=\"https://localhost:44370/Home/Index\"\r  style=\"background:#20e277;text-decoration:none !important; display:inline-block; font-weight:500; margin-top:24px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;\"> Go to the university website\r\n </a>\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td style=\"height:40px;\">&nbsp;</td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"height:20px;\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"text-align:center;\">\r\n                            <p style=\"font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;\">&copy; <strong>www.Hamaideh_university.com</strong> </p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"height:80px;\">&nbsp;</td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <!--/100% body table-->\r\n</body>\r\n\r\n</html>\r\n<!-- partial -->\r\n  \r\n</body>\r\n</html>\r\n";
        //        //mail.IsBodyHtml = true;

        //        //SmtpClient smtp = new SmtpClient();
        //        //smtp.Port = 587; // 25 465
        //        //smtp.EnableSsl = true;
        //        //smtp.UseDefaultCredentials = false;
        //        //smtp.Host = "smtp.gmail.com";
        //        //smtp.Credentials = new System.Net.NetworkCredential("iahmed.hamaideh@gmail.com", "rdocplurxoijtlgk");

        //        //smtp.Send(mail);
        //    }
        //    else
        //    {

        //    }


        //    ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", "as");
        //    return RedirectToAction("pay2", "Master");

        //}

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
