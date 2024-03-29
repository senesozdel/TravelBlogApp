using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class PostlarveYorumlar
    {
        public List<BlogPosts> Gonderiler {  get; set; }

        public List<BlogComments> Comments { get; set; }
    }
}