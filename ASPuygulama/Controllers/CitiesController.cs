using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ASPuygulama.Entity;
using ASPuygulama.Models;

namespace ASPuygulama.Controllers
{
    public class CitiesController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: Cities
        public ActionResult Index()
        {
            return View(db.Cities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {

            Cities cities = db.Cities.Find(id);
            var tumpostlar =  db.BlogPosts.ToList();
            var citypostrelation = db.BlogCityRelations.ToList();
            var tumcommentler = db.BlogComments.ToList();
            
           var secilensehrinblogIdleri = citypostrelation.Where(x=>x.CityId ==  id).Select(x=>x.BlogId).ToList();

            List<BlogPosts> posts = new List<BlogPosts>();

            foreach (var item in tumpostlar)
            {
                for(var i = 0; i < secilensehrinblogIdleri.Count; i++)
                {
                    if(item.Id == secilensehrinblogIdleri[i])
                    {
            
                        posts.Add(item);
                    }
                }
              
            }
            List<BlogComments> comments = new List<BlogComments>();

            foreach (var item in tumcommentler)
            {
                for (var i = 0; i < secilensehrinblogIdleri.Count; i++)
                {
                    if (item.BlogId == secilensehrinblogIdleri[i])
                    {

                        comments.Add(item);
                    }
                }

            }

            PostlarveYorumlar postlarveYorumlar = new PostlarveYorumlar();

            postlarveYorumlar.Gonderiler = posts;
            postlarveYorumlar.Comments = comments;

            var model = postlarveYorumlar;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (cities == null)
            {
                return HttpNotFound();
            }
            ViewData["secilensehir"] = cities.Name;

            foreach(var i in db.BlogPosts.ToList())
                    {
                var Foto = db.Users.Where(x => x.Id == i.UserId).Select(x => x.Image);
                ViewData["Foto"] = Foto;
                break;
            }
         

            return View(postlarveYorumlar);
        }

        [HttpPost]
        public ActionResult Details(string comment, string blogId,  string like, string likedBlog)
        {
            BlogComments blogComments = new BlogComments();
            var signedUser = db.Users.FirstOrDefault(x => x.IsLogin == true);

            blogComments.BlogId = Convert.ToInt32(blogId);
            blogComments.CommentContent = comment;

            

            BlogLikes blogLikes = new BlogLikes();

            if (like != null)
            {
                blogLikes.UserId = Convert.ToInt32(signedUser.Id);
                blogLikes.BlogId = Convert.ToInt32(likedBlog);

                db.BlogLikes.Add(blogLikes);
            }

            if (comment != null)
            {
                blogComments.UserId = signedUser.Id;
                blogComments.Status = false;
                blogComments.CommentTime = DateTime.Now;

                db.BlogComments.Add(blogComments);
            }

           
            db.SaveChanges();

            return RedirectToAction("Details");

        }


        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(cities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cities);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = db.Cities.Find(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // POST: Cities/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cities);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = db.Cities.Find(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cities cities = db.Cities.Find(id);
            db.Cities.Remove(cities);
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
    }
}
