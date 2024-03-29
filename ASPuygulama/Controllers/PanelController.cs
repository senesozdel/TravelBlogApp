using ASPuygulama.Entity;
using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPuygulama.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        Datacontext db = new Datacontext();

        public ActionResult UserPanel()
        {
            var loginuser = db.Users.FirstOrDefault(x=>x.IsLogin == true);
         
            if (loginuser != null)
            {
                
                var user = db.Users.FirstOrDefault(x=>x.Name == loginuser.Name);
                ViewData["user"] = loginuser.Name;

                List<BlogPosts> posts = new List<BlogPosts>();

                foreach(var item in db.BlogPosts.ToList())
                {
                    if(item.UserId == user.Id)
                    {
                       posts.Add(item);
                    }
                }
                List<BlogComments> comments = new List<BlogComments>();

                foreach (var item in db.BlogComments.ToList())
                {
                    foreach (var blog in db.BlogPosts.ToList())
                    {
                        if (item.BlogId == blog.Id)
                        {
                            comments.Add(item);
                        }
                    }

                }
                PostlarveYorumlar postlarveYorumlar = new PostlarveYorumlar();
                postlarveYorumlar.Gonderiler = posts;
                postlarveYorumlar.Comments = comments;

                

                return View(postlarveYorumlar);

            }
            return RedirectToAction("Login","Security");



        }
        public ActionResult AdminPanel()
        {
            var girisyapanuser = db.Users.FirstOrDefault(x => x.IsLogin == true);

            if (girisyapanuser != null)
            {

                return View(girisyapanuser);

            }
            return RedirectToAction("Login", "Security");
        }

        [HttpPost]
        public ActionResult AdminPanel(HttpPostedFileBase file)
        {

            var  girisyapanuser = db.Users.FirstOrDefault(x => x.IsLogin == true);

               if(girisyapanuser != null)
            {

                string ImageName = "";
                string ImagePath = "";

                if (file != null && file.ContentLength > 0)
                {

                    ImageName = Path.GetFileName(file.FileName);

                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/userImages/"), ImageName)))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/userImages/"), ImageName));
                    }

                    ImagePath = Path.Combine(Server.MapPath("~/Content/userImages/"), ImageName);
                    file.SaveAs(ImagePath);
                    girisyapanuser.Image = "/Content/userImages/"+ ImageName;
                    db.SaveChanges();

                }

            }

            return RedirectToAction("AdminPanel","Panel"); // 
        }
        public ActionResult PendingComments()
        {
            List<BlogPosts> posts = new List<BlogPosts>();
            List<BlogComments> onayBekleyenYorumlar = new List<BlogComments>();

           onayBekleyenYorumlar = db.BlogComments.Where(x=>x.Status == false).ToList();
           


            foreach (var item in db.BlogPosts.ToList())
            {
                for (var i = 0; i < onayBekleyenYorumlar.Count; i++)
                {
                    if (item.Id == onayBekleyenYorumlar[i].BlogId)
                    {

                        posts.Add(item);
                    }
                }

            }
            PostlarveYorumlar postlarveYorumlar = new PostlarveYorumlar();
            postlarveYorumlar.Gonderiler = posts;
            postlarveYorumlar.Comments = onayBekleyenYorumlar;

            return View(postlarveYorumlar);
        }
        [HttpPost]
        public ActionResult PendingComments(string approve)
        {
           
                foreach (var item in db.BlogComments.ToList())
                {
                    if (Convert.ToString(item.Id) == approve)
                    {
                        item.Status = true;
                        db.SaveChanges();
                    }
                    
                }
           

            return RedirectToAction("PendingComments");
        }
        public ActionResult LikedPosts()
        {
            var likedPosts = db.BlogLikes.ToList();

            List<BlogPosts> modelPost = new List<BlogPosts>();

            foreach (var item in db.BlogPosts.ToList())
            {
                foreach (var post in likedPosts)
                {
                    if(item.Id == post.BlogId)
                    {
                        modelPost.Add(item);
                    }
                }
            }

          


            return View(modelPost);
        }
        


    }
}