using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using TravelTrip.Models.Classes;
using System.IO;
using System.Web.Hosting;

namespace TravelTrip.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        NewsComments nC = new NewsComments();
        Context c = new Context();
        public ActionResult Index()
        {
            int totalComment = c.Comments.Count();
            ViewBag.totalComment = totalComment;
            int totalNew = c.News.Count();
            ViewBag.totalNew = totalNew;
            int totalMainCategory = c.MainCategories.Count();
            ViewBag.totalMainCategory = totalMainCategory;
            int totalCategory = c.Categories.Count();
            ViewBag.totalCategory = totalCategory;
            int totalView = c.News.Sum(x => x.newViewNumber);
            ViewBag.totalView = totalView;
            int totalEditor = c.Editors.Count();
            ViewBag.totalEditor = totalEditor;

            return View(nC);
        }
        public ActionResult EditProfile()
        {
            string sessionUsername = Session["Username"].ToString();
            string sessionPassword = Session["Password"].ToString();
            var currentEditor = c.Editors.Where(x => x.editorUsername == sessionUsername).FirstOrDefault();
            currentEditor.editorPassword = sessionPassword;
            return View(currentEditor);
        }

        #region /*PartialViews*/
        public PartialViewResult totalComment()
        {
            var listem = c.News.ToList();
            foreach (var x in listem)
            {
                x.Comments = c.Comments.Where(y => y.commentNewId == x.newID).ToList();
                x.newEditor = c.Editors.Where(y => y.editorID == x.newEditorId).FirstOrDefault();
                x.newEditor.editorUsername = x.newEditor.editorUsername;
                x.newCategory = c.Categories.Where(y => y.categoryID == x.newCategoryId).FirstOrDefault();
                x.newCategory.categoryName = x.newCategory.categoryName;

            }
            nC.Deger3 = listem;

            return PartialView("Dashboard/totalComment", nC);
        }
        public PartialViewResult totalNew()
        {
            var listem = c.News.ToList();
            foreach (var x in listem)
            {
                x.newCategory = c.Categories.Where(y => y.categoryID == x.newCategoryId).FirstOrDefault();
                x.newEditor = c.Editors.Where(y => y.editorID == x.newEditorId).FirstOrDefault();
                x.newEditor.editorUsername = x.newEditor.editorUsername;
                x.newCategory.categoryName = x.newCategory.categoryName;
            }
            nC.Deger1 = listem;

            return PartialView("Dashboard/totalNew", nC);
        }
        public PartialViewResult totalCategory()
        {
            var listem = c.Categories.ToList();
            foreach (var x in listem)
            {
                x.MainCategory = c.MainCategories.Where(y => y.mainCategoryID == x.mainCategoryID).FirstOrDefault();
                x.MainCategory.mainCategoryName = x.MainCategory.mainCategoryName;
            }
            nC.Deger5 = listem;

            return PartialView("Dashboard/totalCategory", nC);
        }
        public PartialViewResult totalView()
        {
            var listem = c.News.ToList();
            foreach (var x in listem)
            {
                x.Comments = c.Comments.Where(y => y.commentNewId == x.newID).ToList();
                x.newEditor = c.Editors.Where(y => y.editorID == x.newEditorId).FirstOrDefault();
                x.newEditor.editorUsername = x.newEditor.editorUsername;
                x.newCategory = c.Categories.Where(y => y.categoryID == x.newCategoryId).FirstOrDefault();
                x.newCategory.categoryName = x.newCategory.categoryName;

            }
            nC.Deger3 = listem;


            return PartialView("Dashboard/totalView", nC);
        }
        public PartialViewResult totalEditor()
        {

            var listem = c.News.ToList();
            var listem2 = c.Editors.ToList();
            foreach (var x in listem)
            {
                x.newEditor = c.Editors.Where(y => y.editorID == x.newEditorId).FirstOrDefault();
                x.newEditor.editorUsername = x.newEditor.editorUsername;
            }
            foreach (var item in listem2)
            {
                item.editorState = listem.Where(x => x.newEditor.editorID == item.editorID).Count().ToString();
            }

            foreach (var item in listem2)
            {
                var totalComment = listem.Where(x => x.newEditor.editorUsername == item.editorUsername).Sum(x => c.Comments.Where(y => y.commentNewId == x.newID).Count()).ToString();
                var totalView = listem.Where(x => x.newEditor.editorUsername == item.editorUsername).Sum(y => y.newViewNumber);
                item.EditorPhoto = (Convert.ToInt32(totalComment)).ToString();
                item.editorEmail = (Convert.ToInt32(totalView)).ToString();
            }

            nC.Deger6 = listem2;

            return PartialView("Dashboard/totalEditor", nC);
        }
        #endregion 

        #region /*News Block*/
        public ActionResult News()
        {
            var listem = c.News.ToList();
            foreach (var x in listem)
            {
                x.newCategory = c.Categories.Where(y => y.categoryID == x.newCategoryId).FirstOrDefault();
                x.newMainCategory = c.MainCategories.Where(y => y.mainCategoryID == x.newMainCategoryId).FirstOrDefault();
                x.newEditor = c.Editors.Where(y => y.editorID == x.newEditorId).FirstOrDefault();
                x.newEditor.editorUsername = x.newEditor.editorUsername;
                x.newCategory.categoryName = x.newCategory.categoryName;
                x.newMainCategory.mainCategoryName = x.newMainCategory.mainCategoryName;
            }

            var deger = c.News.ToList();

            return View(deger);
        }
        [HttpGet]
        public ActionResult AddNew()
        {
            List<SelectListItem> maincategoryList = (from i in c.MainCategories.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.mainCategoryName,
                                                         Value = i.mainCategoryID.ToString()
                                                     }).ToList();
            ViewBag.maincategoryList = maincategoryList;
            List<SelectListItem> categoryList = (from i in c.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.categoryName,
                                                     Value = i.categoryID.ToString()
                                                 }).ToList();
            ViewBag.categoryList = categoryList;
            List<SelectListItem> editorList = (from i in c.Editors.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.editorUsername,
                                                   Value = i.editorID.ToString()
                                               }).ToList();
            ViewBag.editorList = editorList;
            return View();
        }
        public JsonResult getDropdownCategory(int p)
        {
            var category = (from x in c.Categories
                            join y in c.MainCategories on x.mainCategoryID equals y.mainCategoryID
                            where x.mainCategoryID == p
                            select new
                            {
                                Text = x.categoryName,
                                Value = x.categoryID.ToString()
                            }).ToList();
            return Json(category, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddNew(New p1)
        {
            var maincategoryAdd = c.MainCategories.Where(m => m.mainCategoryID == p1.newMainCategory.mainCategoryID).FirstOrDefault();
            p1.newMainCategory = maincategoryAdd;
            var categoryAdd = c.Categories.Where(m => m.categoryID == p1.newCategory.categoryID).FirstOrDefault();
            p1.newCategory = categoryAdd;
            var editorAdd = c.Editors.Where(m => m.editorID == p1.newEditor.editorID).FirstOrDefault();
            p1.newEditor = editorAdd;

            c.News.Add(p1);
            var a = p1.newPhotoBase64;
            c.SaveChanges();
            string fileExtension = System.IO.Path.GetExtension(Request.Files[1].FileName); //Summernote eklendiği için index 0'dan 1'e geldi
            //string fileExtension = Path.GetExtension(Request.Files[0].FileName);
            string folder = "~/images/";
            string url = folder + p1.newID + fileExtension;
            var base64 = p1.newPhotoBase64.Split(new string[] { "base64," }, StringSplitOptions.None)[1];
            base64 = base64.Replace('-', '+');
            base64 = base64.Replace('_', '/');
            byte[] imageBytes = Convert.FromBase64String(base64);
            string urlPath = HttpContext.Server.MapPath(url);
            string folderPath = HttpContext.Server.MapPath(folder);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath); //Create directory if it doesn't exist
            }
            System.IO.File.WriteAllBytes(urlPath, imageBytes);
            p1.newPhoto = "/images/" + p1.newID + fileExtension;

            c.SaveChanges();
            return RedirectToAction("News");
        }
        public ActionResult DeleteNew(int id)
        {
            var news = c.News.Find(id);
            c.News.Remove(news);

            var comments = c.Comments.Where(x => x.commentNewId == news.newID).ToList();//Bağlı olan yorumları siliyor(Cascade)
            foreach (var item in comments)
            {
                c.Comments.Remove(item);
            }

            c.SaveChanges();
            return RedirectToAction("News");
        }
        public ActionResult GetNew(int id)
        {
            var news = c.News.Find(id);
            List<SelectListItem> categoryList = (from i in c.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.categoryName,
                                                     Value = i.categoryID.ToString()
                                                 }).ToList();
            ViewBag.categoryList = categoryList;
            List<SelectListItem> editorList = (from i in c.Editors.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.editorUsername,
                                                   Value = i.editorID.ToString()
                                               }).ToList();
            ViewBag.editorList = editorList;
            List<SelectListItem> maincategoryList = (from i in c.MainCategories.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.mainCategoryName,
                                                         Value = i.mainCategoryID.ToString()
                                                     }).ToList();
            ViewBag.maincategoryList = maincategoryList;
            return View("GetNew", news);
        }

        public ActionResult UpdateNew(New p1)
        {
            if (p1.newPhoto == null)
            {
                p1.newPhoto = "/images/" + p1.newID + ".jpg";
            }
            else
            {
                var a = p1.newPhotoBase64;
                c.SaveChanges();
                //string fileName = Path.GetFileName(Request.Files[0].FileName);
                string fileExtension = Path.GetExtension(Request.Files[1].FileName);
                string folder = "~/images/";
                string url = folder + p1.newID + fileExtension;
                var base64 = p1.newPhotoBase64.Split(new string[] { "base64," }, StringSplitOptions.None)[1];
                base64 = base64.Replace('-', '+');
                base64 = base64.Replace('_', '/');
                byte[] imageBytes = Convert.FromBase64String(base64);
                string urlPath = HttpContext.Server.MapPath(url);
                string folderPath = HttpContext.Server.MapPath(folder);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath); //Create directory if it doesn't exist
                }
                System.IO.File.WriteAllBytes(urlPath, imageBytes);
                p1.newPhoto = "/images/" + p1.newID + fileExtension;

            }

            var newUpdate = c.News.Find(p1.newID);
            newUpdate.newTitle = p1.newTitle;
            newUpdate.newDescription = p1.newDescription;
            newUpdate.newDetail = p1.newDetail;
            newUpdate.newPhoto = p1.newPhoto;
            var maincategoryAdd = c.MainCategories.Where(m => m.mainCategoryID == p1.newMainCategory.mainCategoryID).FirstOrDefault();
            newUpdate.newMainCategory = maincategoryAdd;
            var categoryAdd = c.Categories.Where(m => m.categoryID == p1.newCategory.categoryID).FirstOrDefault();
            newUpdate.newCategory = categoryAdd;
            var editorAdd = c.Editors.Where(m => m.editorID == p1.newEditor.editorID).FirstOrDefault();
            p1.newEditor = editorAdd;
            c.SaveChanges();
            return RedirectToAction("News");
        }
        #endregion

        #region /*MainCategories Block*/

        public ActionResult MainCategories()
        {
            var deger = c.MainCategories.ToList();
            return View(deger);
        }
        [HttpGet]   //her zaman çalışır
        public ActionResult AddMainCategory()
        {
            return View();
        }
        [HttpPost]  //sadece post işlemi olursa çalışır
        public ActionResult AddMainCategory(MainCategory p1)
        {
            if (!ModelState.IsValid)
            {
                return View("AddMainCategory");
            }
            c.MainCategories.Add(p1);
            c.SaveChanges();
            return RedirectToAction("MainCategories");
        }

        public ActionResult DeleteMainCategory(int id)
        {
            var deletedMainCategory = c.MainCategories.Find(id);
            c.MainCategories.Remove(deletedMainCategory);
            c.SaveChanges();

            return RedirectToAction("MainCategories");
        }

        public ActionResult GetMainCategory(int id)
        {
            var maincategoryGet = c.MainCategories.Find(id);
            return View("GetMainCategory", maincategoryGet);
        }

        public ActionResult UpdateMainCategory(MainCategory p1)
        {
            var maincategoryUpdate = c.MainCategories.Find(p1.mainCategoryID);
            maincategoryUpdate.mainCategoryName = p1.mainCategoryName;
            c.SaveChanges();
            return RedirectToAction("MainCategories");
        }
        #endregion

        #region /*Categories Block*/

        public ActionResult Categories()
        {
            var listem = c.Categories.ToList();
            foreach (var x in listem)
            {
                x.MainCategory = c.MainCategories.Where(y => y.mainCategoryID == x.mainCategoryID).FirstOrDefault();
                x.MainCategory.mainCategoryName = x.MainCategory.mainCategoryName;
            }

            var deger = c.Categories.ToList();
            return View(deger);
        }

        [HttpGet]   //her zaman çalışır
        public ActionResult AddCategory()
        {
            List<SelectListItem> maincategoryList = (from i in c.MainCategories.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.mainCategoryName,
                                                         Value = i.mainCategoryID.ToString()
                                                     }).ToList();
            ViewBag.maincategoryList = maincategoryList;

            return View();
        }

        [HttpPost]  //sadece post işlemi olursa çalışır
        public ActionResult AddCategory(Category p1)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCategory");
            }
            c.Categories.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id)
        {
            var deletedCategory = c.Categories.Find(id);
            c.Categories.Remove(deletedCategory);
            c.SaveChanges();

            return RedirectToAction("Categories");
        }

        public ActionResult GetCategory(int id)
        {
            var categoryGet = c.Categories.Find(id);
            List<SelectListItem> maincategoryList = (from i in c.MainCategories.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.mainCategoryName,
                                                         Value = i.mainCategoryID.ToString()
                                                     }).ToList();
            ViewBag.maincategoryList = maincategoryList;
            return View("GetCategory", categoryGet);
        }

        public ActionResult UpdateCategory(Category p1)
        {
            var categoryUpdate = c.Categories.Find(p1.categoryID);
            categoryUpdate.categoryName = p1.categoryName;
            categoryUpdate.categoryPhoto = p1.categoryPhoto;
            var maincategoryAdd = c.MainCategories.Where(m => m.mainCategoryID == p1.MainCategory.mainCategoryID).FirstOrDefault();
            categoryUpdate.MainCategory = maincategoryAdd;
            c.SaveChanges();
            return RedirectToAction("Categories");
        }
        #endregion

        #region /*Editors Block*/
        public ActionResult Editors()
        {
            var editors = c.Editors.ToList();
            return View(editors);
        }

        [HttpGet]   //her zaman çalışır
        public ActionResult AddEditor()
        {
            return View();
        }

        [HttpPost]  //sadece post işlemi olursa çalışır
        public ActionResult AddEditor(Editor p1)
        {
            c.Editors.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Editors");
        }

        public ActionResult UpdateEditor(Editor p1)
        {

            var editorUpdate = c.Editors.Find(p1.editorID);
            editorUpdate.editorUsername = p1.editorUsername;
            editorUpdate.editorFirstname = p1.editorFirstname;
            editorUpdate.editorLastname = p1.editorLastname;
            editorUpdate.editorEmail = p1.editorEmail;
            editorUpdate.editorPassword = p1.editorPassword;
            editorUpdate.editorDetail = p1.editorDetail;
            editorUpdate.EditorPhoto = p1.EditorPhoto;
            editorUpdate.editorState = p1.editorState;
            c.SaveChanges();
            return RedirectToAction("Editors");
        }

        public ActionResult GetEditor(int id)
        {
            var editorGet = c.Editors.Find(id);

            return View("GetEditor", editorGet);
        }

        public ActionResult DeleteEditor(int id)
        {
            var deletedEditor = c.Editors.Find(id);
            c.Editors.Remove(deletedEditor);
            var news = c.News.Where(x => x.newEditorId == deletedEditor.editorID).ToList();//Bağlı olan haberleri siliyor(Cascade)
            foreach (var item in news)
            {
                int dgr = item.newID;
                var dgr2 = c.Comments.Where(x => x.commentNewId == dgr).ToList();
                foreach (var itemm in dgr2)
                {
                    c.Comments.Remove(itemm);
                }

                c.News.Remove(item);
            }

            c.SaveChanges();

            return RedirectToAction("Editors");
        }
        #endregion

        #region /*Comments Block*/
        public ActionResult Comments()
        {
            var comments = c.Comments.ToList();
            return View(comments);
        }
        public ActionResult CommentsConfirm()
        {
            var comments = c.Comments.ToList();
            return View(comments);
        }

        public ActionResult ConfirmComment(int id)
        {
            c.Comments.Find(id).commentConfirm = true;
            c.SaveChanges();
            return RedirectToAction("Comments");
        }

        public ActionResult DeleteComment(int id)
        {
            var comment = c.Comments.Find(id);
            c.Comments.Remove(comment);
            c.SaveChanges();
            return RedirectToAction("Comments");
        }
        public ActionResult GetComment(int id)
        {
            var commentGet = c.Comments.Find(id);
            return View("GetComment", commentGet);
        }

        public ActionResult UpdateComment(Comment p1)
        {

            var commentUpdate = c.Comments.Where(x => x.commentID == p1.commentID).FirstOrDefault();
            commentUpdate.commentUsername = p1.commentUsername;
            commentUpdate.commentMessage = p1.commentMessage;
            commentUpdate.commentMail = p1.commentMail;
            commentUpdate.commentAvatar = p1.commentAvatar;

            c.SaveChanges();
            return RedirectToAction("Comments");
        }

        #endregion
    }
}