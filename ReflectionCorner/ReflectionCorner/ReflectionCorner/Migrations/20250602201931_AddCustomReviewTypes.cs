using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReflectionCorner.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomReviewTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomReviewTypeId",
                table: "Reviews",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomReviewTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReviewTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomReviewTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomReviewTypeId",
                table: "Reviews",
                column: "CustomReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomReviewTypes_UserId",
                table: "CustomReviewTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CustomReviewTypes_CustomReviewTypeId",
                table: "Reviews",
                column: "CustomReviewTypeId",
                principalTable: "CustomReviewTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CustomReviewTypes_CustomReviewTypeId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "CustomReviewTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomReviewTypeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CustomReviewTypeId",
                table: "Reviews");
        }
    }
}
