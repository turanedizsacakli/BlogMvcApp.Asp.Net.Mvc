using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult List(int? id,string keyword)
        {
            var blogs = db.blogs
                .Where(i => i.Approval == true)
                .Select(b => new BlogModel
                {
                    Id = b.Id,
                    AddedHomePage = b.AddedHomePage,
                    Approval = b.Approval,
                    Content = b.Content,
                    CreatedDate = b.CreatedDate,
                    Title = b.Title.Length > 100 ? b.Title.Substring(0, 100) + "..." : b.Title,
                    CategoryId = b.CategoryId
                }).AsQueryable();

            if (string.IsNullOrEmpty("keyword")==false)
            {
                blogs=blogs.Where(i=>i.Title.Contains(keyword)||i.Content.Contains(keyword));
            }
            else
            {

            }

            if (id !=null)
            {
                blogs=blogs.Where(i => i.CategoryId == id);
            }
                
            //return View(context.blogs.ToList());
            return View(blogs.ToList());
        }

        // GET: Blog
        public ActionResult Index()
        {
            //to make list line... OrderByDescending(i => i.CreatedDate)
            var blogs = db.blogs.Include(b => b.Category).OrderByDescending(i=>i.CreatedDate);
            return View(blogs.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "CategoryName");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Explanation,Photo,Content,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.CreatedDate = DateTime.Now;
                blog.AddedHomePage = true;
                blog.Approval = true;


                db.blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categories, "Id", "CategoryName", blog.CategoryId);
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "CategoryName", blog.CategoryId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Explanation,Photo,Content,Approval,AddedHomePage,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var entity = db.blogs.Find(blog.Id);
                if (entity!=null)
                {
                    entity.Title = blog.Title;
                    entity.Explanation = blog.Explanation;
                    entity.Photo = blog.Photo;
                    entity.Approval = blog.Approval;
                    entity.AddedHomePage = blog.AddedHomePage;
                    entity.CategoryId = blog.CategoryId;
                    entity.Content = blog.Content; 
                    
                    //db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Blog"] = entity;
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "CategoryName", blog.CategoryId);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.blogs.Find(id);
            db.blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
