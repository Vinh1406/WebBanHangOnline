using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.productId=id;
            var item=db.ProductImage.Where(x=>x.ProductId== id).ToList();
            return View(item);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImage.Find(id);
            if (item != null)
            {

                db.ProductImage.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult AddImage(int productId,string url)
        {
            db.ProductImage.Add(new Models.EF.ProductImage
            {
                ProductId = productId,
                Image=url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            try
            {
                // Logic để xóa tất cả ảnh trong cơ sở dữ liệu
                db.ProductImage.RemoveRange(db.ProductImage);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false});
            }
        }


    }
}