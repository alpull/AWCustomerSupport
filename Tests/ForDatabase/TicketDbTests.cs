using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using AWCustomerSupport.Data;
using AWCustomerSupport.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace Tests.ForDatabase {

    public class TicketDbTests : DatabaseTestBase {

        private readonly DbConnection _connection;

        public TicketDbTests() : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(CreateInMemoryDatabase()).Options) {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase() {
            DbConnection connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        [Fact]
        public void DatabaseIsSeeded() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            const int expectedCount = 3;
            List<Ticket> tickets = context.Tickets.ToList();

            Assert.NotNull(context.Database);
            Assert.NotEmpty(context.Tickets);

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
        public void CanAddTicketToDatabase() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            Ticket t = context.Tickets.Add(ValidTicket).Entity;
            context.SaveChanges();

            Assert.NotNull(t);

            Assert.Equal(VALID_NAME, t.Name);
            Assert.Equal(VALID_DESCRIPTION, t.Description);
            Assert.Equal(ValidDeadline, t.Deadline);
        }

        [Fact]
        public void NewTicketIdEquals4() {
            using AppDbContext context = new AppDbContext(ContextOptions);

            const int expectedId = 4;

            Ticket t = context.Tickets.Add(ValidTicket).Entity;
            context.SaveChanges();

            Assert.Equal(expectedId, t.Id);
        }

        [Theory]
        [InlineData("Ticket 4", "Sample text")]
        [InlineData("Ticket 5", "This is also a text")]
        [InlineData("Ticket 6", "This is a description")]
        public void CanFindNewTicketFromDatabase(string name, string description) {
            using AppDbContext context = new AppDbContext(ContextOptions);

            DateTime deadline = DateTime.Now.AddHours(new Random().Next(0, 100));
            const int expectedId = 4;
            const int expectedCount = 4;

            context.Tickets
                .Add(new Ticket {Name = name, Description = description, Deadline = deadline});
            context.SaveChanges();

            Ticket t = context.Tickets.Find(expectedId);

            Assert.Equal(expectedCount, context.Tickets.Count());

            Assert.Equal(name, t.Name);
            Assert.Equal(description, t.Description);
            Assert.Equal(deadline, t.Deadline);
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
