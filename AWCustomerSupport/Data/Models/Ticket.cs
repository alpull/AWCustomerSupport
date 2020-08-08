using System;
using System.ComponentModel.DataAnnotations;

namespace AWCustomerSupport.Data.Models {

    public class Ticket {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Entry")]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        public TimeSpan TimeToDeadline => Deadline - DateTime.Now;

    }

}
