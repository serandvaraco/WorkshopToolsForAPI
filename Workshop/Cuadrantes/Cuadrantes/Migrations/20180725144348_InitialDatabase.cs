using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cuadrantes.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformacionUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cedula = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    FechaExpedicion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionCuadrantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitud = table.Column<string>(nullable: true),
                    Longitud = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    NumeroCuadrante = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionCuadrantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformacionPolicial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UbicacionCuadranteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionPolicial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacionPolicial_UbicacionCuadrantes_UbicacionCuadranteId",
                        column: x => x.UbicacionCuadranteId,
                        principalTable: "UbicacionCuadrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionAlarmas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitud = table.Column<string>(nullable: true),
                    Longitud = table.Column<string>(nullable: true),
                    UbicacionCuadranteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionAlarmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionAlarmas_UbicacionCuadrantes_UbicacionCuadranteId",
                        column: x => x.UbicacionCuadranteId,
                        principalTable: "UbicacionCuadrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformacionPolicial_UbicacionCuadranteId",
                table: "InformacionPolicial",
                column: "UbicacionCuadranteId");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionAlarmas_UbicacionCuadranteId",
                table: "UbicacionAlarmas",
                column: "UbicacionCuadranteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacionPolicial");

            migrationBuilder.DropTable(
                name: "InformacionUsuario");

            migrationBuilder.DropTable(
                name: "UbicacionAlarmas");

            migrationBuilder.DropTable(
                name: "UbicacionCuadrantes");
        }
    }
}
