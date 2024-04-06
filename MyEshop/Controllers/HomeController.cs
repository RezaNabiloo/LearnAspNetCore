using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.Diagnostics;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyEshopContext _context;
        private static Cart _cart= new Cart();
        public HomeController(ILogger<HomeController> logger, MyEshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Products.ToList();
            return View(data);
        }
        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p=>p.Item)
                .SingleOrDefault(p=>p.Id==id);

            if (product==null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p=>p.Id==id)
                .SelectMany(c=>c.CategoryToProducts)
                .Select(ca=>ca.Category)
                .ToList();


            var data = new DetailsViewModel { Product = product, Categories = categories };
            return View(data);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }


        public IActionResult AddToCart(int itemId) 
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);
            if (product != null)
            {
                var cartItem = new CartItem()
                {
                    Item = product.Item,
                    Quantity = 1
                };
                _cart.addItem(cartItem);
                
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart() 
        {
            var cartVM = new CartViewModel() {
                CartItems = _cart.CartItems,
                OrderTotal = _cart.CartItems.Sum(o => o.getTotalPrice())
            };

            return View(cartVM);
;        }

        public IActionResult RemoveCart(int itemId) 
        {
            _cart.removeItem(itemId);
            return RedirectToAction("ShowCart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}