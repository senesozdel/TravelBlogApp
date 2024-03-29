using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class PopulerBlogs 
    {
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent{ get; set; }
        public DateTime SharedTime{ get; set; }

        public string Comment { get; set; }
        public DateTime CommentTime { get; set; }

    }
}