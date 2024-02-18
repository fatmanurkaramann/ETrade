using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETradeAPI.Persistance.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Products",
                type: "text",
                nullable: true);
        }
    }
}
