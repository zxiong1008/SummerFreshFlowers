using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SummerFreshFlowers.Models;
using System.IO;
using PagedList;

namespace SummerFreshFlowers.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogsController : Controller
    {
		int CONST_PAGE = 6; //const number set
		private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs/
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
			int pageSize = CONST_PAGE; //the number of posts you want to display per page for pageList
			int pageNumber = (page ?? 1);
			return View(db.Blogs.ToList().OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Index(string searchStr, int? page)
		{
			var listPosts = db.Blogs.AsQueryable();
			listPosts = listPosts.Where(p => p.Title.Contains(searchStr) ||
										   p.Body.Contains(searchStr) ||
										   p.Comments.Any(c => c.Body.Contains(searchStr) ||
															  c.Author.FirstName.Contains(searchStr) ||
															  c.Author.LastName.Contains(searchStr) ||
															  c.Author.DisplayName.Contains(searchStr) ||
															  c.Author.Email.Contains(searchStr)));

			int pageSize = CONST_PAGE; //the number of posts you want to display per page for pageList
			int pageNumber = (page ?? 1);

			ViewBag.searchStr = searchStr;

			return View(listPosts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
		}

		// GET: Blogs/Details/5
		[AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Created,Updated,Media,Published,Body")] Blog blog, HttpPostedFileBase image)
        {
            blog.Created = new DateTimeOffset(DateTime.Now);
            blog.Updated = new DateTimeOffset(DateTime.Now);

            if (ModelState.IsValid)
            {
				if (ImageValidator.IsWebFriendlyImage(image))
				{
					var fileName = Path.GetFileName(image.FileName);
					image.SaveAs(Path.Combine(Server.MapPath("~/images/blog/"), fileName));
					blog.Media = "~/images/blog/" + fileName;
				}
				blog.Created = new DateTimeOffset(DateTime.Now);

				db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Created,Updated,Media,Published,Body")] Blog blog, HttpPostedFileBase image)
        {
            blog.Updated = new DateTimeOffset(DateTime.Now);

            if (ModelState.IsValid)
            {
				if (ImageValidator.IsWebFriendlyImage(image))
				{
					var fileName = Path.GetFileName(image.FileName);
					image.SaveAs(Path.Combine(Server.MapPath("~/images/blog/"), fileName));
					blog.Media = "~/images/blog/" + fileName;
				}

				db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
