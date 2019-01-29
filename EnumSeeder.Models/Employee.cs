using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnumSeeder.Models
{
    public enum Department{

        [Description("Sales")]
        Sales = 1,

        [Description("Customer Service")]
        CustomerService = 2,

        [Description("Technical Support")]
        TechnicalSupport = 3
    }

    public class Employee : IEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [StringLength(512, ErrorMessage = "The first name cannot be longer than 512 characters")]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(512, ErrorMessage = "The last name cannot be longer than 512 characters")]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        public Department Department { get; set; }

    }
}
