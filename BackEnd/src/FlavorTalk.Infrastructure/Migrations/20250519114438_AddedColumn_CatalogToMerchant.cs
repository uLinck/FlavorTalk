using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlavorTalk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumn_CatalogToMerchant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CatalogId",
                table: "Merchants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_CatalogId",
                table: "Merchants",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Catalogs_CatalogId",
                table: "Merchants",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Catalogs_CatalogId",
                table: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_CatalogId",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "Merchants");
        }
    }
}
