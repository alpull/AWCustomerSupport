using AWCustomerSupport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Data {

    public class TicketContext : DbContext {

        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options) { }

        public DbSet<Ticket> Ticket { get; set; }

    }

}
