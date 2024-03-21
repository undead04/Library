using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionExams_questions_Examid",
                table: "questionExams");

            migrationBuilder.CreateIndex(
                name: "IX_questionExams_QuestionId",
                table: "questionExams",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_questionExams_questions_QuestionId",
                table: "questionExams",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionExams_questions_QuestionId",
                table: "questionExams");

            migrationBuilder.DropIndex(
                name: "IX_questionExams_QuestionId",
                table: "questionExams");

            migrationBuilder.AddForeignKey(
                name: "FK_questionExams_questions_Examid",
                table: "questionExams",
                column: "Examid",
                principalTable: "questions",
                principalColumn: "Id");
        }
    }
}
