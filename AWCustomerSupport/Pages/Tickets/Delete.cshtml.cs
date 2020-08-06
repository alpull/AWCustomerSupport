using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class DeleteModel : PageModel {

        private readonly TicketContext _context;

        public DeleteModel(TicketContext context) {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Ticket.FirstOrDefaultAsync(m => m.ID == id);

            if (Ticket == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Ticket.FindAsync(id);

            if (Ticket != null) {
                _context.Ticket.Remove(Ticket);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }

}