using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant_App.Models;
using static Restaurant_App.FilterConfig;

namespace Restaurant_App.Controllers
{
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();
        // private IQueryable<Order> userOrder;

        // GET: Users
        [NoDirectAccess]
        public ActionResult Index()
        {

            return View();
        }

        [NoDirectAccess]
        public ActionResult IndexCustomer()
        {

            return View();
        }

        [NoDirectAccess]
        public ActionResult AlreadyOrdered()
        {
            var userID = Int32.Parse(Session["Id"].ToString());
            var user = db.Users.Find(userID);
            ViewBag.flag = user.isAdmin;
            return View();
        }

        [NoDirectAccess]
        public ActionResult Orderedsuccessfully()
        {

            var userID = Int32.Parse(Session["Id"].ToString());
            var user = db.Users.Find(userID);
            ViewBag.flag = user.isAdmin;
            return View();
        }


        [NoDirectAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "Name,Day")] Meal meal)
        {

            if (ModelState.IsValid)
            {
                meal.Date = DateTime.Now.Date;
                meal.UserId = Int32.Parse(Session["Id"].ToString());
                db.Meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal);
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();

            return View(objLoginModel);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Users.Where(m => m.Email == objLoginModel.Email && m.Password == objLoginModel.Password).FirstOrDefault();

                if (admin == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    //return ViewBag.Message;
                }
                if (admin.isAdmin == false)
                {
                    //ModelState.AddModelError("Error", "Email and password isn't matching");
                    //return View();
                    Session["Id"] = admin.Id;
                    return RedirectToAction("SelectFood", "Users");
                }
                else
                {
                    Session["Id"] = admin.Id;
                    return RedirectToAction("Index", "Users");
                }
            }
            return View();
        }


        private static List<SelectListItem> PopulateFood()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                DateTime today = DateTime.Now.Date;
                string query = " SELECT Id,Name FROM Meal where Date = CAST( GETDATE() AS Date ) ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Value = sdr["Id"].ToString(),
                                Text = sdr["Name"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }

        [NoDirectAccess]
        public ActionResult SelectFood()
        {
            FoodModel meal = new FoodModel();
            meal.Foods = PopulateFood();
            return View(meal);
        }

        [NoDirectAccess]
        [HttpPost]
        public ActionResult SelectFood(FoodModel s)
        {
            
            s.Foods = PopulateFood();
            var selectedItem = s.Foods.Find(p => p.Value == s.FoodId.ToString());
            int selectedItemId = Int32.Parse(selectedItem.Value);

            var userID = Int32.Parse(Session["Id"].ToString());
            var dateTime = DateTime.Now.Date;
            string dateString = dateTime.ToString("MM/dd/yyyy");
            
            var userOrder = db.Orders.Where(m=> m.Date == dateTime && m.UserId == userID).ToList();
           
            if (userOrder.Count()!=0)
            {
                return RedirectToAction("AlreadyOrdered", "Users");
            }
            else 
            {

                Order order = new Order();
                order.MealName = selectedItem.Text;
                order.UserId = userID;
                order.Date = DateTime.Now.Date;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Orderedsuccessfully", "Users");
               
            }
            
           

   }
    }

}

