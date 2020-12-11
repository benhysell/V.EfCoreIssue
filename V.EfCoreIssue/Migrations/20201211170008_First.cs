using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace V.EfCoreIssue.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hierarchies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hierarchies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HierarchyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Hierarchies_HierarchyId",
                        column: x => x.HierarchyId,
                        principalTable: "Hierarchies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hierarchies",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("de7ec252-a532-41d0-8566-be20d449874b"), "Hiearchy A" });

            migrationBuilder.InsertData(
                table: "Hierarchies",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("40285c27-ed61-4811-9bed-36d0783897de"), "Hiearchy B" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "HierarchyId", "Name" },
                values: new object[] { new Guid("fd25c099-edd0-43a5-b7db-1e577c4134ee"), new Guid("de7ec252-a532-41d0-8566-be20d449874b"), "Organization 1" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "HierarchyId", "Name" },
                values: new object[] { new Guid("736987f7-487f-4b1c-8c39-60be3d601a85"), new Guid("40285c27-ed61-4811-9bed-36d0783897de"), "Organization 2" });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_HierarchyId",
                table: "Organizations",
                column: "HierarchyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Hierarchies");
        }
    }
}
