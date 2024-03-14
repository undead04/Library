using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_replyQuestions_AspNetUsers_UserId1",
                table: "replyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_replyQuestions_questionSubjects_QuestionSubjectId",
                table: "replyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_replyQuestions_QuestionSubjectId",
                table: "replyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_replyQuestions_UserId1",
                table: "replyQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionSubjectId",
                table: "replyQuestions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "replyQuestions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "replyQuestions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "replyQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreateUserId",
                table: "documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "classLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_classLessons_classRooms_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_classLessons_lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_replyQuestions_QuestionId",
                table: "replyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_replyQuestions_UserId",
                table: "replyQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_CreateUserId",
                table: "documents",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_classLessons_ClassId",
                table: "classLessons",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_classLessons_LessonId",
                table: "classLessons",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_AspNetUsers_CreateUserId",
                table: "documents",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_replyQuestions_AspNetUsers_UserId",
                table: "replyQuestions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_replyQuestions_questionSubjects_QuestionId",
                table: "replyQuestions",
                column: "QuestionId",
                principalTable: "questionSubjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_AspNetUsers_CreateUserId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_replyQuestions_AspNetUsers_UserId",
                table: "replyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_replyQuestions_questionSubjects_QuestionId",
                table: "replyQuestions");

            migrationBuilder.DropTable(
                name: "classLessons");

            migrationBuilder.DropIndex(
                name: "IX_replyQuestions_QuestionId",
                table: "replyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_replyQuestions_UserId",
                table: "replyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_documents_CreateUserId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "replyQuestions");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "documents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "replyQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "QuestionSubjectId",
                table: "replyQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "replyQuestions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_replyQuestions_QuestionSubjectId",
                table: "replyQuestions",
                column: "QuestionSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_replyQuestions_UserId1",
                table: "replyQuestions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_replyQuestions_AspNetUsers_UserId1",
                table: "replyQuestions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_replyQuestions_questionSubjects_QuestionSubjectId",
                table: "replyQuestions",
                column: "QuestionSubjectId",
                principalTable: "questionSubjects",
                principalColumn: "Id");
        }
    }
}
