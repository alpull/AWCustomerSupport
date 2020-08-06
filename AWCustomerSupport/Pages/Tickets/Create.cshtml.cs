using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWCustomerSupport.Pages.Tickets {

    public class CreateModel : PageModel {

        private readonly TicketContext _context;

        public CreateModel(TicketContext context) {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public IActionResult OnGet() {
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) return Page();

            _context.Ticket.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }

}