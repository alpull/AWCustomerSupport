using System;
using AWCustomerSupport.Data.Models;

namespace Tests.ForTicket {

    public class TicketTestBase {

        protected static Ticket _ticket;
        protected const string DATE_FORMAT = Ticket.DATE_FORMAT;

        protected TicketTestBase() {
            const string name = "";
            const string description = "";
            DateTime deadline = DateTime.Now.AddDays(2);

            _ticket = new Ticket {
                Name = name,
                Description = description,
                Deadline = deadline
            };
        }

        // TimeSpans differ by a minimal amount of ticks, therefore the values are rounded
        protected static TimeSpan RoundTimeSpan(TimeSpan ts) {
            int factor = (int) Math.Pow(10, 7);

            return new TimeSpan((long) Math.Round(1.0 * ts.Ticks / factor) * factor);
        }

        // TODO: Deadline <>= EntryDate tests
        // TODO: Any other helper method test

    }

}
