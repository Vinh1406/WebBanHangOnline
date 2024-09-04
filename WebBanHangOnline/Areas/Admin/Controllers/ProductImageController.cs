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

        [HttpPost]
        public ActionResult IsDefault(int id)
        {
            var item = db.ProductImage.Find(id);
            if (item != null)
            {
                // Đặt tất cả ảnh khác thành không phải mặc định
                var otherItems = db.ProductImage.Where(x => x.ProductId == item.ProductId && x.Id != id).ToList();
                foreach (var otherItem in otherItems)
                {
                    otherItem.IsDefault = false;
                }

                // Đặt ảnh được chọn thành mặc định
                item.IsDefault = !item.IsDefault;
                db.SaveChanges();
                return Json(new { success = true, isDefault = item.IsDefault });
            }
            return Json(new { success = false });
        }
        //[HttpPost]
        //public ActionResult SetAvatar(int id)
        //{
        //    var item=db.ProductImage.Where(x=>x.ProductId == id).FirstOrDefault();
        //}
        //[HttpPost]
        //public ActionResult SetAvatar(int id, int productId)
        //{
        //    // Đặt tất cả các hình ảnh khác không phải là avatar cho sản phẩm này
        //    var images = db.ProductImage.Where(x => x.ProductId == productId).ToList();
        //    foreach (var image in images)
        //    {
        //        image.IsDefault = image.Id == id;
        //    }
        //    db.SaveChanges();

        //    return Json(new { success = true });
        //}
        //[HttpPost]
        //public ActionResult UpdateAvatar(int productId, string imageUrl)
        //{
        //    // Tìm sản phẩm theo productId
        //    var product = db.Products.Find(productId);

        //    if (product == null)
        //    {
        //        return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
        //    }

        //    // Cập nhật ảnh đại diện
        //    product.Image = imageUrl;
        //    db.SaveChanges();

        //    return Json(new { success = true, imageUrl = product.Image });
        //}



    }
}