using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsEventModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module_event",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    shor_text = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    context = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    url_link = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    video_link = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    date_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_to = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    table.PrimaryKey("pk_module_event", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_event_organisation_module_service_organisation_module_~",
                        column: x => x.organisation_module_service_id,
                        principalTable: "organisation_module_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "module_event_file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_item_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    module_event_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    is_featured_image = table.Column<bool>(type: "boolean", nullable: false),
                    order_nr = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module_event_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_event_file_item_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_event_file_item_module_event_module_event_id",
                        column: x => x.module_event_id,
                        principalTable: "module_event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "module_event_tag",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    tag_id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    module_event_id = table.Column<string>(
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
                    table.PrimaryKey("pk_module_event_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_event_tag_module_event_module_event_id",
                        column: x => x.module_event_id,
                        principalTable: "module_event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_event_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_event_organisation_module_service_id",
                table: "module_event",
                column: "organisation_module_service_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_event_file_item_file_item_id",
                table: "module_event_file_item",
                column: "file_item_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_event_file_item_module_event_id",
                table: "module_event_file_item",
                column: "module_event_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_event_tag_module_event_id",
                table: "module_event_tag",
                column: "module_event_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_event_tag_tag_id",
                table: "module_event_tag",
                column: "tag_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "module_event_file_item");

            migrationBuilder.DropTable(name: "module_event_tag");

            migrationBuilder.DropTable(name: "module_event");
        }
    }
}
