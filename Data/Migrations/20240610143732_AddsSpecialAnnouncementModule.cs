using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsSpecialAnnouncementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module_special_announcement",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    text_message = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    severity = table.Column<string>(type: "text", nullable: false),
                    url_link = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    organisation_module_service_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module_special_announcement", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_special_announcement_organisation_module_service_organ~",
                        column: x => x.organisation_module_service_id,
                        principalTable: "organisation_module_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_special_announcement_organisation_module_service_id",
                table: "module_special_announcement",
                column: "organisation_module_service_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "module_special_announcement");
        }
    }
}
