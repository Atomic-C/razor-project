using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
            private readonly ApplicationDbContext _db;
            public CreateModel (ApplicationDbContext db)
            {
                _db = db;
            }

            [BindProperty]
            public Book Book { get; set; }

            public void OnGet()
            {

            }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
               await _db.Book.AddAsync(Book); // The book is in a queue, sort of.
               await _db.SaveChangesAsync(); // Now it's saved to the database. Only thing I don't get is this asyncronous stuff.
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
