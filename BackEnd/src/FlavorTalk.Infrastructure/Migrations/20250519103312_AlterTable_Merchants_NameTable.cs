using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlavorTalk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_Merchants_NameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_Users_DeletedById",
                table: "Merchant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant");

            migrationBuilder.RenameTable(
                name: "Merchant",
                newName: "Merchants");

            migrationBuilder.RenameIndex(
                name: "IX_Merchant_DeletedById",
                table: "Merchants",
                newName: "IX_Merchants_DeletedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Users_DeletedById",
                table: "Merchants",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Users_DeletedById",
                table: "Merchants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants");

            migrationBuilder.RenameTable(
                name: "Merchants",
                newName: "Merchant");

            migrationBuilder.RenameIndex(
                name: "IX_Merchants_DeletedById",
                table: "Merchant",
                newName: "IX_Merchant_DeletedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_Users_DeletedById",
                table: "Merchant",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
