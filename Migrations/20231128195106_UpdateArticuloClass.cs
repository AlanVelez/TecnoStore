using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnostore.com.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticuloClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Carts_IdCart",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_IdCart",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "IdCart",
                table: "Articulos");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Articulos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Articulos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Articulos");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCart",
                table: "Articulos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdCart",
                table: "Articulos",
                column: "IdCart");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Carts_IdCart",
                table: "Articulos",
                column: "IdCart",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
