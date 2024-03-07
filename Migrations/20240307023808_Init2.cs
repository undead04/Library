using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_documents_DoucmentId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_lessons_LessonId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_UserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_classRooms_ClassRoomId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Major_MajorId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectClassRooms_Tearcher_TearcherId",
                table: "subjectClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Tearcher_AspNetUsers_UserId",
                table: "Tearcher");

            migrationBuilder.DropForeignKey(
                name: "FK_Tearcher_Major_MajorId",
                table: "Tearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tearcher",
                table: "Tearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Major",
                table: "Major");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "resources");

            migrationBuilder.RenameTable(
                name: "Tearcher",
                newName: "tearchers");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "students");

            migrationBuilder.RenameTable(
                name: "Major",
                newName: "majors");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_LessonId",
                table: "resources",
                newName: "IX_resources_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_DoucmentId",
                table: "resources",
                newName: "IX_resources_DoucmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Tearcher_UserId",
                table: "tearchers",
                newName: "IX_tearchers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tearcher_MajorId",
                table: "tearchers",
                newName: "IX_tearchers_MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UserId",
                table: "students",
                newName: "IX_students_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_MajorId",
                table: "students",
                newName: "IX_students_MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_ClassRoomId",
                table: "students",
                newName: "IX_students_ClassRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resources",
                table: "resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tearchers",
                table: "tearchers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_majors",
                table: "majors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_resources_documents_DoucmentId",
                table: "resources",
                column: "DoucmentId",
                principalTable: "documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_resources_lessons_LessonId",
                table: "resources",
                column: "LessonId",
                principalTable: "lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_AspNetUsers_UserId",
                table: "students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classRooms_ClassRoomId",
                table: "students",
                column: "ClassRoomId",
                principalTable: "classRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_majors_MajorId",
                table: "students",
                column: "MajorId",
                principalTable: "majors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherId",
                table: "subjectClassRooms",
                column: "TearcherId",
                principalTable: "tearchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tearchers_AspNetUsers_UserId",
                table: "tearchers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tearchers_majors_MajorId",
                table: "tearchers",
                column: "MajorId",
                principalTable: "majors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resources_documents_DoucmentId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_lessons_LessonId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_students_AspNetUsers_UserId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_classRooms_ClassRoomId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_majors_MajorId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectClassRooms_tearchers_TearcherId",
                table: "subjectClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_tearchers_AspNetUsers_UserId",
                table: "tearchers");

            migrationBuilder.DropForeignKey(
                name: "FK_tearchers_majors_MajorId",
                table: "tearchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resources",
                table: "resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tearchers",
                table: "tearchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_majors",
                table: "majors");

            migrationBuilder.RenameTable(
                name: "resources",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "tearchers",
                newName: "Tearcher");

            migrationBuilder.RenameTable(
                name: "students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "majors",
                newName: "Major");

            migrationBuilder.RenameIndex(
                name: "IX_resources_LessonId",
                table: "Resources",
                newName: "IX_Resources_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_resources_DoucmentId",
                table: "Resources",
                newName: "IX_Resources_DoucmentId");

            migrationBuilder.RenameIndex(
                name: "IX_tearchers_UserId",
                table: "Tearcher",
                newName: "IX_Tearcher_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tearchers_MajorId",
                table: "Tearcher",
                newName: "IX_Tearcher_MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_students_UserId",
                table: "Student",
                newName: "IX_Student_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_students_MajorId",
                table: "Student",
                newName: "IX_Student_MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_students_ClassRoomId",
                table: "Student",
                newName: "IX_Student_ClassRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tearcher",
                table: "Tearcher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Major",
                table: "Major",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_documents_DoucmentId",
                table: "Resources",
                column: "DoucmentId",
                principalTable: "documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_lessons_LessonId",
                table: "Resources",
                column: "LessonId",
                principalTable: "lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_UserId",
                table: "Student",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_classRooms_ClassRoomId",
                table: "Student",
                column: "ClassRoomId",
                principalTable: "classRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Major_MajorId",
                table: "Student",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectClassRooms_Tearcher_TearcherId",
                table: "subjectClassRooms",
                column: "TearcherId",
                principalTable: "Tearcher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tearcher_AspNetUsers_UserId",
                table: "Tearcher",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tearcher_Major_MajorId",
                table: "Tearcher",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id");
        }
    }
}
