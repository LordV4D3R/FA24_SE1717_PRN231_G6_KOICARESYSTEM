﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiCareSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "koicare");

            migrationBuilder.CreateTable(
                name: "development_stage",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stage_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    required_food_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_development_stage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total_sold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    unit_of_measure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    min_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    max_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_detail_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "koicare",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "koicare",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pond",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pond_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    depth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    drain_count = table.Column<int>(type: "int", nullable: true),
                    skimmer_count = table.Column<int>(type: "int", nullable: true),
                    pump_capacity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_qualified = table.Column<bool>(type: "bit", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pond", x => x.id);
                    table.ForeignKey(
                        name: "FK_pond_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "koicare",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feeding_schedule",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    foodcaculate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    feed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    food_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    feed_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    food_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pond_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feeding_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_feeding_schedule_pond_pond_id",
                        column: x => x.pond_id,
                        principalSchema: "koicare",
                        principalTable: "pond",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "koi",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    physique = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    in_pond_since = table.Column<DateOnly>(type: "date", nullable: false),
                    purchase_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pond_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_koi", x => x.id);
                    table.ForeignKey(
                        name: "FK_koi_pond_pond_id",
                        column: x => x.pond_id,
                        principalSchema: "koicare",
                        principalTable: "pond",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "measurement",
                schema: "koicare",
                columns: table => new
                {
                    measurement_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_record = table.Column<DateTime>(type: "datetime2", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pond_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measurement", x => x.measurement_id);
                    table.ForeignKey(
                        name: "FK_measurement_pond_pond_id",
                        column: x => x.pond_id,
                        principalSchema: "koicare",
                        principalTable: "pond",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "koi_record",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    health_issue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    record_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    physique = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    koi_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    development_stage_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_koi_record", x => x.id);
                    table.ForeignKey(
                        name: "FK_koi_record_development_stage_development_stage_id",
                        column: x => x.development_stage_id,
                        principalSchema: "koicare",
                        principalTable: "development_stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_koi_record_koi_koi_id",
                        column: x => x.koi_id,
                        principalSchema: "koicare",
                        principalTable: "koi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "measure_data",
                schema: "koicare",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    measurement_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measure_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_measure_data_measurement_measurement_id",
                        column: x => x.measurement_id,
                        principalSchema: "koicare",
                        principalTable: "measurement",
                        principalColumn: "measurement_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_measure_data_unit_unit_id",
                        column: x => x.unit_id,
                        principalSchema: "koicare",
                        principalTable: "unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feeding_schedule_pond_id",
                schema: "koicare",
                table: "feeding_schedule",
                column: "pond_id");

            migrationBuilder.CreateIndex(
                name: "IX_koi_pond_id",
                schema: "koicare",
                table: "koi",
                column: "pond_id");

            migrationBuilder.CreateIndex(
                name: "IX_koi_record_development_stage_id",
                schema: "koicare",
                table: "koi_record",
                column: "development_stage_id");

            migrationBuilder.CreateIndex(
                name: "IX_koi_record_koi_id",
                schema: "koicare",
                table: "koi_record",
                column: "koi_id");

            migrationBuilder.CreateIndex(
                name: "IX_measure_data_measurement_id",
                schema: "koicare",
                table: "measure_data",
                column: "measurement_id");

            migrationBuilder.CreateIndex(
                name: "IX_measure_data_unit_id",
                schema: "koicare",
                table: "measure_data",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_measurement_pond_id",
                schema: "koicare",
                table: "measurement",
                column: "pond_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                schema: "koicare",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_product_id",
                schema: "koicare",
                table: "order_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_pond_user_id",
                schema: "koicare",
                table: "pond",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feeding_schedule",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "koi_record",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "measure_data",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "order_detail",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "development_stage",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "koi",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "measurement",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "unit",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "order",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "product",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "pond",
                schema: "koicare");

            migrationBuilder.DropTable(
                name: "user",
                schema: "koicare");
        }
    }
}
