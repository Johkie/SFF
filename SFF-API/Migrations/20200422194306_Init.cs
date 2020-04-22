using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SFF_API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmClubs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    RentalLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmClubModelId = table.Column<int>(nullable: false),
                    MovieModelId = table.Column<int>(nullable: false),
                    RentalDate = table.Column<DateTime>(nullable: false),
                    RentalActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalLog_FilmClubs_FilmClubModelId",
                        column: x => x.FilmClubModelId,
                        principalTable: "FilmClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalLog_Movies_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmClubModelId = table.Column<int>(nullable: false),
                    MovieModelId = table.Column<int>(nullable: false),
                    RentalModelId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRatings_FilmClubs_FilmClubModelId",
                        column: x => x.FilmClubModelId,
                        principalTable: "FilmClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRatings_Movies_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRatings_RentalLog_RentalModelId",
                        column: x => x.RentalModelId,
                        principalTable: "RentalLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTrivias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmClubModelId = table.Column<int>(nullable: false),
                    MovieModelId = table.Column<int>(nullable: false),
                    RentalModelId = table.Column<int>(nullable: false),
                    Trivia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTrivias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieTrivias_FilmClubs_FilmClubModelId",
                        column: x => x.FilmClubModelId,
                        principalTable: "FilmClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTrivias_Movies_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTrivias_RentalLog_RentalModelId",
                        column: x => x.RentalModelId,
                        principalTable: "RentalLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_FilmClubModelId",
                table: "MovieRatings",
                column: "FilmClubModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_MovieModelId",
                table: "MovieRatings",
                column: "MovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_RentalModelId",
                table: "MovieRatings",
                column: "RentalModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieTrivias_FilmClubModelId",
                table: "MovieTrivias",
                column: "FilmClubModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTrivias_MovieModelId",
                table: "MovieTrivias",
                column: "MovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTrivias_RentalModelId",
                table: "MovieTrivias",
                column: "RentalModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalLog_FilmClubModelId",
                table: "RentalLog",
                column: "FilmClubModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalLog_MovieModelId",
                table: "RentalLog",
                column: "MovieModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRatings");

            migrationBuilder.DropTable(
                name: "MovieTrivias");

            migrationBuilder.DropTable(
                name: "RentalLog");

            migrationBuilder.DropTable(
                name: "FilmClubs");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
