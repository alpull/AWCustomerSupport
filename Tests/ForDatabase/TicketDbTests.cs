using System;
using System.Collections.Generic;
using System.Linq;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.ForDatabase {

    public class TicketDbTests : DatabaseTestBase {

        public TicketDbTests() : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TEST").Options) { }

        [Fact]
        public void DatabaseIsSeeded() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            Assert.NotNull(context.Database);
            Assert.NotEmpty(context.Tickets);
        }

        [Fact]
        public void TicketsAreCreated() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            const int expectedCount = 3;
            List<Ticket> tickets = context.Tickets.ToList();

            Assert.Equal(expectedCount, tickets.Count);

            for (int i = 0; i < expectedCount; i++) {
                Assert.Equal($"Ticket {i}", tickets[i].Name);
            }
        }

        [Fact]
        public void CanAddTicket() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            Ticket t = context.Tickets.Add(ValidTicket).Entity;

            Assert.NotNull(t);

            Assert.Equal(VALID_NAME, t.Name);
            Assert.Equal(VALID_DESCRIPTION, t.Description);
            Assert.Equal(ValidDeadline, t.Deadline);
        }

        [Fact]
        public void NewTicketIdEquals4() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            Ticket t = context.Tickets.Add(ValidTicket).Entity;

            Assert.NotNull(t);

            Assert.Equal(4, t.Id);
            Assert.Equal(VALID_NAME, t.Name);
            Assert.Equal(VALID_DESCRIPTION, t.Description);
            Assert.Equal(ValidDeadline, t.Deadline);
        }

        [Theory]
        [InlineData("Ticket 4", "Sample text")]
        [InlineData("Ticket 5", "This is also a text")]
        [InlineData("Ticket 6", "This is a description")]
        public void CanAddTicketToDatabase(string name, string description) {
            using AppDbContext context = new AppDbContext(ContextOptions);

            DateTime deadline = DateTime.Now.AddHours(new Random().Next(0, 100));

            context.Tickets
                .Add(new Ticket {Name = name, Description = description, Deadline = deadline});
            context.SaveChanges();

            Ticket ticket = context.Tickets.Find(4);

            Assert.Equal(4, context.Tickets.Count());

            Assert.Equal(name, ticket.Name);
            Assert.Equal(description, ticket.Description);
            Assert.Equal(deadline, ticket.Deadline);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanEditNameDescriptionAndDeadline(int id) {
            using AppDbContext context = new AppDbContext(ContextOptions);

            const string newName = "New name";
            const string newDescription = "New description";
            DateTime newDeadline = DateTime.Now.AddMonths(20);

            Ticket t = context.Tickets.Find(id);

            t.Name = newName;
            t.Description = newDescription;
            t.Deadline = newDeadline;

            context.SaveChanges();

            t = context.Tickets.Find(id);

            Assert.Equal(id, t.Id);
            Assert.Equal(newName, t.Name);
            Assert.Equal(newDescription, t.Description);
            Assert.Equal(newDeadline, t.Deadline);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanDeleteTicket(int id) {
            using AppDbContext context = new AppDbContext(ContextOptions);

            Ticket t = context.Tickets.Find(id);

            context.Tickets.Remove(t);
            context.SaveChanges();

            t = context.Tickets.Find(id);

            Assert.Null(t);
        }

    }

}
