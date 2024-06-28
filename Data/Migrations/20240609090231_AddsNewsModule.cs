using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsNewsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_logo_mini",
                table: "organisation_file_item",
                type: "boolean",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.CreateTable(
                name: "meta_data",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    key_value = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    meta_value = table.Column<string>(
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
                    table.PrimaryKey("pk_meta_data", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "module_news",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    shor_text = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    context = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
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
                    table.PrimaryKey("pk_module_news", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_news_organisation_module_service_organisation_module_s~",
                        column: x => x.organisation_module_service_id,
                        principalTable: "organisation_module_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "module_news_file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_item_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    module_news_id = table.Column<string>(
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
                    table.PrimaryKey("pk_module_news_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_news_file_item_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_news_file_item_module_news_module_news_id",
                        column: x => x.module_news_id,
                        principalTable: "module_news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "module_news_meta_data",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    meta_data_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    module_news_id = table.Column<string>(
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
                    table.PrimaryKey("pk_module_news_meta_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_news_meta_data_meta_data_meta_data_id",
                        column: x => x.meta_data_id,
                        principalTable: "meta_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_news_meta_data_module_news_module_news_id",
                        column: x => x.module_news_id,
                        principalTable: "module_news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "module_news_tag",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    tag_id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    module_news_id = table.Column<string>(
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
                    table.PrimaryKey("pk_module_news_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_module_news_tag_module_news_module_news_id",
                        column: x => x.module_news_id,
                        principalTable: "module_news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_module_news_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_meta_data_key_value",
                table: "meta_data",
                column: "key_value",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_organisation_module_service_id",
                table: "module_news",
                column: "organisation_module_service_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_file_item_file_item_id",
                table: "module_news_file_item",
                column: "file_item_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_file_item_module_news_id",
                table: "module_news_file_item",
                column: "module_news_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_meta_data_meta_data_id",
                table: "module_news_meta_data",
                column: "meta_data_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_meta_data_module_news_id",
                table: "module_news_meta_data",
                column: "module_news_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_news_tag_module_news_id",
                table: "module_news_tag",
                column: "module_news_id"
            );

            migrationBuilder.CreateIndex(name: "IX_module_news_tag_tag_id", table: "module_news_tag", column: "tag_id");

            migrationBuilder.CreateIndex(name: "IX_tag_name", table: "tag", column: "name", unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "module_news_file_item");

            migrationBuilder.DropTable(name: "module_news_meta_data");

            migrationBuilder.DropTable(name: "module_news_tag");

            migrationBuilder.DropTable(name: "meta_data");

            migrationBuilder.DropTable(name: "module_news");

            migrationBuilder.DropTable(name: "tag");

            migrationBuilder.DropColumn(name: "is_logo_mini", table: "organisation_file_item");
        }
    }
}
