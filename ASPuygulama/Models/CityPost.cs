using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class CityPost 
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent{ get; set; }
        public DateTime SharedTime{ get; set; }


    }
}