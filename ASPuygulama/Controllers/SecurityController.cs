using ASPuygulama.Entity;
using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductManagementWeb.Controllers
{
    public class SecurityController : Controller
    {
        Datacontext db = new Datacontext();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(Email, false);//Giriş Yapma işlemi
                TempData["kullanici"] = user.Name;
                user.IsLogin = true;
                db.SaveChanges();
                if (Email== "admin@example.com")
                {
                    
                    return RedirectToAction("AdminPanel", "Panel");
                }
                else
                {

                    return RedirectToAction("UserPanel", "Panel");
                }
                

            }
            else
            {
                ViewData["Message"] = "Kullanıcı Bilgileri Uyuşmuyor.";
                return View();
            }
        }
        public ActionResult SignUp()
        {
            return View();
        
        }
        [HttpPost]
        public ActionResult SignUp(string name, string email, string password)
        {
            if (!String.IsNullOrEmpty(name)) 
            {
           

            User newUSer = new User();
            newUSer.Email = email;
            newUSer.Password = password;
            newUSer.Name = name;
            newUSer.SignDate = DateTime.Now;

         

                db.Users.Add(newUSer);
                db.SaveChanges();

            }


            return RedirectToAction("SignUp");

        }

        public ActionResult Logout()
        {
           
            var user = db.Users.FirstOrDefault(x=>x.IsLogin == true);
            user.IsLogin = false;
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}