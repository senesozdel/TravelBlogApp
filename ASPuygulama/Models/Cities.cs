using ASPuygulama.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Models
{
    public class Cities: Common
    {

        public string Name { get; set; }

        public ICollection<BlogCityRelations> BlogCityRelations { get; set; }
    }
}