using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dia_Supermarket.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Logout()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Session["user_id"] = null;
            Session["user_name"] = null;

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrdersHistory(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult Wishlist(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult EditProfile(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult Checkout(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult PlaceOrder(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}