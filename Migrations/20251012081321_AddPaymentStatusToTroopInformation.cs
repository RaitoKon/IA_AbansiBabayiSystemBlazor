using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IA_AbansiBabayiSystemBlazor.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatusToTroopInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TroopMemId",
                table: "TroopInformations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "TroopInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "TroopInformations");

            migrationBuilder.AlterColumn<int>(
                name: "TroopMemId",
                table: "TroopInformations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
