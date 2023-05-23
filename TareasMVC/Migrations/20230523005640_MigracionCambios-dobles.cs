using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    public partial class MigracionCambiosdobles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivoAdjuntos_Tareas_TareaId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchivoAdjuntos",
                table: "ArchivoAdjuntos");

            migrationBuilder.RenameTable(
                name: "ArchivoAdjuntos",
                newName: "ArchivosAdjuntos");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivoAdjuntos_TareaId",
                table: "ArchivosAdjuntos",
                newName: "IX_ArchivosAdjuntos_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchivosAdjuntos",
                table: "ArchivosAdjuntos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAdjuntos_Tareas_TareaId",
                table: "ArchivosAdjuntos",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAdjuntos_Tareas_TareaId",
                table: "ArchivosAdjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchivosAdjuntos",
                table: "ArchivosAdjuntos");

            migrationBuilder.RenameTable(
                name: "ArchivosAdjuntos",
                newName: "ArchivoAdjuntos");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivosAdjuntos_TareaId",
                table: "ArchivoAdjuntos",
                newName: "IX_ArchivoAdjuntos_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchivoAdjuntos",
                table: "ArchivoAdjuntos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivoAdjuntos_Tareas_TareaId",
                table: "ArchivoAdjuntos",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
