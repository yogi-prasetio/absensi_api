using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace absensi_api.Migrations
{
    /// <inheritdoc />
    public partial class add_table_position : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "position_id",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.position_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_position_id",
                table: "Accounts",
                column: "position_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Position_position_id",
                table: "Accounts",
                column: "position_id",
                principalTable: "Position",
                principalColumn: "position_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Position_position_id",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_position_id",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "position_id",
                table: "Accounts");
        }
    }
}
