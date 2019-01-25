using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EnumSeeder.Models
{
    public class AppUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int AppUser_ID { get; set; }

        [DisplayName("First Name")]
        public virtual string FirstName { get; set; }

        [DisplayName("Last Name")]
        public virtual string LastName { get; set; }

    }
}
