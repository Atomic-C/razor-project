using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id) // Nullable integer, in case we are Creating, because ID will not exist...
        {
            Book = new Book();
            if (id == null)
            {   // Create
                return Page();
            }

                Book = await _db.Book.FirstOrDefaultAsync(u=>u.Id ==id); // Read more on lambda expressions please
                //Book = await _db.Book.FindAsync(id);
            
            if (Book == null)
            {   // Update
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                if (Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book); //  Here we do Update when we want to update EVERY PROPERTY of the book. Or like in edit page, we can edit properties individually.
                }
                await _db.SaveChangesAsync(); // Now it's saved to the database. Only thing I don't get is this asyncronous stuff.
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
