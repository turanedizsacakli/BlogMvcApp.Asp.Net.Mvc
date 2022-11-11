using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category() { CategoryName="C#" },
                new Category() { CategoryName="Asp.Net MVC" },
                new Category() { CategoryName="Asp.Net WebForm" },
                new Category() { CategoryName="Windows Form" },
                };

            foreach (var category in categories)
            {
                context.categories.Add(category);
            }
            context.SaveChanges();

            List<Blog> blogs = new List<Blog>()
        {
            new Blog(){Title="About C#",Explanation="A Page About C#...",Photo="photo.jpg", CreatedDate=DateTime.Now.AddDays(-10),Content="Programming...",Approval=true,AddedHomePage=true, CategoryId=1},
            new Blog(){Title="About Asp.Net MVC",Explanation="A Page About Asp.Net MVC...",Photo="photo.jpg", CreatedDate=DateTime.Now.AddDays(-10),Content="Programming...",Approval=true,AddedHomePage=true, CategoryId=2},
            new Blog(){Title="About Asp.Net WebForm",Explanation="A Page About Asp.Net WebForm...",Photo="photo.jpg", CreatedDate=DateTime.Now.AddDays(-10),Content="Programming...",Approval=true,AddedHomePage=true, CategoryId=3},
            new Blog(){Title="About Windows Form",Explanation="A Page About Windows Form...",Photo="photo4.jpg", CreatedDate=DateTime.Now.AddDays(-10),Content="Programming...",Approval=true,AddedHomePage=false, CategoryId=4},
            new Blog(){Title="About C#",Explanation="A Page About C#...",Photo="photo.jpg", CreatedDate=DateTime.Now.AddDays(-10),Content="Programming...",Approval=false,AddedHomePage=true, CategoryId=1},

        };

            foreach (var item in blogs)
            {
                context.blogs.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }

    }
}