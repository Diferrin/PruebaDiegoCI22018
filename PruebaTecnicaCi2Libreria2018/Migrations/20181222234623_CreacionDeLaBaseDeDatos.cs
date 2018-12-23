using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaTecnicaCi2Libreria2018.Migrations
{
    public partial class CreacionDeLaBaseDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    GuTareaId = table.Column<Guid>(nullable: false),
                    StrDescripcion = table.Column<string>(maxLength: 200, nullable: true),
                    DatFechaCreacion = table.Column<DateTime>(nullable: false),
                    DatFechaVencimineto = table.Column<DateTime>(nullable: false),
                    BolEstado = table.Column<bool>(nullable: false),
                    IntFkUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.GuTareaId);
                    table.ForeignKey(
                        name: "FK_Tareas_Usuarios_IntFkUserId",
                        column: x => x.IntFkUserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IntFkUserId",
                table: "Tareas",
                column: "IntFkUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
