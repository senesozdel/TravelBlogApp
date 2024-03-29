using ASPuygulama.Entity;
using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ASPuygulama.Controllers
{
    
    public class HomeController : Controller
    {
        Datacontext db = new Datacontext();
        BlogPostProcess blogPostProcess = new BlogPostProcess();
        public ActionResult Index()
        {
            var commentedPosts = db.BlogComments.ToList();

            var populer = commentedPosts.GroupBy(x => x.BlogId).OrderByDescending(group => group.Count()).Take(5).ToList();

            var posts = db.BlogPosts.ToList();



            var populerBlogs = from blog in db.BlogPosts.ToList()
                               join comment in db.BlogComments.ToList() on blog.Id equals comment.BlogId
                               join user in db.Users.ToList() on comment.UserId equals user.Id
                               select new PopulerBlogs
                               {
                                   BlogId = blog.Id,
                                   UserName = user.Name,
                                   BlogTitle = blog.Title,
                                   BlogContent = blog.Content,
                                   SharedTime = blog.PostDate,
                                   Comment = comment.CommentContent,
                                   CommentTime = comment.CommentTime 
             
                         
                               };

    
            var model = populerBlogs.ToList();

       


            return View(model);
        }
        public ActionResult About()
        {
            return View();
        }




    }
}