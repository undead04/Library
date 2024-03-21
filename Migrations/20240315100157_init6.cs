using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EssayExam_exams_ExamId",
                table: "EssayExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EssayExam",
                table: "EssayExam");

            migrationBuilder.RenameTable(
                name: "EssayExam",
                newName: "essayExams");

            migrationBuilder.RenameIndex(
                name: "IX_EssayExam_ExamId",
                table: "essayExams",
                newName: "IX_essayExams_ExamId");

            migrationBuilder.AddColumn<int>(
                name: "Subjectid",
                table: "exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_essayExams",
                table: "essayExams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_exams_Subjectid",
                table: "exams",
                column: "Subjectid");

            migrationBuilder.AddForeignKey(
                name: "FK_essayExams_exams_ExamId",
                table: "essayExams",
                column: "ExamId",
                principalTable: "exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_exams_subjects_Subjectid",
                table: "exams",
                column: "Subjectid",
                principalTable: "subjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_essayExams_exams_ExamId",
                table: "essayExams");

            migrationBuilder.DropForeignKey(
                name: "FK_exams_subjects_Subjectid",
                table: "exams");

            migrationBuilder.DropIndex(
                name: "IX_exams_Subjectid",
                table: "exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_essayExams",
                table: "essayExams");

            migrationBuilder.DropColumn(
                name: "Subjectid",
                table: "exams");

            migrationBuilder.RenameTable(
                name: "essayExams",
                newName: "EssayExam");

            migrationBuilder.RenameIndex(
                name: "IX_essayExams_ExamId",
                table: "EssayExam",
                newName: "IX_EssayExam_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EssayExam",
                table: "EssayExam",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EssayExam_exams_ExamId",
                table: "EssayExam",
                column: "ExamId",
                principalTable: "exams",
                principalColumn: "Id");
        }
    }
}
