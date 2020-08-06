using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class DetailsModel : PageModel {

        private readonly TicketContext _context;

        public DetailsModel(TicketContext context) {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Ticket.FirstOrDefaultAsync(m => m.ID == id);

            if (Ticket == null) return NotFound();
            return Page();
        }

    }

}