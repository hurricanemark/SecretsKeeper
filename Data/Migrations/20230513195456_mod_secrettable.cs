using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretsKeeper.Data.Migrations
{
    public partial class mod_secrettable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpirationDate",
                table: "Secret",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isTrial",
                table: "Secret",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Secret");

            migrationBuilder.DropColumn(
                name: "isTrial",
                table: "Secret");
        }
    }
}
