using Microsoft.EntityFrameworkCore.Migrations;

namespace CrmAzure.Migrations
{
    public partial class addcustomergross : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gross",
                table: "Customer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gross",
                table: "Customer");
        }
    }
}
