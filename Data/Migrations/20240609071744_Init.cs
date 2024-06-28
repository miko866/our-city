using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    last_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    gender = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: true
                    ),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "data_protection_keys",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    friendly_name = table.Column<string>(type: "text", nullable: true),
                    xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_data_protection_keys", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "file_item_type",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_item_type", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    application = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    logged = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    level = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    message = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    logger = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    callsite = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    exception = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    guid = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "module_service",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    icon = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: true
                    ),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module_service", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "pk_asp_net_user_tokens",
                        x => new
                        {
                            x.user_id,
                            x.login_provider,
                            x.name
                        }
                    );
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    country_id = table.Column<string>(
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
                    table.PrimaryKey("pk_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_name = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_origin_name = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    file_extension = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    href = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    alt_text = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    file_item_type_id = table.Column<string>(
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
                    table.PrimaryKey("pk_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_item_file_item_type_file_item_type_id",
                        column: x => x.file_item_type_id,
                        principalTable: "file_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    state_id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_district", x => x.id);
                    table.ForeignKey(
                        name: "fk_district_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_item_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    application_user_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    is_avatar = table.Column<bool>(type: "boolean", nullable: false),
                    order_nr = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_file_item_asp_net_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_user_file_item_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "organisation",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    zip = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    district_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    description = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: true
                    ),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organisation", x => x.id);
                    table.ForeignKey(
                        name: "fk_organisation_district_district_id",
                        column: x => x.district_id,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "organisation_file_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    file_item_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    organisation_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    is_logo = table.Column<bool>(type: "boolean", nullable: false),
                    order_nr = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organisation_file_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_organisation_file_item_file_item_file_item_id",
                        column: x => x.file_item_id,
                        principalTable: "file_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_organisation_file_item_organisation_organisation_id",
                        column: x => x.organisation_id,
                        principalTable: "organisation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "organisation_module_service",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    module_service_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    organisation_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organisation_module_service", x => x.id);
                    table.ForeignKey(
                        name: "fk_organisation_module_service_module_service_module_service_id",
                        column: x => x.module_service_id,
                        principalTable: "module_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_organisation_module_service_organisation_organisation_id",
                        column: x => x.organisation_id,
                        principalTable: "organisation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_organisation",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    application_user_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    organisation_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_organisation", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_organisation_asp_net_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_user_organisation_organisation_organisation_id",
                        column: x => x.organisation_id,
                        principalTable: "organisation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_organisation_permission",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    user_organisation_id = table.Column<string>(
                        type: "character varying(8000)",
                        maxLength: 8000,
                        nullable: false
                    ),
                    permission_id = table.Column<string>(
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
                    table.PrimaryKey("pk_user_organisation_permission", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_organisation_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_user_organisation_permission_user_organisation_user_organis~",
                        column: x => x.user_organisation_id,
                        principalTable: "user_organisation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_roles_normalized_name",
                table: "asp_net_roles",
                column: "normalized_name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_users_normalized_email",
                table: "asp_net_users",
                column: "normalized_email"
            );

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_users_normalized_user_name",
                table: "asp_net_users",
                column: "normalized_user_name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_country_abbreviation",
                table: "country",
                column: "abbreviation",
                unique: true
            );

            migrationBuilder.CreateIndex(name: "IX_country_name", table: "country", column: "name", unique: true);

            migrationBuilder.CreateIndex(name: "IX_district_state_id", table: "district", column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_item_file_item_type_id",
                table: "file_item",
                column: "file_item_type_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_file_item_file_name",
                table: "file_item",
                column: "file_name",
                unique: true
            );

            migrationBuilder.CreateIndex(name: "IX_file_item_href", table: "file_item", column: "href", unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_file_item_type_name",
                table: "file_item_type",
                column: "name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_module_service_name",
                table: "module_service",
                column: "name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_district_id",
                table: "organisation",
                column: "district_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_name",
                table: "organisation",
                column: "name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_file_item_file_item_id",
                table: "organisation_file_item",
                column: "file_item_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_file_item_organisation_id",
                table: "organisation_file_item",
                column: "organisation_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_module_service_module_service_id",
                table: "organisation_module_service",
                column: "module_service_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_organisation_module_service_organisation_id",
                table: "organisation_module_service",
                column: "organisation_id"
            );

            migrationBuilder.CreateIndex(name: "IX_permission_name", table: "permission", column: "name", unique: true);

            migrationBuilder.CreateIndex(name: "IX_state_country_id", table: "state", column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_file_item_application_user_id",
                table: "user_file_item",
                column: "application_user_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_file_item_file_item_id",
                table: "user_file_item",
                column: "file_item_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_organisation_application_user_id",
                table: "user_organisation",
                column: "application_user_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_organisation_organisation_id",
                table: "user_organisation",
                column: "organisation_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_organisation_permission_permission_id",
                table: "user_organisation_permission",
                column: "permission_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_organisation_permission_user_organisation_id",
                table: "user_organisation_permission",
                column: "user_organisation_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "asp_net_role_claims");

            migrationBuilder.DropTable(name: "asp_net_user_claims");

            migrationBuilder.DropTable(name: "asp_net_user_logins");

            migrationBuilder.DropTable(name: "asp_net_user_roles");

            migrationBuilder.DropTable(name: "asp_net_user_tokens");

            migrationBuilder.DropTable(name: "data_protection_keys");

            migrationBuilder.DropTable(name: "logs");

            migrationBuilder.DropTable(name: "organisation_file_item");

            migrationBuilder.DropTable(name: "organisation_module_service");

            migrationBuilder.DropTable(name: "user_file_item");

            migrationBuilder.DropTable(name: "user_organisation_permission");

            migrationBuilder.DropTable(name: "asp_net_roles");

            migrationBuilder.DropTable(name: "module_service");

            migrationBuilder.DropTable(name: "file_item");

            migrationBuilder.DropTable(name: "permission");

            migrationBuilder.DropTable(name: "user_organisation");

            migrationBuilder.DropTable(name: "file_item_type");

            migrationBuilder.DropTable(name: "asp_net_users");

            migrationBuilder.DropTable(name: "organisation");

            migrationBuilder.DropTable(name: "district");

            migrationBuilder.DropTable(name: "state");

            migrationBuilder.DropTable(name: "country");
        }
    }
}
