using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class BlogLikes:Common
    {

        public int UserId { get; set; }

        public User User { get; set; }

        public int BlogId { get; set; }

        public BlogPosts BlogPosts { get; set; }
    }
}