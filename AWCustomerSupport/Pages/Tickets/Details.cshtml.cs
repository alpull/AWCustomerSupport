using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class DetailsModel : PageModel {

        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context) {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null) return NotFound();
            return Page();
        }

    }

}
