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
        public string ErrorMsg { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false) {
            if (id == null) return NotFound();

            Ticket = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null) return NotFound();

            if (saveChangesError.GetValueOrDefault()) ErrorMsg = "Delete failed. Please try again.";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null) {
                return NotFound();}

            try {
                _context.Tickets.Remove(Ticket);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            } catch (DbUpdateException) {
                return RedirectToAction("./Delete", new {id, saveChangesError = true});
            }

        }

    }

}
