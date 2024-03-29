using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class BlogComments: Common
    {
        public int BlogId { get; set; }

        public BlogPosts BlogPosts { get; set; }

        public string CommentContent { get; set; }

        

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CommentTime { get; set; }

        public bool Status { get; set; }
    }
}