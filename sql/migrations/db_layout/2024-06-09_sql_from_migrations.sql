START TRANSACTION;

CREATE TABLE module_event (
    id character varying(8000) NOT NULL,
    title character varying(256) NOT NULL,
    shor_text character varying(600) NOT NULL,
    context character varying(8000),
    url_link character varying(8000),
    video_link character varying(8000),
    date_from timestamp without time zone NOT NULL,
    date_to timestamp without time zone,
    organisation_module_service_id character varying(8000) NOT NULL,
    created_by character varying(256) NOT NULL,
    updated_by character varying(256),
    created_at timestamp without time zone NOT NULL,
    updated_at timestamp without time zone,
    CONSTRAINT pk_module_event PRIMARY KEY (id),
    CONSTRAINT "fk_module_event_organisation_module_service_organisation_module_~" FOREIGN KEY (organisation_module_service_id) REFERENCES organisation_module_service (id) ON DELETE CASCADE
);

CREATE TABLE module_event_file_item (
    id character varying(8000) NOT NULL,
    file_item_id character varying(8000) NOT NULL,
    module_event_id character varying(8000) NOT NULL,
    is_featured_image boolean NOT NULL,
    order_nr integer,
    created_by character varying(256) NOT NULL,
    updated_by character varying(256),
    created_at timestamp without time zone NOT NULL,
    updated_at timestamp without time zone,
    CONSTRAINT pk_module_event_file_item PRIMARY KEY (id),
    CONSTRAINT fk_module_event_file_item_file_item_file_item_id FOREIGN KEY (file_item_id) REFERENCES file_item (id) ON DELETE CASCADE,
    CONSTRAINT fk_module_event_file_item_module_event_module_event_id FOREIGN KEY (module_event_id) REFERENCES module_event (id) ON DELETE CASCADE
);

CREATE TABLE module_event_tag (
    id character varying(8000) NOT NULL,
    tag_id character varying(8000) NOT NULL,
    module_event_id character varying(8000) NOT NULL,
    created_by character varying(256) NOT NULL,
    updated_by character varying(256),
    created_at timestamp without time zone NOT NULL,
    updated_at timestamp without time zone,
    CONSTRAINT pk_module_event_tag PRIMARY KEY (id),
    CONSTRAINT fk_module_event_tag_module_event_module_event_id FOREIGN KEY (module_event_id) REFERENCES module_event (id) ON DELETE CASCADE,
    CONSTRAINT fk_module_event_tag_tag_tag_id FOREIGN KEY (tag_id) REFERENCES tag (id) ON DELETE CASCADE
);

CREATE INDEX "IX_module_event_organisation_module_service_id" ON module_event (organisation_module_service_id);

CREATE INDEX "IX_module_event_file_item_file_item_id" ON module_event_file_item (file_item_id);

CREATE INDEX "IX_module_event_file_item_module_event_id" ON module_event_file_item (module_event_id);

CREATE INDEX "IX_module_event_tag_module_event_id" ON module_event_tag (module_event_id);

CREATE INDEX "IX_module_event_tag_tag_id" ON module_event_tag (tag_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240609141335_AddsEventModule', '8.0.4');

COMMIT;

