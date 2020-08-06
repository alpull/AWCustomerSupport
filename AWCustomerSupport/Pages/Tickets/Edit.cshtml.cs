using System.Linq;
using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class EditModel : PageModel {

        private readonly TicketContext _context;

        public EditModel(TicketContext context) {
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Ticket).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!TicketExists(Ticket.ID))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool TicketExists(int id) {
            return _context.Ticket.Any(e => e.ID == id);
        }

    }

}