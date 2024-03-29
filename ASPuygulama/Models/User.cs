using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class User : Common
    {
        public string Image {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime SignDate { get; set; }

        public bool IsLogin { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<BlogPosts> BlogPosts { get; set; }
        public ICollection<BlogComments> BlogComments { get; set; }
        public ICollection<BlogLikes> BlogLikes { get; set; }
    }
}