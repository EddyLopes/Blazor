using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HospitalManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class v1_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCode_AspNetUsers_CreatedById",
                table: "SystemCode");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "SystemCodeDetail");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "SystemCode");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "SystemCode",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    ModifiedById = table.Column<string>(type: "varchar(255)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedById = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departaments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departaments_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_CreatedById",
                table: "Departaments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_ModifiedById",
                table: "Departaments",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCode_AspNetUsers_CreatedById",
                table: "SystemCode",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCode_AspNetUsers_CreatedById",
                table: "SystemCode");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "SystemCodeDetail",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "SystemCode",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "SystemCode",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCode_AspNetUsers_CreatedById",
                table: "SystemCode",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
