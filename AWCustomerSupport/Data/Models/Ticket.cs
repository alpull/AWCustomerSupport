using System;
using System.ComponentModel.DataAnnotations;

namespace AWCustomerSupport.Data.Models {

    public class Ticket {

        private const string NAME_TOO_SHORT = "Name must be at least 3 characters.";
        private const string NAME_TOO_LONG = "Name must be a maximum of 100 characters.";

        private const string DESCRIPTION_TOO_SHORT = "Description must be at least 10 characters.";
        private const string DESCRIPTION_TOO_LONG = "Name must be a maximum of 300 characters.";

        public const string DATE_FORMAT = "dd/MM/yyyy HH:mm";

        public int Id { get; private set; }

        [Required]
        [MinLength(3, ErrorMessage = NAME_TOO_SHORT),
         MaxLength(100, ErrorMessage = NAME_TOO_LONG)]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = DESCRIPTION_TOO_SHORT),
         MaxLength(300, ErrorMessage = DESCRIPTION_TOO_LONG)]
        public string Description { get; set; }

        [Display(Name = "Entry")]
        public DateTime EntryDate { get; private set; } = DateTime.Now;

        [ValidateDateRange]
        public DateTime Deadline { get; set; }

        public TimeSpan TimeToDeadline => Deadline - DateTime.Now;

    }

    public class ValidateDateRange : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext context) {
            return Convert.ToDateTime(value) >= DateTime.Now
                ? ValidationResult.Success
                : new ValidationResult("Deadline must be in the future.");
        }

    }

}
