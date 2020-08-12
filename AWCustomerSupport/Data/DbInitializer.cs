using System;
using System.Linq;
using AWCustomerSupport.Data.Models;

namespace AWCustomerSupport.Data {

    public static class DbInitializer {

        public static void Initialize(AppDbContext context) {

            // TODO: Ensure Tickets table is created even when database exists
            if (context.Tickets.Any()) return;

            const string dateFormat = Ticket.DATE_FORMAT;

            var tickets = new[] {
                new Ticket {
                    Name = "URGENT ISSUE",
                    Description = "Help pls, my cat broke down!!1!",
                    Deadline = DateTime.Parse(DateTime.Now.AddHours(1).ToString(dateFormat))
                },
                new Ticket {
                    Name = "Moderate issue",
                    Description = "My dog isn't working properly",
                    Deadline = DateTime.Parse(DateTime.Now.AddDays(3).ToString(dateFormat))
                },
                new Ticket {
                    Name = "Take your time",
                    Description = "Lost my stress ball :( I have another, tho",
                    Deadline = DateTime.Parse(DateTime.MaxValue.ToString(dateFormat))
                }
            };

            context.Tickets.AddRange(tickets);
            context.SaveChanges();
        }

    }

}
