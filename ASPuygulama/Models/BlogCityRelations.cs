using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class BlogCityRelations : Common
    {

        public int CityId { get; set; }
        public Cities City { get; set; }
        public int BlogId { get; set; }

        public BlogPosts BlogPostsId { get; set; }


    }
}