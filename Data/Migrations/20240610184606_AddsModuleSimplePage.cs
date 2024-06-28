using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsModuleSimplePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module_simple_page",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    context = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    icon = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    url_link = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    video_link = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
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
                    table.PrimaryKey("pk_module_simple_page", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_simple_page_organisation_module_service_organisation_m~",
                        column: x => x.organisation_module_service_id,
                        principalTable: "organisation_module_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "module_simple_page_file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_item_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    module_simple_page_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    order_nr = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module_simple_page_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_simple_page_file_item_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_simple_page_file_item_module_simple_page_module_simp~",
                        column: x => x.module_simple_page_id,
                        principalTable: "module_simple_page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_simple_page_organisation_module_service_id",
                table: "module_simple_page",
                column: "organisation_module_service_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_simple_page_file_item_file_item_id",
                table: "module_simple_page_file_item",
                column: "file_item_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_simple_page_file_item_module_simple_page_id",
                table: "module_simple_page_file_item",
                column: "module_simple_page_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "module_simple_page_file_item");

            migrationBuilder.DropTable(name: "module_simple_page");
        }
    }
}
