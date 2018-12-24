using Microsoft.EntityFrameworkCore.Migrations;

namespace Libreria.Migrations
{
    public partial class ActualizacionCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Usuarios",
                newName: "Nombres");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Usuarios",
                newName: "Apellidos");

            migrationBuilder.AlterColumn<string>(
                name: "Objetivo",
                table: "Tareas",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "Usuarios",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Apellidos",
                table: "Usuarios",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "Objetivo",
                table: "Tareas",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
