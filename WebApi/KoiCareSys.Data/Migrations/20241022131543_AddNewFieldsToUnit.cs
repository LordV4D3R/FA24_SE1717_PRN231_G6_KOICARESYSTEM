using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiCareSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alert_message",
                schema: "koicare",
                table: "unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "critical_threshold",
                schema: "koicare",
                table: "unit",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "koicare",
                table: "unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ideal_value",
                schema: "koicare",
                table: "unit",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "koicare",
                table: "unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "warning_threshold",
                schema: "koicare",
                table: "unit",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alert_message",
                schema: "koicare",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "critical_threshold",
                schema: "koicare",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "koicare",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "ideal_value",
                schema: "koicare",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "koicare",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "warning_threshold",
                schema: "koicare",
                table: "unit");
        }
    }
}
