using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TravelTrip.Models.Classes
{
    public class Context : DbContext
    {
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<New> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //modelBuilder.Entity<New>()
            //    .HasRequired<Category>(s => s.newCategory)
            //    .WithMany(g => g.News)
            //    .HasForeignKey<int>(s => s.newCategoryId);


            //modelBuilder.Entity<New>()
            //    .HasRequired<Editor>(s => s.newEditor)
            //    .WithMany(g => g.News)
            //    .HasForeignKey<int>(s => s.newEditorId);

            // modelBuilder.Entity<Category>()
            //.HasMany<New>(g => g.News)
            //.WithRequired(s => s.newCategory)
            // .HasForeignKey<int>(s => s.newCategoryId);

            // modelBuilder.Entity<Editor>()
            //.HasMany<New>(g => g.News)
            //.WithRequired(s => s.newEditor)
            // .HasForeignKey<int>(s => s.newEditorId);

            modelBuilder.Entity<New>()
                .HasRequired<Category>(s => s.newCategory)
                .WithMany(g => g.News)
               .HasForeignKey<int>(s => s.newCategoryId);

            modelBuilder.Entity<New>()
               .HasRequired<Editor>(s => s.newEditor)
               .WithMany(g => g.News)
               .HasForeignKey<int>(s => s.newEditorId);

            modelBuilder.Entity<Category>()
             .HasRequired<MainCategory>(s => s.MainCategory)
             .WithMany(g => g.Categories)
             .HasForeignKey<int>(s => s.mainCategoryID);

            modelBuilder.Entity<New>()
             .HasRequired<MainCategory>(s => s.newMainCategory)
             .WithMany(g => g.News)
             .HasForeignKey<int>(s => s.newMainCategoryId)
             .WillCascadeOnDelete(false);


        }

    }
}
