using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfluencersPlatformBackend.Migrations
{
    /// <inheritdoc />
    public partial class TryingWithUsersAndProfilesFourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InfluencerProfiles_UserId",
                table: "InfluencerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_UserId",
                table: "CompanyProfiles");

            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<int>(
                name: "CompanyProfileId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InfluencerProfileId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerProfiles_UserId",
                table: "InfluencerProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_UserId",
                table: "CompanyProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InfluencerProfiles_UserId",
                table: "InfluencerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_UserId",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "CompanyProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InfluencerProfileId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerProfiles_UserId",
                table: "InfluencerProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_UserId",
                table: "CompanyProfiles",
                column: "UserId");
        }
    }
}
