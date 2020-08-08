using System;
using System.Linq;
using AWCustomerSupport.Data.Models;

namespace AWCustomerSupport.Data {

    public static class DbInitializer {

        public static void Initialize(AppDbContext context) {
            context.Database.EnsureCreated();

            // TODO: Ensure Tickets table is created even when database exists
            if (context.Tickets.Any()) return;

            var tickets = new[] {
                new Ticket {
                    Name = "URGENT ISSUE",
                    Description = "Help pls, my cat broke down!!1!",
                    EntryDate = DateTime.Now,
                    Deadline = DateTime.Today.AddDays(1),
                },
                new Ticket {
                    Name = "Moderate issue",
                    Description = "My dog isn't working properly",
                    EntryDate = DateTime.Now,
                    Deadline = DateTime.Today.AddDays(2),
                },
                new Ticket {
                    Name = "Take your time",
                    Description = "Lost my stress ball :( I have another, tho",
                    EntryDate = DateTime.Now,
                    Deadline = DateTime.MaxValue,
                }
            };

            context.Tickets.AddRange(tickets);
            context.SaveChanges();
        }

    }

}
