using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_classResources_classRooms_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_classResources_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_classResources_ClassId",
                table: "classResources",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_classResources_ResourceId",
                table: "classResources",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classResources");
        }
    }
}
