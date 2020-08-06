using System.Collections.Generic;
using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class IndexModel : PageModel {

        private readonly TicketContext _context;

        public IndexModel(TicketContext context) {
            _context = context;
        }

        public IList<Ticket> Ticket { get; set; }

        public async Task OnGetAsync() {
            Ticket = await _context.Ticket.ToListAsync();
        }

    }

}