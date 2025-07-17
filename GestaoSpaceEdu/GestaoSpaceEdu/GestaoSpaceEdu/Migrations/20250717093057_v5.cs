using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoSpaceEdu.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepeatGroup",
                table: "FinancialTransactions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepeatGroup",
                table: "FinancialTransactions");
        }
    }
}
