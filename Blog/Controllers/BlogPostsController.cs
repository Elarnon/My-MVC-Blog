using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{ 
    public class BlogPostsController : Controller
    {
        private BlogPostDBContext db = new BlogPostDBContext();
        private CommentDBContext dbComments = new CommentDBContext();

        //
        // GET: /BlogPosts/

        public ViewResult Index()
        {
            return View(db.BlogPosts.ToList());
        }

        //
        // GET: /BlogPosts/Details/5

        public ViewResult Details(int id)
        {
            BlogPost blogpost = db.BlogPosts.Find(id);
            List<Comment> comments = (from comment in dbComments.Comments
                               where comment.BlogPostID == id
                               orderby comment.ParentID, comment.ID
                               select comment).ToList();
            System.Collections.Generic.KeyValuePair<BlogPost, List<Comment>> pair =
                new KeyValuePair<BlogPost, List<Comment>>(blogpost, comments);
            return View(pair);
        }

        //
        // GET: /BlogPosts/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /BlogPosts/Create

        [HttpPost]
        public ActionResult Create(BlogPost blogpost)
        {
            if (ModelState.IsValid)
            {
                blogpost.PostDate = DateTime.Now;
                blogpost.UpDate = DateTime.Now;
                db.BlogPosts.Add(blogpost);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(blogpost);
        }
        
        //
        // GET: /BlogPosts/Edit/5
 
        public ActionResult Edit(int id)
        {
            BlogPost blogpost = db.BlogPosts.Find(id);
            return View(blogpost);
        }

        //
        // POST: /BlogPosts/Edit/5

        [HttpPost]
        public ActionResult Edit(BlogPost blogpost)
        {
            if (ModelState.IsValid)
            {
                blogpost.UpDate = DateTime.Now;
                db.Entry(blogpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogpost);
        }

        //
        // GET: /BlogPosts/Delete/5
 
        public ActionResult Delete(int id)
        {
            BlogPost blogpost = db.BlogPosts.Find(id);
            return View(blogpost);
        }

        //
        // POST: /BlogPosts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            IEnumerable<Comment> comments = from comment in dbComments.Comments
                               where comment.BlogPostID == id
                               select comment;
            foreach (Comment comment in comments)
            {
                dbComments.Comments.Remove(comment);
            }
            BlogPost blogpost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogpost);
            db.SaveChanges();
            // TODO: dbComments changes are not staged!!!
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}