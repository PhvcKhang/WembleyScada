using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteExecutionTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionTime",
                table: "Shot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExecutionTime",
                table: "Shot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
