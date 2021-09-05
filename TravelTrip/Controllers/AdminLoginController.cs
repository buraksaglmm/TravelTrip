using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTrip.Models.Classes;
using System.Web.Security;

namespace TravelTrip.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Editor editor)
        {
            var login = c.Editors.FirstOrDefault(x => x.editorUsername == editor.editorUsername && x.editorPassword == editor.editorPassword);
            if(login!=null)
            {
                FormsAuthentication.SetAuthCookie(login.editorUsername,false);
                Session["Username"] = login.editorUsername.ToString();
                Session["Password"] = login.editorPassword.ToString();
                Session["Photo"] = login.EditorPhoto.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
    }
}