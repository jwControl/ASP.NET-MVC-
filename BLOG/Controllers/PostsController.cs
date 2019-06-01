using System.Web.Mvc;
using Repository.IRepo;
using System.Net;
using Microsoft.AspNet.Identity;
using System;
using Repository.Models;

namespace BLOG.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsRepo _repo;
        public PostsController(IPostsRepo repo)
        {
            _repo = repo;
        }
        //private BlogContext db = new BlogContext(); zamiast tworzenia nowego obiketu kontekstu chcemy aby kontekst byl oddzielony od kontrolera i robimy obiekt klasy repozytorium
        //PostRepo repo = new PostRepo(); instancje klasy PostRepo robi za nas IoC


        // GET: Posts
        public ActionResult Index()
        {

            //Include(dd => dd.User); jak robimy zapytanie przez include to wylaczamy lazy loading co powoduje zaciaganie wszystkich danych o uzytkowniku
            var posts = _repo.GetPosts(); //kontekt nie sledzi juz zmian

            return View(posts);
        }

        public ActionResult Partial()
        {
            var posts = _repo.GetPosts();
            return PartialView("Index", posts);
        }



        //GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _repo.GetPostById((int)id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //// GET: Posts/Create
        [Authorize] //jezeli uzytkownik nie jest zalogowany to nie bedzie mogl pobrac formmularza do tworzenia postu
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Posts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize] //post moze utworzyc tylko zalogowany uzytkonik
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content")] Post post) //Bind oznacza ze tylko tytul i Kontent bedzie przesylany z przegladarki do serwera bo userId i data chcemy zeby sie automatycznie wygenerowala
        {
            if (ModelState.IsValid) //sprawdzamy czy dane wpisywane w formularz pasuja do tych ktore sa w mode bazy danych (typy i warotsci)
            {
                post.IdUsera= User.Identity.GetUserId();
                post.PostingDate = DateTime.Now;
               
                try
                {
                    _repo.AddPost(post);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(post);
                }
            }

            return View(post);
        }

        //// GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _repo.GetPostById((int)id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else if (post.IdUsera != User.Identity.GetUserId() && !(User.IsInRole("Admin")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(post);
        }

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Title,Content, PostingDate, IdUsera")] Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdatePost(post);
                    _repo.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(post);
                }

            }
            ViewBag.Error = false;
            return View(post);
        }
        [Authorize]
        // GET: Posts/Delete/5
        public ActionResult Delete(int? id, bool ? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _repo.GetPostById((int)id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else if (post.IdUsera != User.Identity.GetUserId() && !(User.IsInRole("Admin")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (error != null)
            {
               
                ViewBag.Error = true;
            }
            return View(post);
        }

        //// POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.RemovePost(id);
            try
            {
                _repo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, error = true });
             }
                    
            return RedirectToAction("Index");


        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
