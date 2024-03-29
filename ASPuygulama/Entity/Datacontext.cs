using ASPuygulama.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services.Description;

namespace ASPuygulama.Entity
{
    public class Datacontext:DbContext
    {
        public Datacontext(): base("DbConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<BlogLikes> BlogLikes { get; set; }
        public DbSet<BlogComments> BlogComments { get; set; }
        public DbSet<BlogCityRelations> BlogCityRelations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User-Post ilişkisi
            modelBuilder.Entity<BlogPosts>()
                .HasRequired(p => p.User)
                .WithMany(u => u.BlogPosts)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            // BlogPosts-BlogComments ilişkisi
            modelBuilder.Entity<BlogComments>()
                .HasRequired(c => c.BlogPosts)
                .WithMany(p => p.BlogComments)
                .HasForeignKey(c => c.BlogId).WillCascadeOnDelete(false);
            // BlogComments-User ilişkisi
            modelBuilder.Entity<BlogComments>()
                .HasRequired(c => c.User)
                .WithMany(u => u.BlogComments)
                .HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);

            // BlogPosts-BlogLikes ilişkisi
            modelBuilder.Entity<BlogLikes>()
                .HasRequired(l => l.BlogPosts)
                .WithMany(p => p.BlogLikes)
                .HasForeignKey(l => l.BlogId).WillCascadeOnDelete(false);

            // BlogLikes-User ilişkisi
            modelBuilder.Entity<BlogLikes>()
                .HasRequired(l => l.User)
                .WithMany(u => u.BlogLikes)
                .HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<BlogCityRelations>()
                .HasRequired(bcr => bcr.City)
                .WithMany(c => c.BlogCityRelations)
                .HasForeignKey(bcr => bcr.CityId).WillCascadeOnDelete(false);

            // BlogCityRelations-BlogPosts ilişkisi
            modelBuilder.Entity<BlogCityRelations>()
                .HasRequired(bcr => bcr.BlogPostsId)
                .WithMany(p => p.BlogCityRelations)
                .HasForeignKey(bcr => bcr.BlogId).WillCascadeOnDelete(false);
        }


    }
}