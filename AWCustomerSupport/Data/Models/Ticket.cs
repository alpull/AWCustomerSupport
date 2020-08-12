using System;
using System.ComponentModel.DataAnnotations;

namespace AWCustomerSupport.Data.Models {

    public class Ticket {

        public const string DATE_FORMAT = "dd/MM/yyyy HH:mm";

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Entry")]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [ValidateDateRange]
        public DateTime Deadline { get; set; }

        public TimeSpan TimeToDeadline => Deadline - DateTime.Now;

    }

    public class ValidateDateRange : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext context) {
            return Convert.ToDateTime(value) >= DateTime.Now ?
                ValidationResult.Success : new ValidationResult("Deadline must be in the future!");
        }

    }

}
