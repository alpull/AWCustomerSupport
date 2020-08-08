using System.Collections.Generic;
using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class IndexModel : PageModel {

        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context) {
            _context = context;
        }

        public IList<Ticket> Tickets { get; set; }

        public async Task OnGetAsync() {
            Tickets = await _context.Tickets.ToListAsync();
        }

    }

}
