using Microsoft.EntityFrameworkCore.Migrations;

namespace SverigesFF_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmstudio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmstudio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    QuantityForRent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RatingNumber = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    FilmstudioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Filmstudio_FilmstudioId",
                        column: x => x.FilmstudioId,
                        principalTable: "Filmstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieId = table.Column<int>(nullable: false),
                    FilmstudioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rental_Filmstudio_FilmstudioId",
                        column: x => x.FilmstudioId,
                        principalTable: "Filmstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trivia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TriviaText = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: false),
                    FilmstudioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trivia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trivia_Filmstudio_FilmstudioId",
                        column: x => x.FilmstudioId,
                        principalTable: "Filmstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trivia_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_FilmstudioId",
                table: "Rating",
                column: "FilmstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_MovieId",
                table: "Rating",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_FilmstudioId",
                table: "Rental",
                column: "FilmstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_MovieId",
                table: "Rental",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Trivia_FilmstudioId",
                table: "Trivia",
                column: "FilmstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trivia_MovieId",
                table: "Trivia",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "Trivia");

            migrationBuilder.DropTable(
                name: "Filmstudio");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
