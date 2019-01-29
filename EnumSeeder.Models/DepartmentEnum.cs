using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnumSeeder.Models
{
    [Table("Department")]
    public class DepartmentEnum : EnumBase<Department>
    {
        public DepartmentEnum()
        {
        }
    }
}
