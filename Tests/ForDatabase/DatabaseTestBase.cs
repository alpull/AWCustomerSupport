using System;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests.ForDatabase {

    public class DatabaseTestBase {

        protected DatabaseTestBase(DbContextOptions<AppDbContext> options) {
            ContextOptions = options;
            Seed();

            ValidTicket = new Ticket {Name = VALID_NAME, Description = VALID_DESCRIPTION, Deadline = ValidDeadline};
        }

        protected DbContextOptions<AppDbContext> ContextOptions { get; }

        protected const string VALID_NAME = "This is a valid name";
        protected const string VALID_DESCRIPTION = "This is a valid description";

        protected DateTime ValidDeadline = DateTime.Now.AddDays(2);

        protected readonly Ticket ValidTicket;

        private void Seed() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Ticket[] tickets = {
                new Ticket {
                    Name = "Ticket 0",
                    Description = "Something is happening",
                    Deadline = DateTime.Now
                },
                new Ticket {
                    Name = "Ticket 1",
                    Description = "Woah",
                    Deadline = DateTime.Now.AddHours(1)
                },
                new Ticket {
                    Name = "Ticket 2",
                    Description = "This is amazing",
                    Deadline = DateTime.Now.AddDays(5)
                }
            };

            context.AddRange(tickets);
            context.SaveChanges();
        }

    }

}
