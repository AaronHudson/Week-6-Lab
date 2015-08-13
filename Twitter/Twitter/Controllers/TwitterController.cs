using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Twitter.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Twitter.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();

        public ActionResult StartPage()
        {
            string ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            TwitterUser user = TwitterDB.Users.SingleOrDefault(x => x.Id == ID);
            List<Post> postFeed = new List<Post>();
            postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher.Id == user.Id));
            foreach (TwitterUser followee in user.Following.ToList())
            {
                postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher.Id == followee.Id).ToList());
            }
            return View(postFeed);
        }

        //public ActionResult Follow(TwitterUser user)
        //{
        //    TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Following.Add(user);
        //    TwitterDB.SaveChanges();
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Follow(string FolloweeID)
        {
            string FollowerID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            TwitterUser Followed = TwitterDB.Users.SingleOrDefault(x => x.Id == FolloweeID);
            TwitterUser Follower = TwitterDB.Users.SingleOrDefault(x => x.Id == FollowerID);
            Follower = TwitterDB.Users.SingleOrDefault(x => x.Id == Follower.Id);
            Follower.Following.Add(Followed);
            TwitterDB.SaveChanges();
            return PartialView("Unfollow", FollowerID);
        }

        [HttpPost]
        public ActionResult Unfollow(string FolloweeID)
        {
            string FollowerID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            TwitterUser Followed = TwitterDB.Users.SingleOrDefault(x => x.Id == FolloweeID);
            TwitterUser Follower = TwitterDB.Users.SingleOrDefault(x => x.Id == FollowerID);
            Follower = TwitterDB.Users.SingleOrDefault(x => x.Id == Follower.Id);
            Follower.Following.Remove(Followed);
            TwitterDB.SaveChanges();
            return PartialView("Follow", FolloweeID);
        }
    }
}