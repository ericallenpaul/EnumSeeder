using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumSeeder.Service.Migrations
{
    public partial class EnumSqlScripts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = @"C:\Users\ericp\Source\Repos\EnumSeeder\EnumSeeder.Service\EnumSqlScripts\Custom.Sql";
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
