using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial.Data;
using RazorTutorial.Models;

namespace RazorTutorial.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CustomerDbContext _db;
        public CreateModel(CustomerDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Customer Customer { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customer.Add(Customer);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
