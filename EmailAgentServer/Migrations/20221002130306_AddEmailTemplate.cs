﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailAgentServer.Migrations
{
    public partial class AddEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TemplateName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdateTime = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailPlaceholders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaceHolder = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPlaceholders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailPlaceholders_EmailTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailPlaceholders_EmailTemplateId",
                table: "EmailPlaceholders",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_TemplateName",
                table: "EmailTemplates",
                column: "TemplateName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailPlaceholders");

            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}
