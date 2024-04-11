using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop.Data;
using MyEshop.Models;
using System.Runtime.CompilerServices;

namespace MyEshop.Pages.Admin
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddEditProductViewModel product { get; set; }
        private MyEshopContext _context;

        public AddModel(MyEshopContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var item = new Item
            {
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };

            _context.Add(item);

            var prod = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Item = item,

            };
            _context.Add(prod);

            _context.SaveChanges();

            if (product.Picture?.Length>0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    prod.Id + Path.GetExtension(product.Picture.FileName)
                    );
                using (var stream = new FileStream(filePath, FileMode.Create ))
                {
                    product.Picture.CopyTo(stream);
                }

            }

            return RedirectToPage("Index");
        }


    }
}
