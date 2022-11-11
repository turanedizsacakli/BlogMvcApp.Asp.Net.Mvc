using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogContext:DbContext 
    {
        public BlogContext()
        {
            Database.SetInitializer(new BlogInitializer()); 
        }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}