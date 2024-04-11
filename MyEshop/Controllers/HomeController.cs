using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.Diagnostics;
using System.Security.Claims;
using ZarinpalSandbox;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyEshopContext _context;
        //private static Cart _cart = new Cart();
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
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
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

        [Authorize]
        public IActionResult AddToCart(int itemId)
        //public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);
            if (product != null)
            {
                //// New Method add to cart - DataBase
                ///

                int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

                var order = _context.Orders.FirstOrDefault(o => o.UserId == UserId && o.IsFinaly == false);

                if (order != null)
                {
                    var orderDetail =
                        _context.OrderDetails.FirstOrDefault(d =>
                        d.ProductId == product.Id && d.OrderId == order.OrderId);

                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;
                    }
                    else
                    {
                        _context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Count = 1,
                            Price = product.Item.Price
                        });

                    }

                }
                else
                {
                    order = new Order()
                    {
                        UserId = UserId,
                        CreateDate = DateTime.Now,
                        IsFinaly = false
                    };

                    _context.Orders.Add(order);
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        Order = order,
                        ProductId = product.Id,
                        Price = product.Item.Price,
                        Count = 1
                    });


                }

                _context.SaveChanges();


                //// Old Method add to cart
                //var cartItem = new CartItem()
                //{
                //    Item = product.Item,
                //    Quantity = 1
                //};
                //_cart.addItem(cartItem);

            }
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult ShowCart()
        {
            ///Old Method
            //var cartVM = new CartViewModel() {
            //    CartItems = _cart.CartItems,
            //    OrderTotal = _cart.CartItems.Sum(o => o.getTotalPrice())
            //};

            //return View(cartVM);



            ////New Method
            ///
            int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            var order = _context.Orders.Where(o => o.UserId == UserId && !o.IsFinaly)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefault();


            return View(order);
            ;
        }

        //public IActionResult RemoveCart(int itemId) 
        public IActionResult RemoveCart(int detailId)
        {
            //OldMethod
            //_cart.removeItem(itemId);
            //return RedirectToAction("ShowCart");

            ///New Method
            ///
            int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var orderDetail = _context.OrderDetails.Find(detailId);

            if (orderDetail != null)
            {
                if (orderDetail.Count <= 1)
                {
                    _context.Remove(orderDetail);
                }
                else
                {
                    orderDetail.Count -= 1;
                    _context.Update(orderDetail);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize]
        public IActionResult Payment(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

            if (order == null)            
                return NotFound();

            var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price * d.Count));

            return null;
        }
    }
}