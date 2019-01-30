using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EnumSeeder.Service
{
    public class CreateEnumRowOperation : MigrationOperation
    {
        public string TableName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
