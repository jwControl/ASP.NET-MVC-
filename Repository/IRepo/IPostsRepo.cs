using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IPostsRepo
    {
        IQueryable<Post> GetPosts();
        Post GetPostById(int id);
        void RemovePost(int id);
        void SaveChanges();
        void AddPost(Post post);
        void UpdatePost(Post post);
    }
}
