using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfluencersPlatformBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedFollowerCountsToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowersCountFrom",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowersCountTo",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowersCountFrom",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FollowersCountTo",
                table: "Categories");
        }
    }
}
