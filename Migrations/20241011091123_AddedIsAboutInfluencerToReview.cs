using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfluencersPlatformBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsAboutInfluencerToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAboutInfluencer",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAboutInfluencer",
                table: "Reviews");
        }
    }
}
