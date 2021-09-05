using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTrip.Models.Classes;

namespace TravelTrip.Controllers
{
    public class NewController : Controller
    {
        // GET: New
        Context c = new Context();
        NewsComments nC = new NewsComments();
        public ActionResult Index()
        {
            var listem = c.News.ToList();
            foreach (var x in listem)
            {
                x.newCategory = c.Categories.Where(y => y.categoryID == x.newCategoryId).FirstOrDefault();
                x.newCategory.categoryName = x.newCategory.categoryName.ToUpper();
                x.newDetail = c.Comments.Where(y => y.commentNewId == x.newID).Count().ToString();
            }
            nC.Deger1 = c.News.ToList();
            nC.Deger3 = c.News.Take(5).ToList();
            nC.Deger4 = c.Comments.OrderByDescending(x => x.commentID).Take(5).ToList(); //Sağ menü son 5 yorum
            return View(nC);
        }
        public ActionResult NewDetail(int id = 9)
        {
            ViewBag.isComment = Request.QueryString["isComment"];

            nC.Deger1 = c.News.Where(x => x.newID == id).ToList(); // Haber detaylarını gösterir
            int dgr1 = nC.Deger1.ElementAt(0).newCategoryId;

            nC.Deger2 = c.Comments.Where(x => x.commentNewId == id).ToList();//Haber yorumlarını gösterir

            nC.Deger3 = c.News.Where(x => x.newCategoryId == dgr1).ToList(); //Benzer haberleri gösterir

            nC.Deger4 = c.Comments.OrderByDescending(x => x.commentID).Take(5).ToList(); //Sağ menü son 5 yorum

            nC.Deger5 = c.Categories.Where(x => x.categoryID == dgr1).Take(1).ToList(); // Tepedeki kategori yöneltme yapar


            int dgr6 = nC.Deger1.ElementAt(0).newEditorId;
            nC.Deger6 = c.Editors.Where(x => x.editorID == dgr6).Take(1).ToList();

            nC.Deger7 = c.News.ToList();//Tüm haberler listesi

            int benzerler = c.News.Find(id).newMainCategoryId;
            nC.Deger8 = c.Categories.Where(x => x.mainCategoryID == benzerler).ToList();
            if (id > 9)
            {
                NewViewNumber(id);
            }

            return View(nC);
        }
        [HttpGet]
        public PartialViewResult NewCommentList(int id = 1)
        {
            ViewBag.deger = id;
            ViewBag.isComment = (Request.QueryString["isComment"] == "true");
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult NewCommentList(Comment p1, int id)
        {
            if (p1.commentAvatar == null)
            {
                p1.commentAvatar = "/images/avatars/avatar-1.png";
            }
            p1.New = c.News.Where(x => x.newID == id).FirstOrDefault();
            p1.commentConfirm = false;
            c.Comments.Add(p1);
            ViewBag.deger = id;
            c.SaveChanges();
            Response.Redirect("/New/NewDetail/" + id + "?isComment=true");

            return PartialView();
        }
        public void NewViewNumber(int id)
        {
            c.News.Find(id).newViewNumber++;
            c.SaveChanges();
        }
    }
}