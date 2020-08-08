using System;

namespace AWCustomerSupport.Data.Models {

    public class Ticket {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }

    }

    public enum Status {

        Active,
        Inactive

    }

}
