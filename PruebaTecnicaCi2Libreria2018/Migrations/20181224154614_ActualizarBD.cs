using Microsoft.EntityFrameworkCore.Migrations;

namespace Libreria.Migrations
{
    public partial class ActualizarBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuarios",
                nullable: true);
        }
    }
}
