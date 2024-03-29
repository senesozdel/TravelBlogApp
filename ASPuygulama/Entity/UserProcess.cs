using ASPuygulama.Entity.Interfaces;
using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPuygulama.Entity
{
    public class UserProcess : ICrud<User>
    {
        Datacontext db = new Datacontext();
        public string Add(User entity)
        {
            string output = " ";
           var user = db.Users.FirstOrDefault(x=>x.Id == entity.Id);

            if(user  == null)
            {
                db.Users.Add(entity);
                output = entity.Name + "eklendi";
            }
            else
            {
                output = entity.Name + "mevcut. Başka kullanıcı adı seçiniz";
            }
            return output;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            var user = db.Users.FirstOrDefault(x=>x.Id==id);

            if(user != null)
            {
                return user;
            }
            return null;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}