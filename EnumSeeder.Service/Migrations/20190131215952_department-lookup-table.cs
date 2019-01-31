using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumSeeder.Service.Migrations
{
    public partial class departmentlookuptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Deleted", "Description", "Name" },
                values: new object[] { 1, false, "Sales", "Sales" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Deleted", "Description", "Name" },
                values: new object[] { 2, false, "Customer Service", "Customer Service" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Deleted", "Description", "Name" },
                values: new object[] { 3, false, "Technical Support", "TechnicalSupport" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
