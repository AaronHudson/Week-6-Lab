using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitter.Models;

namespace Twitter.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Post post)
        {
            string ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            post.PublishedOn = DateTime.Now;
            post.Publisher = TwitterDB.Users.SingleOrDefault(x => x.Id == ID);
            TwitterDB.Posts.Add(post);
            TwitterDB.Users.Find(ID).Posts.Add(TwitterDB.Posts.Find(post.Id));
            TwitterDB.SaveChanges();
            return RedirectToAction("StartPage", "Twitter");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
        [HttpPost]
        public ActionResult Delete(Post post)
        {
            TwitterDB.Posts.Attach(post);
            TwitterDB.Posts.Remove(post);
            TwitterDB.SaveChanges();

            return RedirectToAction("StartPage", "Twitter");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            Post oldPost = TwitterDB.Posts.FirstOrDefault(x => x.Id == post.Id);
            post.PublishedOn = oldPost.PublishedOn;
            post.Publisher = oldPost.Publisher;
            TwitterDB.Posts.Remove(oldPost);
            TwitterDB.Posts.Add(post);
            TwitterDB.SaveChanges();
            return RedirectToAction("StartPage", "Twitter");
        }

        public ActionResult Details(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
    }
}