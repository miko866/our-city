START TRANSACTION;

CREATE TABLE module_special_announcement (
                                             id character varying(8000) NOT NULL,
                                             text_message character varying(8000) NOT NULL,
                                             severity text NOT NULL,
                                             url_link character varying(8000),
                                             organisation_module_service_id character varying(8000) NOT NULL,
                                             created_by character varying(256) NOT NULL,
                                             updated_by character varying(256),
                                             created_at timestamp without time zone NOT NULL,
                                             updated_at timestamp without time zone,
                                             CONSTRAINT pk_module_special_announcement PRIMARY KEY (id),
                                             CONSTRAINT "fk_module_special_announcement_organisation_module_service_organ~" FOREIGN KEY (organisation_module_service_id) REFERENCES organisation_module_service (id) ON DELETE CASCADE
);

CREATE INDEX "IX_module_special_announcement_organisation_module_service_id" ON module_special_announcement (organisation_module_service_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240610143732_AddsSpecialAnnouncementModule', '8.0.4');

COMMIT;

