using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabularyQuiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizzSession",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quizz_session_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "QuizzParticipation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizzSessionId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quizz_participation_pkey", x => x.id);
                    table.ForeignKey(
                        name: "quizz_session_participation_fk",
                        column: x => x.QuizzSessionId,
                        principalTable: "QuizzSession",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizzSessionQuestion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    QuizzSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quizz_session_question_pkey", x => x.id);
                    table.ForeignKey(
                        name: "quizz_session_question_fk",
                        column: x => x.QuizzSessionId,
                        principalTable: "QuizzSession",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizzSessionQuestionAnswer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizzQuestionId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quizz_session_question_answer_pkey", x => x.id);
                    table.ForeignKey(
                        name: "quizz_session_question_answer_fk",
                        column: x => x.QuizzQuestionId,
                        principalTable: "QuizzSessionQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizzParticipation_QuizzSessionId",
                table: "QuizzParticipation",
                column: "QuizzSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzSessionQuestion_QuizzSessionId",
                table: "QuizzSessionQuestion",
                column: "QuizzSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzSessionQuestionAnswer_QuizzQuestionId",
                table: "QuizzSessionQuestionAnswer",
                column: "QuizzQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizzParticipation");

            migrationBuilder.DropTable(
                name: "QuizzSessionQuestionAnswer");

            migrationBuilder.DropTable(
                name: "QuizzSessionQuestion");

            migrationBuilder.DropTable(
                name: "QuizzSession");
        }
    }
}
