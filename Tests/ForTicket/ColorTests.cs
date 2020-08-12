using System;
using AWCustomerSupport.Pages.Tickets;
using Xunit;

namespace Tests.ForTicket {

    public class ColorTests : TicketTestBase {

        private const string _expectedBackgroundColor = "background-color: crimson";
        private const string _expectedTextColor = "color: white";

        private readonly DateTime _hourToDeadline = DateTime.Now.AddHours(1);
        private readonly DateTime _overDeadline = DateTime.Now.AddHours(-20);
        private readonly DateTime _overAnHourToDeadline = DateTime.Now.AddHours(1.0001);

        [Fact]
        public void ColorsChangeWhenAnHourToDeadline() {
            _ticket.Deadline = _hourToDeadline;

            string actualBgColor = IndexModel.GetBackgroundColor(_ticket);
            string actualTextColor = IndexModel.GetTextColor(_ticket);

            Assert.Equal(_expectedBackgroundColor, actualBgColor);
            Assert.Equal(_expectedTextColor, actualTextColor);
        }

        [Fact]
        public void ColorsChangesWhenOverDeadline() {
            _ticket.Deadline = _overDeadline;

            string actualBgColor = IndexModel.GetBackgroundColor(_ticket);
            string actualTextColor = IndexModel.GetTextColor(_ticket);

            Assert.Equal(_expectedBackgroundColor, actualBgColor);
            Assert.Equal(_expectedTextColor, actualTextColor);
        }

        [Fact]
        public void ColorsAreNullWhenOverAnHourToDeadline() {
            _ticket.Deadline = _overAnHourToDeadline;

            string actualBgColor = IndexModel.GetBackgroundColor(_ticket);
            string actualTextColor = IndexModel.GetTextColor(_ticket);

            Assert.Null(actualBgColor);
            Assert.Null(actualTextColor);
        }

    }

}
