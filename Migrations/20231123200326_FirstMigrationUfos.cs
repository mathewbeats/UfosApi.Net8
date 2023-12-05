using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiUfoCasesNet8.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationUfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Testigo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Testimonio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testigo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ufo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LugarDeOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAvistamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetallesAvistamiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelCredibilidad = table.Column<int>(type: "int", nullable: false),
                    ExplicacionesAlternativas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Investigado = table.Column<bool>(type: "bit", nullable: false),
                    ResultadosInvestigacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ufo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Testigo");

            migrationBuilder.DropTable(
                name: "Ufo");
        }
    }
}
