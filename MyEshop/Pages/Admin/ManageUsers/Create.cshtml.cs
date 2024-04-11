using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin.ManageUsers
{
    public class CreateModel : PageModel
    {
        private readonly MyEshop.Data.MyEshopContext _context;

        public CreateModel(MyEshop.Data.MyEshopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var errs = ModelState.Where(x => x.Value.Errors.Count > 0
                ).Select(
                  x => new { x.Key, x.Value.Errors }
                );

            if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
