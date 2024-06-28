START TRANSACTION;

CREATE TABLE module_simple_page (
    id character varying(8000) NOT NULL,
    title character varying(256) NOT NULL,
    context character varying(8000),
    icon character varying(256),
    url_link character varying(8000),
    video_link character varying(8000),
    organisation_module_service_id character varying(8000) NOT NULL,
    created_by character varying(256) NOT NULL,
    updated_by character varying(256),
    created_at timestamp without time zone NOT NULL,
    updated_at timestamp without time zone,
    CONSTRAINT pk_module_simple_page PRIMARY KEY (id),
    CONSTRAINT "fk_module_simple_page_organisation_module_service_organisation_m~" FOREIGN KEY (organisation_module_service_id) REFERENCES organisation_module_service (id) ON DELETE CASCADE
);

CREATE TABLE module_simple_page_file_item (
    id character varying(8000) NOT NULL,
    file_item_id character varying(8000) NOT NULL,
    module_simple_page_id character varying(8000) NOT NULL,
    order_nr integer,
    created_by character varying(256) NOT NULL,
    updated_by character varying(256),
    created_at timestamp without time zone NOT NULL,
    updated_at timestamp without time zone,
    CONSTRAINT pk_module_simple_page_file_item PRIMARY KEY (id),
    CONSTRAINT fk_module_simple_page_file_item_file_item_file_item_id FOREIGN KEY (file_item_id) REFERENCES file_item (id) ON DELETE CASCADE,
    CONSTRAINT "fk_module_simple_page_file_item_module_simple_page_module_simp~" FOREIGN KEY (module_simple_page_id) REFERENCES module_simple_page (id) ON DELETE CASCADE
);

CREATE INDEX "IX_module_simple_page_organisation_module_service_id" ON module_simple_page (organisation_module_service_id);

CREATE INDEX "IX_module_simple_page_file_item_file_item_id" ON module_simple_page_file_item (file_item_id);

CREATE INDEX "IX_module_simple_page_file_item_module_simple_page_id" ON module_simple_page_file_item (module_simple_page_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240610184606_AddsModuleSimplePage', '8.0.4');

COMMIT;

