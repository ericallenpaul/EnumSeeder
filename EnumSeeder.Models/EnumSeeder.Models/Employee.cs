using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnumSeeder.Models
{
    public enum Department {
       Sales = 1,
       CustomerService = 2,
       TechnicalSupport = 3
    }

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(512, ErrorMessage = "First Name cannot be longer than 512 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(512, ErrorMessage = "Last Name cannot be longer than 512 characters")]
        public string LastName { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }
    }
}
