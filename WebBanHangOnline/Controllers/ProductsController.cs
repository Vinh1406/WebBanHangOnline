using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int? id)
        {
            var item=db.Products.ToList();
            if (id != null)
            {
                item = item.Where(x => x.Id == id).ToList();
            }
            return View(item);
        }

        public ActionResult ProductCategory(string alias,int? id)
        {
            var item=db.Products.ToList();
            if (id >0)
            {
                item=item.Where(x=>x.ProductCategoryID == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName=cate.Title;
            }
            ViewBag.CateId = id;
            return View(item);
        }
        

        public ActionResult Partial_ItemsByCateId()
        {
            var items=db.Products.Where(x=>x.IsHome &&x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Detail(string alias,int id)
        {
            var product = db.Products
                       .Include(p => p.ProductImages)
                       .FirstOrDefault(p => p.Id == id);

            var item = db.Products.Find(id);
            return View(item);
        }
    }
}