using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dia_Supermarket.Models;
using System.Data.Entity;
using System.IO;
using Dia_Supermarket.Common;

namespace Dia_Supermarket.Controllers
{
    public class AdminController : Controller
    {
        db_dia_supermarketEntities db = new db_dia_supermarketEntities();


        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            // pending orders
            string[] statuses = { "waiting", "confirmed" };
            ViewBag.PendingOrders = db.tb_Orders.Where(x => statuses.Contains(x.order_status)).Select(x => x.order_id).Count();

            // shipped orders
            ViewBag.ShippedOrders = db.tb_Orders.Where(x => x.order_status.Equals("shipped")).Select(x => x.order_id).Count();

            // completed orders
            ViewBag.CompletedOrders = db.tb_Orders.Where(x => x.order_status.Equals("delivered")).Select(x => x.order_id).Count();

            // total earning
            ViewBag.TotalEarning = db.tb_Orders.Where(x => x.order_status.Equals("delivered")).Select(x => x.sub_total).Sum();



            //best selling products
            /*
                SELECT P.product_name, P.product_image, P.price, P.stock, C.cat_name, SUM(OS.quantity) AS 'SALES'
	                FROM tb_Categories C INNER JOIN tb_Products P 
		                ON P.category_id = C.cat_id
	                INNER JOIN tb_Orders_Summary OS 
		                ON P.product_id = OS.product_id
	                GROUP BY P.product_name, P.product_image, P.price, P.stock, C.cat_name
	                ORDER BY SUM(OS.quantity) DESC;	
             */
            // had to made class top seller products for this to work *meh*
            ViewBag.BestSellers = (from cats in db.tb_Categories
                                   join products in db.tb_Products on cats.cat_id equals products.category_id
                                   join os in db.tb_Orders_Summary on products.product_id equals os.product_id
                                   group os by new { products.product_id, products.product_name, products.product_image, products.price, products.stock, cats.cat_name } into g
                                   orderby g.Sum(x => x.quantity) descending
                                   select new TopSellerProducts
                                   {
                                       product_id = g.Key.product_id,
                                       product_name = g.Key.product_name,
                                       product_image = g.Key.product_image,
                                       price = g.Key.price,
                                       stock = g.Key.stock,
                                       cat_name = g.Key.cat_name,
                                       sales = g.Sum(x => x.quantity)
                                   });

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["admin_id"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(tb_Admins admin)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.tb_Admins.Any(x => x.email_address == admin.email_address);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("email_address", "Email already exists, please enter another email");
                    return View(admin);
                }

                admin.join_date = DateTime.Now;
                admin.updated_at = DateTime.Now;

                db.tb_Admins.Add(admin);
                db.SaveChanges();
                TempData["success"] = "Your account has been created!";

                return RedirectToAction("Login");
            }

            return View(admin);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["admin_id"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login_form)
        {
            if (ModelState.IsValid)
            {
                var admin = db.tb_Admins.Where(a => a.email_address.Equals(login_form.email_address) && a.admin_password.Equals(login_form.password) && a.verified_user == true).FirstOrDefault();

                if (admin != null)
                {
                    Session["admin_id"] = admin.admin_id.ToString();
                    Session["admin_name"] = admin.first_name.ToString() + " " + admin.last_name.ToString();

                    FormsAuthentication.SetAuthCookie(admin.email_address, false); // form fields saved in browser are stored in cookies

                    return RedirectToAction("Index", "Admin");
                }
            }

            // in case of error
            ModelState.AddModelError("InvalidLoginError", "Invalid email or password or perhaps you are not a verified user!");
            return View(login_form);
        }

