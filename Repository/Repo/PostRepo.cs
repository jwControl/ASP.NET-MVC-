using Repository.IRepo;
using Repository.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Repo
{
    public class PostRepo:IPostsRepo
    {
        private readonly IBlogContext _db;

        //private BlogContext db = new BlogContext(); nie tworze juz instancji klasy kontekstu bo robi to za mnie IoC
        public PostRepo(IBlogContext db)
        {
            _db = db;
        }

        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
        }

        public Post GetPostById(int id)
        {
            Post post = _db.Posts.Find(id);
            return post;
        }
        //public IQueryable<Post> GetPostsWithUserName()
        //{
        //    //db.Database.Log = message => Trace.WriteLine(message);

        //    return _db.Posts.AsNoTracking();
        //}

        public IQueryable<Post> GetPosts()
        {
            //db.Database.Log = message => Trace.WriteLine(message);
            var posts = _db.Posts.Include(x => x.User);
            return posts;
        }

        public void RemovePost(int id)
        {
            Post post = _db.Posts.Find(id);
            _db.Posts.Remove(post);
   
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }
    }
}