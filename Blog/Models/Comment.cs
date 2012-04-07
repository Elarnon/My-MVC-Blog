using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Blog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int BlogPostID { get; set; }
        public int ParentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
    public class CommentDBContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
    }
}