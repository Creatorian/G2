using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gnome.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AwardsAndWhatNot2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "Products",
                type: "nvarchar(max)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Products");
        }
    }
}
