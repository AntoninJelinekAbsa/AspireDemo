using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireDemo.SeriesDb.Migrations
{
    /// <inheritdoc />
    public partial class furtherideachanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GreenlightFromBoss",
                table: "Idea");

            migrationBuilder.RenameColumn(
                name: "BossNotes",
                table: "Idea",
                newName: "BossReview");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BossReview",
                table: "Idea",
                newName: "BossNotes");

            migrationBuilder.AddColumn<string>(
                name: "GreenlightFromBoss",
                table: "Idea",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
