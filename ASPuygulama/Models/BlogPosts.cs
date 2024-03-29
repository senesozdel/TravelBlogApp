using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class BlogPosts:Common
    {
        public string Image {  get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


        public ICollection<BlogComments> BlogComments { get; set; }
        public ICollection<BlogLikes> BlogLikes { get; set; }

        public ICollection<BlogCityRelations> BlogCityRelations { get; set; }


    }
}