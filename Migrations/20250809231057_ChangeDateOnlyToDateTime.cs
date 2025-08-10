using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IA_AbansiBabayiSystemBlazor.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateOnlyToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authRoleID",
                table: "REGISTERED_TROOP_LEADER");

            migrationBuilder.AlterColumn<int>(
                name: "troopMemID",
                table: "TROOP_MEMBER_REGISTRATION",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "troopMemBirthdate",
                table: "TROOP_MEMBER_REGISTRATION",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "troopMemTroopNumber",
                table: "TROOP_MEMBER_REGISTRATION",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "leaderRole",
                table: "TROOP_LEADER_REGISTRATION",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "leaderBirthdate",
                table: "TROOP_LEADER_REGISTRATION",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "leaderTorNT",
                table: "REGISTERED_TROOP_LEADER",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "leaderBirthdate",
                table: "REGISTERED_TROOP_LEADER",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TROOP_MEMBER_REGISTRATION",
                table: "TROOP_MEMBER_REGISTRATION",
                column: "troopMemID");

            migrationBuilder.CreateTable(
                name: "REGISTERED_TROOP_MEMBER",
                columns: table => new
                {
                    troopMemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    troopMemRole = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    troopMemScoutNumber = table.Column<int>(type: "int", nullable: true),
                    troopMemLname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    troopMemFname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    troopMemMInitial = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    troopMemBirthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    troopMemGradeOrYear = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    troopMemRegStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    troopMemBeneficiary = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    troopMemEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    troopMemTroopNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTERED_TROOP_MEMBER", x => x.troopMemID);
                });

            migrationBuilder.CreateTable(
                name: "TROOP_MEMBER_ACCOUNTS",
                columns: table => new
                {
                    accountsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    troopMemID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TROOP_ME__4A22408AB573DA3D", x => x.accountsID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTERED_TROOP_MEMBER");

            migrationBuilder.DropTable(
                name: "TROOP_MEMBER_ACCOUNTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TROOP_MEMBER_REGISTRATION",
                table: "TROOP_MEMBER_REGISTRATION");

            migrationBuilder.DropColumn(
                name: "troopMemTroopNumber",
                table: "TROOP_MEMBER_REGISTRATION");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "troopMemBirthdate",
                table: "TROOP_MEMBER_REGISTRATION",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "troopMemID",
                table: "TROOP_MEMBER_REGISTRATION",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "leaderRole",
                table: "TROOP_LEADER_REGISTRATION",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "leaderBirthdate",
                table: "TROOP_LEADER_REGISTRATION",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "leaderTorNT",
                table: "REGISTERED_TROOP_LEADER",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "leaderBirthdate",
                table: "REGISTERED_TROOP_LEADER",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "authRoleID",
                table: "REGISTERED_TROOP_LEADER",
                type: "int",
                nullable: true);
        }
    }
}
