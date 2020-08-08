using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Pages.Tickets {

    public class IndexModel : PageModel {

        private const string NAME_ASC = "name_asc";
        private const string NAME_DESC = "name_desc";

        private const string DESCR_ASC = "descr_asc";
        private const string DESCR_DESC = "descr_desc";

        private const string ENTRY_ASC = "entry_asc";
        private const string ENTRY_DESC = "entry_desc";

        private const string DL_DESC = "dl_desc";

        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context) {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DescriptionSort { get; set; }
        public string EntrySort { get; set; }
        public string DeadlineSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Ticket> Tickets { get; set; }

        public async Task OnGetAsync(string sortOrder) {
            NameSort = sortOrder == NAME_ASC ? NAME_DESC : NAME_ASC;
            DescriptionSort = sortOrder == DESCR_ASC ? DESCR_DESC : DESCR_ASC;
            EntrySort = sortOrder == ENTRY_ASC ? ENTRY_DESC : ENTRY_ASC;
            DeadlineSort = string.IsNullOrEmpty(sortOrder) ? DL_DESC : "";

            IQueryable<Ticket> ticketsIQ = from t in _context.Tickets select t;

            switch (sortOrder) {
                case NAME_DESC:
                    ticketsIQ = ticketsIQ.OrderByDescending(t => t.Name);
                    break;
                case NAME_ASC:
                    ticketsIQ = ticketsIQ.OrderBy(t => t.Name);
                    break;
                case DESCR_DESC:
                    ticketsIQ = ticketsIQ.OrderByDescending(t => t.Description);
                    break;
                case DESCR_ASC:
                    ticketsIQ = ticketsIQ.OrderBy(t => t.Description);
                    break;
                case ENTRY_DESC:
                    ticketsIQ = ticketsIQ.OrderByDescending(t => t.EntryDate);
                    break;
                case ENTRY_ASC:
                    ticketsIQ = ticketsIQ.OrderBy(t => t.EntryDate);
                    break;
                case DL_DESC:
                    ticketsIQ = ticketsIQ.OrderByDescending(t => t.Deadline);
                    break;
                default:
                    ticketsIQ = ticketsIQ.OrderBy(t => t.Deadline);
                    break;
            }

            Tickets = await ticketsIQ.AsNoTracking().ToListAsync();
        }

    }

}
