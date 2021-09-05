using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTrip.Models.Classes;


namespace TravelTrip.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        NewsComments nC = new NewsComments();
        Context c = new Context();
        public ActionResult Index()
        {
            nC.Deger8 = c.Categories.ToList();
            nC.Deger9 = c.MainCategories.ToList();
            return View(nC);
        }
        public ActionResult MainCategoryDetail(int id = 1)
        {
            nC.Deger9 = c.MainCategories.ToList();
            nC.Deger8 = c.Categories.ToList();
            nC.Deger1 = c.News.Where(x => x.newMainCategoryId == id).ToList(); // Main categorydeki haberleri gösterir
            foreach (var item in nC.Deger1)
            {
                int temp = item.newID;
                item.newDetail = c.Comments.Where(x => x.commentNewId == temp).Count().ToString();
            }
            var catName = c.MainCategories.Find(id).mainCategoryName;
            ViewBag.catName = catName;
            return View(nC);
        }
        public ActionResult CategoryDetail(int id = 1)
        {
            nC.Deger9 = c.MainCategories.ToList();
            nC.Deger8 = c.Categories.ToList();
            nC.Deger1 = c.News.Where(x => x.newCategoryId == id).ToList(); // Main categorydeki haberleri gösterir
            foreach (var item in nC.Deger1)
            {
                int temp = item.newID;
                item.newDetail = c.Comments.Where(x => x.commentNewId == temp).Count().ToString();
            }
            int maincat = c.Categories.Find(id).mainCategoryID;
            ViewBag.maincat = maincat;
            var maincatName = c.MainCategories.Find(maincat).mainCategoryName;
            ViewBag.maincatName = maincatName;
            var catName = c.Categories.Find(id).categoryName;
            ViewBag.catName = catName;
            return View(nC);
        }
    }
}