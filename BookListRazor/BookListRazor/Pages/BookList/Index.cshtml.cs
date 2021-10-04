using System.Collections.Generic;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        // MVC has action methods, Razor Pages has handlers!!!
        public async Task OnGetAsync() // Async will let us run multiple tasks at a time until they're awaiting.
        {
            Books = await _db.Book.ToListAsync(); // We're storing all the books inside the IEnumerable after extracting them from database.
        }
    }
}