        public ActionResult Logout()
        {
            if (Session["admin_id"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            Session["admin_id"] = null;
            Session["admin_name"] = null;

            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            int loggen_in_admin_id = Convert.ToInt32(Session["admin_id"]);
            tb_Admins admin = db.tb_Admins.Find(loggen_in_admin_id);
            admin.confirm_password = admin.admin_password;

            return View(admin);
        }


        [HttpPost]
        public ActionResult EditProfile(tb_Admins admin_form)
        {
            if (ModelState.IsValid)
            {
                admin_form.updated_at = DateTime.Now;
                db.Entry(admin_form).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Session["admin_name"] = admin_form.first_name + " " + admin_form.last_name;
                TempData["success"] = "Profile Updated succesfully";
            }

            return View(admin_form);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            // select box of category name
            ViewBag.CategoriesList = new SelectList(db.tb_Categories, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(tb_Products product, HttpPostedFileBase customFile)
        {
            // select box with selected cat id - shows in case of error
            ViewBag.CategoriesList = new SelectList(db.tb_Categories, "cat_id", "cat_name");

            if (ModelState.IsValid)
            {
                var isProductExists = db.tb_Products.Any(x => x.product_name.Equals(product.product_name));
                if (isProductExists)
                {
                    ModelState.AddModelError("product_name", "Product already exists");
                    return View(product);
                }

                var isSKUexists = db.tb_Products.Any(x => x.sku.Equals(product.sku));
                if (isSKUexists)
                {
                    ModelState.AddModelError("sku", "SKU already registered");
                    return View(product);
                }

                Dictionary<string, string> fileResult = UploadImage(customFile);
                if (fileResult.ContainsKey("success")) // image uploaded
                {
                    product.product_image = fileResult["success"];

                    product.inserted_at = DateTime.Now;
                    product.updated_at = DateTime.Now;
                    //product.updated_by_admin = Convert.ToInt32(Session["admin_id"]);

                    db.tb_Products.Add(product);
                    db.SaveChanges();
                    TempData["success"] = "Product added successfully!";
                    return RedirectToAction("ProductsList", "Admin");
                }
                else // image not uploaded
                {
                    TempData["error"] = fileResult["error"];
                    return View(product);
                }
            }
            return View(product);
        }

        public ActionResult ProductsList()
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            var products = db.tb_Products.Include(x => x.tb_Categories);

            return View(products.ToList());
        }

        [HttpGet]
        public ActionResult DeleteProduct(int? id)
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                return RedirectToAction("ProductsList", "Admin");
            }

            tb_Products product = db.tb_Products.Find(id);

            if (product == null)
            {
                TempData["error"] = "No product found!!";
                return RedirectToAction("ProductsList", "Admin");
            }

            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult ConfirmDeleteProduct(int id)
        {
            tb_Products product = db.tb_Products.Find(id);
            DeleteImage(product.product_image);
            db.tb_Products.Remove(product);
            db.SaveChanges();

            TempData["success"] = "Product deleted successfully!";
            return RedirectToAction("ProductsList", "Admin");
        }

        [HttpGet]
        public ActionResult ViewProduct(int? id)
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                return RedirectToAction("ProductsList", "Admin");
            }

            tb_Products product = db.tb_Products.Find(id);

            if (product == null)
            {
                TempData["error"] = "No product found!";
                return RedirectToAction("ProductsList", "Admin");
            }

            return View(product);
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (Session["admin_id"] == null)
            {
               return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                return RedirectToAction("ProductsList", "Admin");
            }

            tb_Products product = db.tb_Products.Find(id);

            if (product == null)
            {
                TempData["error"] = "No product found!";
                return RedirectToAction("ProductsList", "Admin");
            }

            ViewBag.CategoriesList = new SelectList(db.tb_Categories, "cat_id", "cat_name");
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(int? id, tb_Products product_form, HttpPostedFileBase customFile)
        {
            ViewBag.CategoriesList = new SelectList(db.tb_Categories, "cat_id", "cat_name");

            if (ModelState.IsValid)
            {
                // here id must be different 
                var isProductExists = db.tb_Products.Any(x => x.product_name.Equals(product_form.product_name) && x.product_id != product_form.product_id);
                if (isProductExists)
                {
                    ModelState.AddModelError("product_name", "Product already exists");
                    return View(product_form);
                }

                var isSKUexists = db.tb_Products.Any(x => x.sku.Equals(product_form.sku) && x.product_id != product_form.product_id);
                if (isSKUexists)
                {
                    ModelState.AddModelError("sku", "SKU already registered");
                    return View(product_form);
                }

                tb_Products product = db.tb_Products.Find(id);
                if (product == null)
                {
                    TempData["error"] = "No product found!";
                    return RedirectToAction("ProductsList", "Admin");
                }

                // display image preview in case of error or no error
                product_form.product_image = product.product_image;

                // must save details
                product.product_name = product_form.product_name;
                product.sku = product_form.sku;
                product.long_description = product_form.long_description;
                product.price = product_form.price;
                product.stock = product_form.stock;
                product.category_id = product_form.category_id;

                product.updated_at = DateTime.Now;
                //product.updated_by_admin = Convert.ToInt16(Session["admin_id"]);

                if (customFile == null) // if file is not added
                {
                    db.SaveChanges();
                    TempData["success"] = "Product Updated succesfully";
                }
                else // if file is added
                {
                    Dictionary<string, string> fileResult = UploadImage(customFile, product.product_image);

                    if (fileResult.ContainsKey("success"))
                    {
                        product.product_image = fileResult["success"];
                        product_form.product_image = fileResult["success"]; // for preview

                        db.SaveChanges();
                        TempData["success"] = "Product Updated succesfully";
                    }
                    else
                    {
                        TempData["error"] = fileResult["error"];
                    }
                }
            }
            return View(product_form);
        }



        private bool DeleteImage(string filename)
        {
            bool result = false;
            if (isFileALreadyExist(filename))
            {
                try
                {
                    string path = Server.MapPath(filename);
                    FileInfo file = new FileInfo(path);
                    file.Delete();
                    result = true;
                }
                catch (System.IO.IOException ex)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        private bool isFileALreadyExist(string filename)
        {
            string path = Server.MapPath(filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Dictionary<string, string> UploadImage(HttpPostedFileBase file, string productFileName = "")
        {
            Dictionary<string, string> result = new Dictionary<string, string>(); // output variable

            if (file != null && file.ContentLength > 0) // if file IS sent here
            {
                string filename = Path.GetFileName(file.FileName); // get filename
                filename = "/Templates/UploadedFiles/" + filename; // set prefix for our uploads folder

                if (isFileALreadyExist(filename)) // if file sent already exist w.r.t. name
                {
                    if (filename == productFileName) // exist but it is exactly same file choosen which is saved in db so no need to upload new and delete old one - just return
                    {
                        result.Add("success", filename);
                        return result;
                    }
                    else // in any other case where file exists with same name, either add or edi -> give error
                    {
                        result.Add("error", "Another file with same name already exists, please rename your file and upload again!");
                        return result;
                    }
                }

                string extension = Path.GetExtension(file.FileName);

                // not works if extension is jpeg, jgp or png
                if (extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        // set unique file name
                        // string filename = Path.GetFileNameWithoutExtension(file.FileName);
                        // filename += "_" + DateTime.Now.ToString("yymmssfff") + extension;

                        // map with local path w.r.t drive and save image there
                        string path = Server.MapPath(filename);
                        file.SaveAs(path);

                        // in case of edit, delete old file too
                        if (productFileName != "")
                        {
                            DeleteImage(productFileName);
                        }

                        result.Add("success", filename);
                    }
                    catch (Exception ex)
                    {
                        result.Add("error", "Some unknown error occured while uploading the file!");
                    }
                }
                else
                {
                    result.Add("error", "Only JPG,JPEG and PNG formats are acceptable!");
                }
            }
            else
            {
                result.Add("error", "Please Select a File!");
            }
            return result;
        }
    }
}