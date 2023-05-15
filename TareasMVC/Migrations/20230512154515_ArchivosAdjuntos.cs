using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    public partial class ArchivosAdjuntos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivoAdjuntos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TareaId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchivoAdjuntoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivoAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivoAdjuntos_ArchivoAdjuntos_ArchivoAdjuntoId",
                        column: x => x.ArchivoAdjuntoId,
                        principalTable: "ArchivoAdjuntos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchivoAdjuntos_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_ArchivoAdjuntoId",
                table: "ArchivoAdjuntos",
                column: "ArchivoAdjuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_TareaId",
                table: "ArchivoAdjuntos",
                column: "TareaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivoAdjuntos");
        }
    }
}
