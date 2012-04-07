using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "You must choose a title.")]
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime UpDate { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You must enter enter some text.")]
        public string Content { get; set; }
    }
    public class BlogPostDBContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}