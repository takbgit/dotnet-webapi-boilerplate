using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class ProductCategoriesInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Identity",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "Group",
                schema: "Identity",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Identity",
                table: "RoleClaims");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<Guid>(
                name: "JobTypeId",
                schema: "Catalog",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryId",
                schema: "Catalog",
                table: "Products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<decimal>(
                name: "RetailExTaxPrice",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RetailTaxPrice",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "WholesaleExTaxPrice",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WholesaleTaxPrice",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "JobTypeCategories",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypeCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductCategoryId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalSchema: "Catalog",
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JobTypes",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTypeCategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTypes_JobTypeCategories_JobTypeCategoryId",
                        column: x => x.JobTypeCategoryId,
                        principalSchema: "Catalog",
                        principalTable: "JobTypeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_JobTypeId",
                schema: "Catalog",
                table: "Products",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                schema: "Catalog",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_JobTypeCategoryId",
                schema: "Catalog",
                table: "JobTypes",
                column: "JobTypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoryId",
                schema: "Catalog",
                table: "ProductCategories",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_JobTypes_JobTypeId",
                schema: "Catalog",
                table: "Products",
                column: "JobTypeId",
                principalSchema: "Catalog",
                principalTable: "JobTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                schema: "Catalog",
                table: "Products",
                column: "ProductCategoryId",
                principalSchema: "Catalog",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_JobTypes_JobTypeId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropTable(
                name: "JobTypes",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "JobTypeCategories",
                schema: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Products_JobTypeId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RetailExTaxPrice",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RetailTaxPrice",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WholesaleExTaxPrice",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WholesaleTaxPrice",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Identity",
                table: "RoleClaims",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                schema: "Identity",
                table: "RoleClaims",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "RoleClaims",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Identity",
                table: "RoleClaims",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                schema: "Catalog",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);
        }
    }
}
