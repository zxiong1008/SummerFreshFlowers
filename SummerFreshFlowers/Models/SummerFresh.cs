using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SummerFreshFlowers.Models
{
    public class SummerFresh
    {

    }
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string Media { get; set; }
        public bool Published { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }

        public virtual Blog Blogs { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
    public class NewsFeed
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Media { get; set; }
        public bool Published { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
    public class Gallery
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Media { get; set; }
        public bool Published { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
}