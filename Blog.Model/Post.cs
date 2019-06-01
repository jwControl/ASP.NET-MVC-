using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
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
        public string IdUsera { get; set; } 
        public string PostUserName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public User User { get; set; }
    }
}