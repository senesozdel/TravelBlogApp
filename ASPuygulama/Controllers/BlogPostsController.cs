using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPuygulama.Entity;
using ASPuygulama.Models;

namespace ASPuygulama.Controllers
{
    public class BlogPostsController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.User);
            return View(blogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            return View(blogPosts);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
           
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult Create(string title,string content, string city)
        {
            var PostTitle = title;
            var PostContent = content;
            var PostCity = city;
            var user = TempData["kullanici"].ToString();

            BlogPosts blogPosts = new BlogPosts();

            blogPosts.Title = PostTitle;
            blogPosts.Content = PostContent;
            blogPosts.PostDate = DateTime.Now;
            blogPosts.UserId = db.Users.FirstOrDefault(x => x.Name == user).Id;

            db.BlogPosts.Add(blogPosts);
            db.SaveChanges();

            BlogCityRelations blogCityRelations = new BlogCityRelations();

            blogCityRelations.CityId = db.Cities.FirstOrDefault(x=>x.Name == city).Id;
            blogCityRelations.BlogId = db.BlogPosts.FirstOrDefault(x=>x.Title == PostTitle).Id;
            db.BlogCityRelations.Add(blogCityRelations);
            db.SaveChanges();
            return View();

        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", blogPosts.UserId);
            return View(blogPosts);
        }

        // POST: BlogPosts/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PostDate,UserId")] BlogPosts blogPosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", blogPosts.UserId);
            return View(blogPosts);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            return View(blogPosts);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPosts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListedPosts()
        {
            var tumpostlar = db.BlogPosts.ToList();
            var citypostrelation = db.BlogCityRelations.ToList();
            var tumcommentler = db.BlogComments.ToList();
            List<BlogPosts> posts = new List<BlogPosts>();
            List<BlogComments> comments = new List<BlogComments>();

            posts.AddRange(tumpostlar);
            comments.AddRange(tumcommentler);

            PostlarveYorumlar postlarveYorumlar = new PostlarveYorumlar();

            postlarveYorumlar.Gonderiler = posts;
            postlarveYorumlar.Comments = comments;


            return View(postlarveYorumlar);
        }
        [HttpPost]
        public ActionResult ListedPosts(string comment, string blogId,string like,string likedBlog)
        {
            var signedUser = db.Users.FirstOrDefault(x => x.IsLogin == true);
            BlogComments blogComments = new BlogComments();
            blogComments.BlogId = Convert.ToInt32(blogId);
            blogComments.CommentContent = comment;

            BlogLikes blogLikes = new BlogLikes();

            if(like != null)
            {
                blogLikes.UserId = Convert.ToInt32(signedUser.Id);
                blogLikes.BlogId = Convert.ToInt32(likedBlog);

                db.BlogLikes.Add(blogLikes);
            }

           

            if(comment != null)
            {
                blogComments.UserId = signedUser.Id;
                blogComments.Status = false;
                blogComments.CommentTime = DateTime.Now;

                db.BlogComments.Add(blogComments);
            }


            db.SaveChanges();
            ViewData["Basarılı"] = "Yorum Ekleme Talebiniz Alınmıştır.";
            return RedirectToAction("ListedPosts");

        }
    }
}
