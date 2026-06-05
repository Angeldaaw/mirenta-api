using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRenta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerUniqueEmailPerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId_Email",
                table: "Owners",
                columns: new[] { "UserId", "Email" },
                unique: true,
                filter: "[IsActive] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId_Email",
                table: "Owners");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");
        }
    }
}
