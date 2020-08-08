using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWCustomerSupport.Pages.Tickets {

    public class CreateModel : PageModel {

        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var emptyTicket = new Ticket();

            if (await TryUpdateModelAsync<Ticket>(emptyTicket, "ticket",
                t => t.Name,
                t => t.Description,
                t => t.Deadline)) {
                _context.Tickets.Add(emptyTicket);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }

    }

}
