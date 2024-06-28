START TRANSACTION;

CREATE TABLE message (
                         id character varying(8000) NOT NULL,
                         text_message character varying(8000) NOT NULL,
                         category character varying(256) NOT NULL,
                         created_by character varying(256) NOT NULL,
                         updated_by character varying(256),
                         created_at timestamp without time zone NOT NULL,
                         updated_at timestamp without time zone,
                         CONSTRAINT pk_message PRIMARY KEY (id)
);

CREATE TABLE module_municipal_radio (
                                        id character varying(8000) NOT NULL,
                                        shor_text character varying(600) NOT NULL,
                                        organisation_module_service_id character varying(8000) NOT NULL,
                                        created_by character varying(256) NOT NULL,
                                        updated_by character varying(256),
                                        created_at timestamp without time zone NOT NULL,
                                        updated_at timestamp without time zone,
                                        CONSTRAINT pk_module_municipal_radio PRIMARY KEY (id),
                                        CONSTRAINT "fk_module_municipal_radio_organisation_module_service_organisati" FOREIGN KEY (organisation_module_service_id) REFERENCES organisation_module_service (id) ON DELETE CASCADE
);

CREATE TABLE module_municipal_radio_message (
                                                id character varying(8000) NOT NULL,
                                                message_id character varying(8000) NOT NULL,
                                                module_municipal_radio_id character varying(8000) NOT NULL,
                                                created_by character varying(256) NOT NULL,
                                                updated_by character varying(256),
                                                created_at timestamp without time zone NOT NULL,
                                                updated_at timestamp without time zone,
                                                CONSTRAINT pk_module_municipal_radio_message PRIMARY KEY (id),
                                                CONSTRAINT fk_module_municipal_radio_message_message_message_id FOREIGN KEY (message_id) REFERENCES message (id) ON DELETE CASCADE,
                                                CONSTRAINT "fk_module_municipal_radio_message_module_municipal_radio_modul~" FOREIGN KEY (module_municipal_radio_id) REFERENCES module_municipal_radio (id) ON DELETE CASCADE
);

CREATE INDEX "IX_module_municipal_radio_organisation_module_service_id" ON module_municipal_radio (organisation_module_service_id);

CREATE INDEX "IX_module_municipal_radio_message_message_id" ON module_municipal_radio_message (message_id);

CREATE INDEX "IX_module_municipal_radio_message_module_municipal_radio_id" ON module_municipal_radio_message (module_municipal_radio_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240610125901_AddsRadioModule', '8.0.4');

COMMIT;