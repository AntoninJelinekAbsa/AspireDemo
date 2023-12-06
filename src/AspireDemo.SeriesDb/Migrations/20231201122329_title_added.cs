using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireDemo.SeriesDb.Migrations
{
    /// <inheritdoc />
    public partial class title_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkingTitle",
                table: "Idea",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingTitle",
                table: "Idea");
        }
    }
}
