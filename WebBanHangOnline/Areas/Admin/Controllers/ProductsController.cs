using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products

        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                model.ProductImages = new List<ProductImage>();

                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        var isDefault = (i + 1 == rDefault[0]);
                        var productImage = new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = isDefault
                        };

                        model.ProductImages.Add(productImage);

                        if (isDefault)
                        {
                            model.Image = Images[i];
                        }
                    }
                }

                model.CreateDate = DateTime.Now;
                model.ModifiedrDate = DateTime.Now;

                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = WebBanHangOnline.Models.Commons.Filter.FilterChar(model.Title);
                }

                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }

                db.Products.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title", model.ProductCategoryID);

            return View(model);
        }



       
        public ActionResult Edit(int id)
        {
            // Nạp sản phẩm bao gồm cả hình ảnh sản phẩm
            var item = db.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title", item.ProductCategoryID);
            return View(item);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                // Nạp sản phẩm từ cơ sở dữ liệu để chỉnh sửa
                var product = db.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == model.Id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                product.ProductCategoryID = model.ProductCategoryID;
                product.Title = model.Title;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Detail = model.Detail;
                product.Quantity = model.Quantity;
                product.Alias = WebBanHangOnline.Models.Commons.Filter.FilterChar(model.Title);
                product.ModifiedrDate = DateTime.Now;
                // Lưu thay đổi
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title", model.ProductCategoryID);
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items.Any() && items != null)
                {
                    foreach (var item in items)
                    {
                        var id = db.Products.Find(Convert.ToInt32(item));
                        db.Products.Remove(id);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item=db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });

        }


        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(new { success = false });

        }
        //[HttpPost]
        //public ActionResult UpdateAvatar(int productId, string imageUrl)
        //{
        //    // Giả sử bạn có một lớp Product để làm việc với sản phẩm
        //    var product = db.Products.Find(productId); // Lấy sản phẩm từ database
        //    if (product != null)
        //    {
        //        product.Image = imageUrl; // Cập nhật URL ảnh đại diện
        //        db.SaveChanges(); // Lưu thay đổi vào database
        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
        //}


    }
}