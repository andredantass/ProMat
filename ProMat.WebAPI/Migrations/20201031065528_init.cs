using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMat.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisqualifiedQueues",
                columns: table => new
                {
                    DisqualifiedQueueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Situation = table.Column<DateTime>(nullable: false),
                    DateBorn = table.Column<string>(nullable: true),
                    PrevSituation = table.Column<string>(nullable: true),
                    DateJobEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisqualifiedQueues", x => x.DisqualifiedQueueId);
                });

            migrationBuilder.CreateTable(
                name: "QualifiedQueues",
                columns: table => new
                {
                    QualifiedQueueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Situation = table.Column<string>(nullable: true),
                    DateBorn = table.Column<DateTime>(nullable: false),
                    PrevSituation = table.Column<string>(nullable: true),
                    DateJobEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifiedQueues", x => x.QualifiedQueueId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisqualifiedQueues");

            migrationBuilder.DropTable(
                name: "QualifiedQueues");
        }
    }
}
