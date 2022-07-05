using B2BApp.Data;
using B2BApp.Models;
using B2BApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace B2BApp.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index(string currentFilter, string searchString, int ?page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var empquery = from c in _db.Products select c;
            if(!String.IsNullOrEmpty(searchString))
            {
                empquery = empquery.Where(c=>c.Name.Contains(searchString));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(empquery.ToPagedList(pageNumber, pageSize)); 
        }

        
        
        [HttpGet]
        private bool ProductExists(int id)
        {
            return _db.Products.Any(p => p.Id == id);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET Product Detail Action Method

        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c=>c.ProductTypes).Include(c => c.SpecialTags).FirstOrDefault(c => c.Id == id);
            if(product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Product Detail Action Method
        [HttpPost]
       
        public ActionResult AddToCart(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if(products==null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            product.Quantity++;
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));

        }

        

        //GET Remove Action Method
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }

        //GET Product Cart Action Method

        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if( products==null)
            {
                products = new List<Products>();
            }
            return View(products);
        }

        public IActionResult QtyPlus(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                for (int i = 0; i < products.Count; i++)
                {
                    if(products[i].Id == id)
                    {
                        product = products[i];
                    }
                }
                if (product != null)
                {
                    product.Quantity++;
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult QtyMinus(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Id == id)
                    {
                        product = products[i];
                    }
                }
                if (product.Quantity > 1)
                {
                    product.Quantity--;
                    HttpContext.Session.Set("products", products);
                }
                else
                {
                    return Content("Quantity cannot be less than 1!");
                }
            }
            return RedirectToAction(nameof(Cart));
        }

    }
}
