using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchiTemplate.Infrastructure.Persistence.Migrations
{
    public partial class PhotourlandReleasedyear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleasedYear",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleasedYear",
                table: "Movies");
        }
    }
}
