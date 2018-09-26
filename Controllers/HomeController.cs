using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcomStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcomStore.Controllers
{
    public class HomeController : Controller
    {
        private EcomContext _eContext;
        public HomeController(EcomContext context)
        {
            _eContext = context;
        }
        private Customer ActiveUser 
        {
            get 
            {
                return _eContext.customers.Where(u => u.customer_id == HttpContext.Session.GetInt32("customer_id")).FirstOrDefault();
            }
        }
        [HttpGet("")]
        public IActionResult Register()
        {
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpPost("registeruser")]
        public IActionResult RegisterUser(RegisterUser newuser)
        {
            Customer CheckEmail = _eContext.customers
                .Where(u => u.email == newuser.email)
                .SingleOrDefault();

            if(CheckEmail != null)
            {
                ViewBag.errors = "That email already exists";
                return RedirectToAction("Register");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<RegisterUser> Hasher = new PasswordHasher<RegisterUser>();
                Customer newUser = new Customer
                {
                    customer_id = newuser.customer_id,
                    first_name = newuser.first_name,
                    last_name = newuser.last_name,
                    email = newuser.email,
                    address = newuser.address,
                    city = newuser.city,
                    state = newuser.state,
                    zip = newuser.zip,
                    phone = newuser.phone,
                    password = Hasher.HashPassword(newuser, newuser.password)
                  };
                _eContext.Add(newUser);
                _eContext.SaveChanges();
                ViewBag.success = "Successfully registered";
                return RedirectToAction("Login");
            }
            else
            {
                return View("Register");
            }
        }

        [HttpPost("loginuser")]
        public IActionResult LoginUser(LoginUser loginUser) 
        {
            Customer CheckEmail = _eContext.customers
                .SingleOrDefault(u => u.email == loginUser.email);
            if(CheckEmail != null)
            {
                var Hasher = new PasswordHasher<Customer>();
                if(0 != Hasher.VerifyHashedPassword(CheckEmail, CheckEmail.password, loginUser.password))
                {
                    HttpContext.Session.SetInt32("customer_id", CheckEmail.customer_id);
                    HttpContext.Session.SetString("first_name", CheckEmail.first_name);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.errors = "Incorrect Password";
                    return View("Register");
                }
            }
            else
            {
                ViewBag.errors = "Email not registered";
                return View("Register");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

 //*************************************************************************************Dashboard******************************************************************* */

            [HttpGet("Dashboard")]
            public IActionResult Dashboard()
            {
                if(ActiveUser == null)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.user = ActiveUser;
                return View();
            }


        //*************************************************************************************Products******************************************************************* */


        [HttpGet("Products")]
        public IActionResult Products()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<Product> products = _eContext.products.ToList();
            ViewBag.user = ActiveUser;
            ViewBag.products = products;
            return View();
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product item)
        {
            if(ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    name = item.name,
                    short_desc = item.short_desc,
                    desc = item.desc,
                    image = item.image,
                    price = item.price,
                    weight = item.weight,
                    qty = item.qty
                };
                _eContext.products.Add(newProduct);
                _eContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return View("Products");
        }

        [HttpGet("EditProduct/{product_id}")]
        public IActionResult EditProduct(int product_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Product product = _eContext.products.Where(p => p.product_id == product_id).SingleOrDefault();
            ViewBag.product = product;
            ViewBag.user = ActiveUser;
            return View();
        }

        [Route("{product_id}/ProcessEditProduct")]
        public IActionResult ProcessEditProduct(int product_id, string name, string short_desc, string desc, string image, double price, double weight, int qty)
        {
            Product product = _eContext.products.Where(p => p.product_id == product_id).SingleOrDefault();
            product.name = name;
            product.short_desc = short_desc;
            product.desc = desc;
            product.image = image;
            product.price = price;
            product.weight = weight;
            product.qty = qty;
            _eContext.SaveChanges();
            return RedirectToAction("Products");
        }

        [HttpGet("DeleteProduct/{product_id}")]
        public IActionResult DeleteProduct(int product_id)
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login");
            }
            Product product = _eContext.products.Where(p => p.product_id == product_id).SingleOrDefault();
            _eContext.products.Remove(product);
            _eContext.SaveChanges();
            return RedirectToAction("Products");
        }

        //*************************************************************************************Orders******************************************************************* */


        [HttpGet("Orders")]
        public IActionResult Orders()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<Customer> customers = _eContext.customers.ToList();
            List<Product> products = _eContext.products.ToList();
            List<Order> orders = _eContext.orders
                .Include(p => p.Products)
                .Include(c => c.Customer)
                .ToList();
            ViewBag.orders = orders;
            ViewBag.customers = customers;
            ViewBag.products = products;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(int customer_id, int product_id, int order_qty)
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login");
            }
            if(ModelState.IsValid)
            {
                Order newOrder = new Order
                {
                    product_id = product_id,
                    order_qty = order_qty,
                    customer_id = customer_id
                };
                _eContext.orders.Add(newOrder);
                _eContext.SaveChanges();
                return RedirectToAction("Orders");
            }
            return View("Orders");
        }

        //*************************************************************************************Customers******************************************************************* */

        [HttpGet("Customers")]
        public IActionResult Customers()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<Customer> customers = _eContext.customers.Include(o => o.Orders).ToList();
            ViewBag.customers = customers;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("EditUser/{customer_id}")]
        public IActionResult EditUser(int customer_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Customer customer = _eContext.customers.Where(c => c.customer_id == customer_id).SingleOrDefault();
            ViewBag.customer = customer;
            ViewBag.user = ActiveUser;
            return View();
        }
        [Route("{customer_id}/ProcessEditUser")]
        public IActionResult ProcessEditUser(int customer_id, string first_name, string last_name, string address, string city, string state, string zip, string phone, string email)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Customer customer = _eContext.customers.Where(c => c.customer_id == customer_id).SingleOrDefault();
            customer.first_name = first_name;
            customer.last_name = last_name;
            customer.address = address;
            customer.city = city;
            customer.state = state;
            customer.zip = zip;
            customer.phone = phone;
            customer.email = email;
            _eContext.SaveChanges();
            return RedirectToAction("Customers");
        }

        [HttpGet("DeleteCustomer/{customer_id}")]
        public IActionResult DeleteCustomer(int customer_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Customer customer = _eContext.customers.Where(c => c.customer_id == customer_id).SingleOrDefault();
            _eContext.customers.Remove(customer);
            _eContext.SaveChanges();
            return RedirectToAction("Customers");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
