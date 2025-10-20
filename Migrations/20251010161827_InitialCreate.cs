using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IA_AbansiBabayiSystemBlazor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    AccountStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AccountStatusDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.AccountStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AddEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventImagePath = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    EventLocation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddEvents", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaderPosition",
                columns: table => new
                {
                    LeaderPositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaderPosition = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderRole", x => x.LeaderPositionId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TroopDetails",
                columns: table => new
                {
                    TroopDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TroopName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TroopAgeLevel = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    TroopAddress = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TroopSponsoringGroup = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TroopTroopTelNo = table.Column<int>(type: "int", nullable: false),
                    TroopMailingAddress = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TroopDistrictCommittee = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TroopBarangayCommittee = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TroopBirthdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TroopType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TroopStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TroopDateApplied = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TroopDetails", x => x.TroopDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "TroopMembers",
                columns: table => new
                {
                    TroopMemId = table.Column<int>(type: "int", nullable: false),
                    TroopMemRole = table.Column<int>(type: "int", nullable: false),
                    TroopMemScoutNumber = table.Column<int>(type: "int", nullable: true),
                    TroopMemLname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TroopMemFname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TroopMemMname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TroopMemBirthdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TroopMemGradeOrYear = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TroopMemRegStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TroopMemBeneficiary = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    TroopMemEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TroopMemEmailRegistered = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TroopMemDateRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredTroopMembers", x => x.TroopMemId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    AccountStatusId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AccountStatus",
                        column: x => x.AccountStatusId,
                        principalTable: "AccountStatus",
                        principalColumn: "AccountStatusId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ProductStock = table.Column<int>(type: "int", nullable: true),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: true),
                    ProductDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ProductImagePath = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory",
                        column: x => x.ProductCategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TroopLeaders",
                columns: table => new
                {
                    LeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaderPositionId = table.Column<int>(type: "int", nullable: true),
                    LeaderRole = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderTorNT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderRegStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderLname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderFname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderMname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeaderBirthdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LeaderBeneficiary = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    LeaderEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LeaderRegisteredEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LeaderDateRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    LeaderTroopNo = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredTroopLeaders", x => x.LeaderId);
                    table.ForeignKey(
                        name: "FK_TroopLeaders_AspNetUsers",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TroopLeaders_LeaderPosition",
                        column: x => x.LeaderPositionId,
                        principalTable: "LeaderPosition",
                        principalColumn: "LeaderPositionId");
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchases",
                columns: table => new
                {
                    ProductPurchaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ProductPurchasePrice = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ProductPurchaseQuantity = table.Column<int>(type: "int", nullable: true),
                    ProductPurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(19,2)", nullable: true, computedColumnSql: "([ProductPurchaseQuantity]*[ProductPurchasePrice])", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchases", x => x.ProductPurchaseID);
                    table.ForeignKey(
                        name: "FK_ProductPurchases_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    ProductSaleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ProductSalePrice = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ProductSaleQuantity = table.Column<int>(type: "int", nullable: true),
                    ProductSaleDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(19,2)", nullable: true, computedColumnSql: "([ProductSaleQuantity]*[ProductSalePrice])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.ProductSaleID);
                    table.ForeignKey(
                        name: "FK_ProductSales_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "TroopInformations",
                columns: table => new
                {
                    TroopInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TroopNo = table.Column<int>(type: "int", nullable: true),
                    TroopDetailsId = table.Column<int>(type: "int", nullable: true),
                    TroopLeaderId = table.Column<int>(type: "int", nullable: true),
                    TroopMemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TroopInformations", x => x.TroopInfoId);
                    table.ForeignKey(
                        name: "FK_TroopInformations_RegisteredTroopLeaders_TroopLeaderId",
                        column: x => x.TroopLeaderId,
                        principalTable: "TroopLeaders",
                        principalColumn: "LeaderId");
                    table.ForeignKey(
                        name: "FK_TroopInformations_RegisteredTroopMembers_TroopMemId",
                        column: x => x.TroopMemId,
                        principalTable: "TroopMembers",
                        principalColumn: "TroopMemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TroopInformations_TroopDetails_TroopDetailsId",
                        column: x => x.TroopDetailsId,
                        principalTable: "TroopDetails",
                        principalColumn: "TroopDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccountStatusId",
                table: "AspNetUsers",
                column: "AccountStatusId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryID",
                table: "Product",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchases_ProductID",
                table: "ProductPurchases",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductID",
                table: "ProductSales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TroopInformations_TroopDetailsId",
                table: "TroopInformations",
                column: "TroopDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopInformations_TroopLeaderId",
                table: "TroopInformations",
                column: "TroopLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopInformations_TroopMemId",
                table: "TroopInformations",
                column: "TroopMemId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopLeaders_ApplicationUserId",
                table: "TroopLeaders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopLeaders_LeaderPositionId",
                table: "TroopLeaders",
                column: "LeaderPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopMembers_ApplicationUserId",
                table: "TroopMembers",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductPurchases");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "TroopInformations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "TroopLeaders");

            migrationBuilder.DropTable(
                name: "TroopMembers");

            migrationBuilder.DropTable(
                name: "TroopDetails");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LeaderPosition");

            migrationBuilder.DropTable(
                name: "AccountStatus");
        }
    }
}
