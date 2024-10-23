using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiCareSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feeding_schedule_koi_koi_id",
                schema: "koicare",
                table: "feeding_schedule");

            migrationBuilder.RenameColumn(
                name: "koi_id",
                schema: "koicare",
                table: "feeding_schedule",
                newName: "pond_id");

            migrationBuilder.RenameIndex(
                name: "IX_feeding_schedule_koi_id",
                schema: "koicare",
                table: "feeding_schedule",
                newName: "IX_feeding_schedule_pond_id");

            migrationBuilder.AddForeignKey(
                name: "FK_feeding_schedule_pond_pond_id",
                schema: "koicare",
                table: "feeding_schedule",
                column: "pond_id",
                principalSchema: "koicare",
                principalTable: "pond",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feeding_schedule_pond_pond_id",
                schema: "koicare",
                table: "feeding_schedule");

            migrationBuilder.RenameColumn(
                name: "pond_id",
                schema: "koicare",
                table: "feeding_schedule",
                newName: "koi_id");

            migrationBuilder.RenameIndex(
                name: "IX_feeding_schedule_pond_id",
                schema: "koicare",
                table: "feeding_schedule",
                newName: "IX_feeding_schedule_koi_id");

            migrationBuilder.AddForeignKey(
                name: "FK_feeding_schedule_koi_koi_id",
                schema: "koicare",
                table: "feeding_schedule",
                column: "koi_id",
                principalSchema: "koicare",
                principalTable: "koi",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
