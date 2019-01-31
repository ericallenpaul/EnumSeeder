using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnumSeeder.Models
{
    public class EnumBase<TEnum> where TEnum : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Name { get; set; }

        [MaxLength(100)]
        public virtual string Description { get; set; }

        public virtual bool Deleted { get; set; } = false;
    }
}
