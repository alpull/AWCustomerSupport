using System.Linq;
using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class EditModel : PageModel {

        private readonly AppDbContext _context;

        public EditModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Tickets.FindAsync(id);

            if (Ticket == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id) {
            var ticketToUpdate = await _context.Tickets.FindAsync(id);

            if (ticketToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Ticket>(ticketToUpdate, "ticket",
                t => t.Name,
                t => t.Description,
                t => t.Deadline)) {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool TicketExists(int id) {
            return _context.Tickets.Any(e => e.Id == id);
        }

    }

}
