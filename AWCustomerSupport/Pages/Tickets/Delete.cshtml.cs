using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class DeleteModel : PageModel {

        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Tickets.FindAsync(id);

            if (Ticket != null) {
                _context.Tickets.Remove(Ticket);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }

}
