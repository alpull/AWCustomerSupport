using System;
using Xunit;

namespace Tests.ForTicket {

    public class PropertyTests : TicketTestBase {

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

            Assert.Equal(expected.ToString(DATE_FORMAT), _ticket.EntryDate.ToString(DATE_FORMAT));
        }

        [Fact]
        public void DeadlineIsEqual() {
            DateTime expected = DateTime.Now.AddDays(2);

            Assert.Equal(expected.ToString(DATE_FORMAT), _ticket.Deadline.ToString(DATE_FORMAT));
        }

        [Fact]
        public void TimeToDeadlineIsCorrectlyCalculated() {
            DateTime entry = DateTime.Now;
            DateTime deadline = DateTime.Now.AddDays(2);
            TimeSpan expected = deadline - entry;

            Assert.Equal(RoundTimeSpan(expected), RoundTimeSpan(_ticket.TimeToDeadline));
        }

    }

}
