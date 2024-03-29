using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Entity.Functions
{
    public class Functions
    {
        Datacontext db = new Datacontext();
        BlogPostProcess BlogPostProcess = new BlogPostProcess();
        public void Like(int id) 
        {
            var likedPost = BlogPostProcess.Get(id);
            if(likedPost != null)
            {
                BlogLikes blogLikes = new BlogLikes();
                blogLikes.UserId = likedPost.UserId;
                blogLikes.BlogId = likedPost.Id;

                db.BlogLikes.Add(blogLikes);
                db.SaveChanges();
                
            }
        
        }
        public void Comment(int id)
        {
            var CommentPost = BlogPostProcess.Get(id);
            if (CommentPost != null)
            {
                BlogComments blogComments = new BlogComments();
                blogComments.UserId = CommentPost.UserId;
                blogComments.BlogId = CommentPost.Id;
                blogComments.CommentTime = DateTime.Now;
                blogComments.Status = true;


                db.BlogComments.Add(blogComments);
                db.SaveChanges();

            }


        }
    }
}