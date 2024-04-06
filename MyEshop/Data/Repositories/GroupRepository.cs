using MyEshop.Models;

namespace MyEshop.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private MyEshopContext _context;

        public GroupRepository(MyEshopContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            var data = _context.Categories.Select(c => new ShowGroupViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = c.CategoryToProducts.Count(p => p.CategoryId == c.Id)
            }).ToList();

            return data;
        }
    }
}
