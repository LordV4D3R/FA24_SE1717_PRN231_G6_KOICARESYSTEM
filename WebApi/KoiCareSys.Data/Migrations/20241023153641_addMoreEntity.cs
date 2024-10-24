using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiCareSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class addMoreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color",
                schema: "koicare",
                table: "koi_record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "koicare",
                table: "koi_record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "health_issue",
                schema: "koicare",
                table: "koi_record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                schema: "koicare",
                table: "koi_record",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "record_name",
                schema: "koicare",
                table: "koi_record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "feed_by",
                schema: "koicare",
                table: "feeding_schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "foodcaculate",
                schema: "koicare",
                table: "feeding_schedule",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "temperature",
                schema: "koicare",
                table: "feeding_schedule",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                schema: "koicare",
                table: "koi_record");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "koicare",
                table: "koi_record");

            migrationBuilder.DropColumn(
                name: "health_issue",
                schema: "koicare",
                table: "koi_record");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "koicare",
                table: "koi_record");

            migrationBuilder.DropColumn(
                name: "record_name",
                schema: "koicare",
                table: "koi_record");

            migrationBuilder.DropColumn(
                name: "feed_by",
                schema: "koicare",
                table: "feeding_schedule");

            migrationBuilder.DropColumn(
                name: "foodcaculate",
                schema: "koicare",
                table: "feeding_schedule");

            migrationBuilder.DropColumn(
                name: "temperature",
                schema: "koicare",
                table: "feeding_schedule");
        }
    }
}
