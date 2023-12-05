using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireDemo.SeriesDb.Migrations
{
    /// <inheritdoc />
    public partial class greenlightchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GreenlightFromBoss",
                table: "Idea",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GreenlightFromBoss",
                table: "Idea",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
