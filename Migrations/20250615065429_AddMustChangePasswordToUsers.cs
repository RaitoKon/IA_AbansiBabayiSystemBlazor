using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IA_AbansiBabayiSystemBlazor.Migrations
{
    /// <inheritdoc />
    public partial class AddMustChangePasswordToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
            name: "MustChangePassword",
            table: "AspNetUsers",
            type: "bit",
            nullable: false,
            defaultValue: true
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "MustChangePassword",
            table: "AspNetUsers");
        }
    }
}
