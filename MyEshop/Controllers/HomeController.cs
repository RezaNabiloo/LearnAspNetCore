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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}