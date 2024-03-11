using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class FKLotReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_References_ReferenceId",
                table: "Lot");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "Lot",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_References_ReferenceId",
                table: "Lot",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "ReferenceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_References_ReferenceId",
                table: "Lot");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "Lot",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_References_ReferenceId",
                table: "Lot",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "ReferenceId");
        }
    }
}
