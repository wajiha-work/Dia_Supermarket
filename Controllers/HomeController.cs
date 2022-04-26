using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Dia_Supermarket.Models;
using System.Web.Security;

namespace Dia_Supermarket.Controllers
{
    public class HomeController : Controller
    {
        db_dia_supermarketEntities db = new db_dia_supermarketEntities();

        // GET: Home
        public ActionResult Index()
        {
            var products = db.tb_Products.OrderByDescending(x => x.updated_at).Take(8).ToList();

            return View(products);
        }

        /*
            @page - tells how many pages are made for your products
        */
        public ActionResult Shop(int? page, int ? cat_filter = 0)
        {
            Products_with_Filter shop_object = new Products_with_Filter();

            // set params
            int pageSize = 8    ;
            int pageNumber = (page ?? 1); // if no page number is passed in param then set it to 1

            //create list
            if (cat_filter == 0)
            {
                shop_object.ProductsList = db.tb_Products.OrderByDescending(x => x.updated_at).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                shop_object.ProductsList = db.tb_Products.Where(x => x.category_id == cat_filter).OrderByDescending(x => x.updated_at).ToPagedList(pageNumber, pageSize);
            }
            

            ViewBag.Categories = db.tb_Categories.OrderBy(x => x.inserted_at).ToList();

            return View(shop_object);
        }

        public ActionResult SingleProduct(int ? id)
        {
            if (id == null)
            {
                return RedirectToAction("Shop", "Home");
            }
            var product = db.tb_Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Shop", "Home");
            }


            // how much sold
            /*
                SELECT SUM(OS.quantity) 
	                FROM tb_Orders_Summary OS inner join tb_Orders O
		                ON o.order_id = os.order_id
	                WHERE OS.product_id = 1011 and o.order_status = 'delivered';
             */
            ViewBag.Sold = (from order in db.tb_Orders join order_summ in db.tb_Orders_Summary on order.order_id equals order_summ.order_id where (order_summ.product_id == id && order.order_status.Equals("delivered")) select new { qty = order_summ.quantity }).ToList().Sum(x => x.qty) ?? 0;


            // avg product rating
            /*
                SELECT AVG(rating) FROM tb_Products_Rating WHERE product_id = 1011;
             */
            ViewBag.AvgRating = (decimal ?)(db.tb_Products_Rating.Where(x => x.product_id == id).Average(x => x.rating));
            ViewBag.AvgRating = ViewBag.AvgRating != null ? Math.Round(ViewBag.AvgRating, 1) : 0;


            // how much ratings
            /*
                SELECT COUNT(rating) FROM tb_Products_Rating WHERE product_id = 1011;
             */
            ViewBag.Ratings = db.tb_Products_Rating.Where(x => x.product_id == id).Select(x => x.rating).Count();


            // related products list by same cat but expect this very product - only 4
            ViewBag.RelatedProducts = db.tb_Products.Where(x => x.category_id == product.category_id && x.product_id != product.product_id).OrderByDescending(x => x.updated_at).Take(4).ToList();

            return View(product);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login_form)
        {
            if (ModelState.IsValid)
            {
                var user = db.tb_Users.Where(a => a.email_address.Equals(login_form.email_address) && a.user_password.Equals(login_form.password)).FirstOrDefault();

                if (user != null)
                {
                    Session["user_id"] = user.user_id.ToString();
                    Session["user_name"] = user.first_name.ToString() + " " + user.last_name.ToString();

                    FormsAuthentication.SetAuthCookie(user.email_address, false); // form fields saved in browser are stored in cookies

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // in case of error
                    ModelState.AddModelError("InvalidLoginError", "Invalid email or password");
                }
            }
            return View(login_form);
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(tb_Users user)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.tb_Users.Any(x => x.email_address == user.email_address);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("email_address", "Email already exists, please enter another email");
                    return View(user);
                }

                user.join_date = DateTime.Now;
                user.updated_at = DateTime.Now;

                db.tb_Users.Add(user);
                db.SaveChanges();
                TempData["success"] = "Your account has been created!";

                return RedirectToAction("Login", "Home");
            }

            return View(user);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult ReturnsAndExchange()
        {
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult ShippingInformation()
        {
            return View();
        }

        public ActionResult AddToCart()
        {
            return View();
        }

        public ActionResult DisplayCart()
        {
            return View();
        }
    }
}