using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dia_Supermarket.Common;

namespace Dia_Supermarket.Controllers
{
    public class UserController : Controller
    {
        [AuthorizeUser]
        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["user_name"] = null;

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUser]
        public ActionResult OrdersHistory(int? id)
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult Wishlist(int? id)
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult EditProfile(int? id)
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult Checkout(int? id)
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult PlaceOrder(int? id)
        {
            return View();
        }
    }
}