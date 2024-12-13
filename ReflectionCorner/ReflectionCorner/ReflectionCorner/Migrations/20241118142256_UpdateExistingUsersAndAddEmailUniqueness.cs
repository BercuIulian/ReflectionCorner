using Microsoft.EntityFrameworkCore.Migrations;

namespace ReflectionCorner.Migrations
{
    public partial class UpdateExistingUsersAndAddEmailUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing records with a placeholder email
            migrationBuilder.Sql(
                @"UPDATE Users 
                SET Email = 'user_' + CAST(Id AS NVARCHAR(10)) + '@example.com' 
                WHERE Email = '' OR Email IS NULL");

            // Now add the unique index
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");
        }
    }
}