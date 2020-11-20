using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMat.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendants",
                columns: table => new
                {
                    AttendantId = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendants", x => x.AttendantId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "DisqualifiedLeads",
                columns: table => new
                {
                    DisqualifiedLeadId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentID = table.Column<int>(nullable: false),
                    AttendantID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Situation = table.Column<string>(nullable: true),
                    DateBorn = table.Column<DateTime>(nullable: true),
                    PrevSituation = table.Column<string>(nullable: true),
                    DateJobEnd = table.Column<DateTime>(nullable: true),
                    SegJobReceive = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisqualifiedLeads", x => x.DisqualifiedLeadId);
                });

            migrationBuilder.CreateTable(
                name: "QualifiedLeads",
                columns: table => new
                {
                    QualifiedLeadId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentID = table.Column<int>(nullable: false),
                    AttendantID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Situation = table.Column<string>(nullable: true),
                    DateBorn = table.Column<DateTime>(nullable: true),
                    PrevSituation = table.Column<string>(nullable: true),
                    DateJobEnd = table.Column<DateTime>(nullable: true),
                    SegJobReceive = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifiedLeads", x => x.QualifiedLeadId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendants");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DisqualifiedLeads");

            migrationBuilder.DropTable(
                name: "QualifiedLeads");
        }
    }
}
