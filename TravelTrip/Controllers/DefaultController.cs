using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTrip.Models.Classes;

namespace TravelTrip.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();
        NewsComments nC = new NewsComments();

        // GET: Default
        public ActionResult Index()
        {
            nC.Deger1 = c.News.ToList(); //eskiden yeniye
            foreach (var item in nC.Deger1)
            {
                int id = item.newID;
                item.newDetail = c.Comments.Where(x => x.commentNewId == id).Count().ToString();
            }
            nC.Deger3 = c.News.OrderByDescending(x => x.newID).ToList();//yeniden eskiye
            nC.Deger4 = c.Comments.OrderByDescending(x => x.commentID).ToList();

            return View(nC);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public PartialViewResult LayoutRightPanel()
        {
            nC.Deger1 = c.News.ToList(); //eskiden yeniye
            foreach (var item in nC.Deger1)
            {
                int id = item.newID;
                item.newDetail = c.Comments.Where(x => x.commentNewId == id).Count().ToString();
            }
            nC.Deger4 = c.Comments.OrderByDescending(x => x.commentID).Take(10).ToList();

            return PartialView(nC);
        }
        public PartialViewResult DynamicNavbar()
        {
            nC.Deger9 = c.MainCategories.ToList();
            nC.Deger8 = c.Categories.ToList();

            return PartialView(nC);
        }
        public PartialViewResult BreakingNews()
        {
            nC.Deger3 = c.News.ToList();
            return PartialView(nC);
        }
        public PartialViewResult Footer()
        {
            nC.Deger9 = c.MainCategories.ToList();
            return PartialView(nC);
        }
        [HttpGet]
        public ActionResult SearchNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchNew(string search = "")
        {
            nC.Deger1 = c.News.Where(x => x.newDescription.Contains(search)).ToList();
            foreach (var item in nC.Deger1)
            {
                item.newEditorId = c.Comments.Where(x => x.commentNewId == item.newID).Count();

            }
            ViewBag.search = search.ToString();
            ViewBag.count = nC.Deger1.Count();
            return View(nC);
        }
    }
}