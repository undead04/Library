using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class Init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherId",
                table: "subjectClassRooms");

            migrationBuilder.DropIndex(
                name: "IX_subjectClassRooms_TearcherId",
                table: "subjectClassRooms");

            migrationBuilder.DropColumn(
                name: "TearcherId",
                table: "subjectClassRooms");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "questionSubjects");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TearcherUserId",
                table: "subjectClassRooms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByUserId",
                table: "documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateCancel",
                table: "documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "historyLikes",
                columns: table => new
                {
                    SubjectQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historyLikes", x => new { x.UserId, x.SubjectQuestionId });
                    table.ForeignKey(
                        name: "FK_historyLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_historyLikes_questionSubjects_SubjectQuestionId",
                        column: x => x.SubjectQuestionId,
                        principalTable: "questionSubjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "privateFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_privateFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_privateFiles_AspNetUsers_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_subjects_UserId",
                table: "subjects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectClassRooms_TearcherUserId",
                table: "subjectClassRooms",
                column: "TearcherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_historyLikes_SubjectQuestionId",
                table: "historyLikes",
                column: "SubjectQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_privateFiles_CreateUserId",
                table: "privateFiles",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherUserId",
                table: "subjectClassRooms",
                column: "TearcherUserId",
                principalTable: "tearchers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_AspNetUsers_UserId",
                table: "subjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherUserId",
                table: "subjectClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_subjects_AspNetUsers_UserId",
                table: "subjects");

            migrationBuilder.DropTable(
                name: "historyLikes");

            migrationBuilder.DropTable(
                name: "privateFiles");

            migrationBuilder.DropIndex(
                name: "IX_subjects_UserId",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_subjectClassRooms_TearcherUserId",
                table: "subjectClassRooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "TearcherUserId",
                table: "subjectClassRooms");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "exams");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "exams");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "CreateCancel",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "documents");

            migrationBuilder.AddColumn<string>(
                name: "TearcherId",
                table: "subjectClassRooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "questionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_subjectClassRooms_TearcherId",
                table: "subjectClassRooms",
                column: "TearcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherId",
                table: "subjectClassRooms",
                column: "TearcherId",
                principalTable: "tearchers",
                principalColumn: "UserId");
        }
    }
}
