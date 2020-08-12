using System;
using AWCustomerSupport.Data.Models;
using AWCustomerSupport.Pages.Tickets;
using Xunit;

namespace Tests {

    public class TicketTests {

        private readonly Ticket _ticket;
        private const string DateFormat = Ticket.DATE_FORMAT;

        public TicketTests() {
            const string name = "";
            const string description = "";
            DateTime deadline = DateTime.Now.AddDays(2);

            _ticket = new Ticket {
                Name = name,
                Description = description,
                Deadline = deadline
            };
        }

        [Fact]
        public void IdIsEqual() {
            const int expected = 0;

            Assert.Equal(expected, _ticket.Id);
        }

        [Fact]
        public void NameIsEqual() {
            const string expected = "";

            Assert.Equal(expected, _ticket.Name);
        }

        [Fact]
        public void DescriptionIsEqual() {
            const string expected = "";

            Assert.Equal(expected, _ticket.Description);
        }

        [Fact]
        public void EntryDateIsEqual() {
            DateTime expected = DateTime.Now;

            Assert.Equal(expected.ToString(DateFormat), _ticket.EntryDate.ToString(DateFormat));
        }

        [Fact]
        public void DeadlineIsEqual() {
            DateTime expected = DateTime.Now.AddDays(2);

            Assert.Equal(expected.ToString(DateFormat), _ticket.Deadline.ToString(DateFormat));
        }

        [Fact]
        public void TimeToDeadlineIsCorrectlyCalculated() {
            DateTime entry = DateTime.Now;
            DateTime deadline = DateTime.Now.AddDays(2);
            TimeSpan expected = deadline - entry;

            Assert.Equal(RoundTimeSpan(expected), RoundTimeSpan(_ticket.TimeToDeadline));
        }

        private static TimeSpan RoundTimeSpan(TimeSpan ts) {
            const int precision = 0; // Digits after the decimal point to round to
            const int TIMESPAN_SIZE = 7; // There are always 7 digits after the decimal point

            int factor = (int) Math.Pow(10, TIMESPAN_SIZE - precision);

            return new TimeSpan((long) Math.Round(1.0 * ts.Ticks / factor) * factor);
        }

        [Fact]
        public void BackgroundColorChangesWhenAnHourToDeadline() {
            DateTime deadline = DateTime.Now.AddHours(1);
            _ticket.Deadline = deadline;

            string expected = "background-color: crimson";
            string actual = IndexModel.GetBackgroundColor(_ticket);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BackgroundColorChangesWhenOverDeadline() {
            DateTime deadline = DateTime.Now.AddDays(-20);
            _ticket.Deadline = deadline;

            string expected = "background-color: crimson";
            string actual = IndexModel.GetBackgroundColor(_ticket);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BackgroundColorIsNullWhenOverAnHourToDeadline() {
            DateTime deadline = DateTime.Now.AddHours(1.00001);
            _ticket.Deadline = deadline;

            string expected = null;
            string actual = IndexModel.GetBackgroundColor(_ticket);

            Assert.Equal(expected, actual);
        }

    }

}
