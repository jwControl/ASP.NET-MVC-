using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLOG.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public System.DateTime CommentingDate { get; set; }
        public int IdPost { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }   
        public int Post_Id { get; set; }

        public virtual Post Post { get; set; }
    }
}