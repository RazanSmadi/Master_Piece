using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public ActionResult Create([Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image,
            HttpPostedFileBase MainImage, HttpPostedFileBase poolImage, HttpPostedFileBase gardenImage, HttpPostedFileBase playGroundImage,
            HttpPostedFileBase roomImage,HttpPostedFileBase kitchenImage , HttpPostedFileBase livingRoomImage,HttpPostedFileBase bathroomImage ,
            HttpPostedFileBase viewImage ,HttpPostedFileBase randomImage)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                string pathMainImage = guid + MainImage.FileName;
                MainImage.SaveAs(Server.MapPath("../img/MainImage/" + pathMainImage));
                image.MainImage = pathMainImage;

                Guid guid1 = Guid.NewGuid();
                string pathpoolImage = guid1 + poolImage.FileName;
                poolImage.SaveAs(Server.MapPath("../img/poolImage/" + pathpoolImage));
                image.poolImage = pathpoolImage;

                Guid guid3 = Guid.NewGuid();
                string pathgardenImage = guid3 + gardenImage.FileName;
                gardenImage.SaveAs(Server.MapPath("../img/gardenImage/" + pathgardenImage));
                image.gardenImage = pathgardenImage;

                Guid guid4 = Guid.NewGuid();
                string pathplayGroundImage = guid4 + playGroundImage.FileName;
                playGroundImage.SaveAs(Server.MapPath("../img/playGroundImage/" + pathplayGroundImage));
                image.playGroundImage = pathplayGroundImage;

                Guid guid5 = Guid.NewGuid();
                string pathroomImage = guid5 + roomImage.FileName;
                roomImage.SaveAs(Server.MapPath("../img/roomImage/" + pathroomImage));
                image.roomImage = pathroomImage;

                Guid guid6 = Guid.NewGuid();
                string pathkitchenImage = guid6 + kitchenImage.FileName;
                kitchenImage.SaveAs(Server.MapPath("../img/kitchenImage/" + pathkitchenImage));
                image.kitchenImage = pathkitchenImage;
         
          

                Guid guid7 = Guid.NewGuid();
                string pathlivingRoomImage = guid7 + livingRoomImage.FileName;
                livingRoomImage.SaveAs(Server.MapPath("../img/livingRoomImage/" + pathlivingRoomImage));
                image.livingRoomImage = pathlivingRoomImage;

                Guid guid8 = Guid.NewGuid();
                string pathbathroomImage = guid8 + bathroomImage.FileName;
                bathroomImage.SaveAs(Server.MapPath("../img/bathroomImage/" + pathbathroomImage));
                image.bathroomImage = pathbathroomImage;

                Guid guid9 = Guid.NewGuid();
                string pathrandomImage = guid9 + randomImage.FileName;
                randomImage.SaveAs(Server.MapPath("../img/randomImage/" + pathrandomImage));
                image.randomImage = pathrandomImage;

                Guid guid10 = Guid.NewGuid();
                string pathviewImage = guid10 + viewImage.FileName;
                viewImage.SaveAs(Server.MapPath("../img/viewImage/" + pathviewImage));
                image.viewImage = pathviewImage;


                db.Images.Add(image);
                db.SaveChanges();
                Session["Images_ID"] = image.Images_ID;
                var ChaletID = Session["ChaletID"];
                return RedirectToAction("ADD", "SubAdminChalets", new { Id = ChaletID });

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
        public ActionResult OwnerAllImages( int id)
        {

            Image img = db.Images.Find(id);
            List<Image> imges = db.Images.Where(f => f.Images_ID == 20).ToList();
            return View(imges);

        }


        public ActionResult Admin_Images(int id, int? imgId)
        {
            Session["id"] = id;
            

                Image img = db.Images.Find(imgId);
                List<Image> imges = db.Images.Where(f => f.Images_ID == imgId).ToList();
                return View(imges);

            
        }


        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(int? id,[Bind(Include = "Images_ID,MainImage,poolImage,gardenImage,playGroundImage,roomImage,kitchenImage,livingRoomImage,bathroomImage,viewImage,randomImage")] Image image, HttpPostedFileBase MainImage, HttpPostedFileBase poolImage, HttpPostedFileBase gardenImage, HttpPostedFileBase playGroundImage,
            HttpPostedFileBase roomImage, HttpPostedFileBase kitchenImage, HttpPostedFileBase livingRoomImage, HttpPostedFileBase bathroomImage,
            HttpPostedFileBase viewImage, HttpPostedFileBase randomImage){
         
            if (ModelState.IsValid)
            {
                if (MainImage != null)
                {
                    Guid guid = Guid.NewGuid();
                    string path = guid + MainImage.FileName;
                    MainImage.SaveAs(Server.MapPath("../../MainImage/" + path));
                    image.MainImage = path;
                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID ==id);
                    image.MainImage = existingModel.MainImage;
                }

                if (poolImage != null)
                {
                    Guid guid1 = Guid.NewGuid();
                    string pathpoolImage = guid1 + poolImage.FileName;
                    poolImage.SaveAs(Server.MapPath("../poolImage/" + pathpoolImage));
                    image.poolImage = pathpoolImage;
                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.poolImage = existingModel.poolImage;
                }
                if (gardenImage != null)
                {

                    Guid guid3 = Guid.NewGuid();
                    string pathgardenImage = guid3 + gardenImage.FileName;
                    gardenImage.SaveAs(Server.MapPath("../gardenImage/" + pathgardenImage));
                    image.gardenImage = pathgardenImage;

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.gardenImage = existingModel.gardenImage;
                }
                if (playGroundImage != null)
                {

                    Guid guid4 = Guid.NewGuid();
                    string pathplayGroundImage = guid4 + playGroundImage.FileName;
                    playGroundImage.SaveAs(Server.MapPath("../playGroundImage/" + pathplayGroundImage));
                    image.playGroundImage = pathplayGroundImage;

              

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.playGroundImage = existingModel.playGroundImage;
                }
                if (roomImage != null)
                {
                    Guid guid5 = Guid.NewGuid();
                    string pathroomImage = guid5 + roomImage.FileName;
                    roomImage.SaveAs(Server.MapPath("../roomImage/" + pathroomImage));
                    image.roomImage = pathroomImage;

                    

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.roomImage = existingModel.roomImage;
                }
                if (kitchenImage != null)
                {
                    Guid guid6 = Guid.NewGuid();
                    string pathkitchenImage = guid6 + kitchenImage.FileName;
                    kitchenImage.SaveAs(Server.MapPath("../kitchenImage/" + pathkitchenImage));
                    image.kitchenImage = pathkitchenImage;

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.kitchenImage = existingModel.kitchenImage;
                }
                if (livingRoomImage != null)
                {

                    Guid guid7 = Guid.NewGuid();
                    string pathlivingRoomImage = guid7 + livingRoomImage.FileName;
                    livingRoomImage.SaveAs(Server.MapPath("../livingRoomImage/" + pathlivingRoomImage));
                    image.livingRoomImage = pathlivingRoomImage;

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.livingRoomImage = existingModel.livingRoomImage;
                }
                if (bathroomImage != null)
                {

                    Guid guid8 = Guid.NewGuid();
                    string pathbathroomImage = guid8 + bathroomImage.FileName;
                    bathroomImage.SaveAs(Server.MapPath("../bathroomImage/" + pathbathroomImage));
                    image.bathroomImage = pathbathroomImage;

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.bathroomImage = existingModel.bathroomImage;
                }
                if (randomImage != null)
                {

                    Guid guid9 = Guid.NewGuid();
                    string pathrandomImage = guid9 + randomImage.FileName;
                    randomImage.SaveAs(Server.MapPath("../randomImage/" + pathrandomImage));
                    image.randomImage = pathrandomImage;

                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.randomImage = existingModel.randomImage;
                }
                if (viewImage != null)
                {

                    Guid guid10 = Guid.NewGuid();
                    string pathviewImage = guid10 + viewImage.FileName;
                    viewImage.SaveAs(Server.MapPath("../viewImage/" + pathviewImage));
                    image.viewImage = pathviewImage;
                }
                else
                {
                    var existingModel = db.Images.AsNoTracking().FirstOrDefault(x => x.Images_ID == id);
                    image.viewImage = existingModel.viewImage;
                }


                db.Entry(image).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("OwnerAllImages", "Images", new { id = image.Images_ID });

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
