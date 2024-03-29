using ASPuygulama.Entity.Interfaces;
using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Entity
{
    public class BlogPostProcess : ICrud<BlogPosts>
    {
        Datacontext db = new Datacontext();
        public string Add(BlogPosts entity)
        {
            string output = " ";
            var post = db.BlogPosts.FirstOrDefault(x => x.Id == entity.Id);

            if (post == null)
            {
                db.BlogPosts.Add(entity);
                output = entity.Title + "eklendi";
            }
            else
            {
                output = entity.Title + "mevcut. Başka post adı seçiniz";
            }
            return output;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BlogPosts Get(int id)
        {
            var post = db.BlogPosts.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                return post;
            }
            return null;
        }

        public List<BlogPosts> GetAll()
        {
           return db.BlogPosts.ToList();
        }

        public bool Update(BlogPosts entity)
        {
            throw new NotImplementedException();
        }
    }
}