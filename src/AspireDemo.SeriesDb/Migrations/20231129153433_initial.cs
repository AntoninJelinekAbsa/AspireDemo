using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireDemo.SeriesDb.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "actor_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "genre_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "idea_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "special_prop_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Actors = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SpecialProps = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Plot = table.Column<string>(type: "text", maxLength: -1, nullable: true),
                    GreenlightFromBoss = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    BossNotes = table.Column<string>(type: "text", maxLength: -1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialProp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialProp", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Idea");

            migrationBuilder.DropTable(
                name: "SpecialProp");

            migrationBuilder.DropSequence(
                name: "actor_hilo");

            migrationBuilder.DropSequence(
                name: "genre_hilo");

            migrationBuilder.DropSequence(
                name: "idea_hilo");

            migrationBuilder.DropSequence(
                name: "special_prop_hilo");
        }
    }
}
