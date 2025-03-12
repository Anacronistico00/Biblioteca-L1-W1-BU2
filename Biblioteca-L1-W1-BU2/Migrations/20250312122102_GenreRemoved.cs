using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca_L1_W1_BU2.Migrations
{
    /// <inheritdoc />
    public partial class GenreRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestito_Books_BookId",
                table: "Prestito");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestito",
                table: "Prestito");

            migrationBuilder.RenameTable(
                name: "Prestito",
                newName: "Prestiti");

            migrationBuilder.RenameIndex(
                name: "IX_Prestito_BookId",
                table: "Prestiti",
                newName: "IX_Prestiti_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestiti",
                table: "Prestiti",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestiti_Books_BookId",
                table: "Prestiti",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestiti_Books_BookId",
                table: "Prestiti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestiti",
                table: "Prestiti");

            migrationBuilder.RenameTable(
                name: "Prestiti",
                newName: "Prestito");

            migrationBuilder.RenameIndex(
                name: "IX_Prestiti_BookId",
                table: "Prestito",
                newName: "IX_Prestito_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestito",
                table: "Prestito",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Prestito_Books_BookId",
                table: "Prestito",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
