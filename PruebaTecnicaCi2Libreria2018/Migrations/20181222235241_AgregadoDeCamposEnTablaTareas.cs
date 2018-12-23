using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaTecnicaCi2Libreria2018.Migrations
{
    public partial class AgregadoDeCamposEnTablaTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroActividades",
                table: "Tareas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Objetivo",
                table: "Tareas",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroActividades",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Objetivo",
                table: "Tareas");
        }
    }
}
