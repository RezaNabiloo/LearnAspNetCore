using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Componetns
{
    public class ProductGroupsComponent : ViewComponent
    {
        private MyEshopContext _context;

        public ProductGroupsComponent(MyEshopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _context.Categories.Select(c => new ShowGroupViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = c.CategoryToProducts.Count(p => p.CategoryId == c.Id)
            }).ToList();
            return View("/Views/Components/ProductGroupsComponent.cshtml", data);
        }
    }
}
