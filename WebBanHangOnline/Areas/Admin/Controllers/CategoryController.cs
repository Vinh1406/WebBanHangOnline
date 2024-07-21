using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = db.categories;
            return View(items);
        }
    }
}