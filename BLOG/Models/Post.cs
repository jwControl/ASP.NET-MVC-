using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLOG.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        [Display(Name = "Title")]
        [MaxLength(72)]
        public string Title { get; set; }
        
        public string Content { get; set; }
        [Display(Name = "PostingDate")]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostingDate { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
    }
}