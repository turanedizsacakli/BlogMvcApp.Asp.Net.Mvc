using BlogMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext context = new BlogContext();


        // GET: Home
        public ActionResult Index()
        {
            var blogs = context.blogs
                .Select(b => new BlogModel
                {
                    Id = b.Id,
                    AddedHomePage = b.AddedHomePage,
                    Approval = b.Approval,
                    Content = b.Content,
                    CreatedDate = b.CreatedDate,
                    Title = b.Title.Length > 100 ? b.Title.Substring(0, 100) + "..." : b.Title,

                })
                .Where(i => i.Approval == true && i.AddedHomePage == true);

            //return View(context.blogs.ToList());
            return View(blogs.ToList());
        }
    }
}