START TRANSACTION;

ALTER TABLE organisation_file_item ADD is_logo_mini boolean NOT NULL DEFAULT FALSE;

CREATE TABLE meta_data (
                           id character varying(8000) NOT NULL,
                           key_value character varying(256) NOT NULL,
                           meta_value character varying(8000) NOT NULL,
                           created_by character varying(256) NOT NULL,
                           updated_by character varying(256),
                           created_at timestamp without time zone NOT NULL,
                           updated_at timestamp without time zone,
                           CONSTRAINT pk_meta_data PRIMARY KEY (id)
);

CREATE TABLE module_news (
                             id character varying(8000) NOT NULL,
                             title character varying(256) NOT NULL,
                             shor_text character varying(600) NOT NULL,
                             context character varying(8000),
                             url_link character varying(8000),
                             video_link character varying(8000),
                             organisation_module_service_id character varying(8000) NOT NULL,
                             created_by character varying(256) NOT NULL,
                             updated_by character varying(256),
                             created_at timestamp without time zone NOT NULL,
                             updated_at timestamp without time zone,
                             CONSTRAINT pk_module_news PRIMARY KEY (id),
                             CONSTRAINT "fk_module_news_organisation_module_service_organisation_module_s" FOREIGN KEY (organisation_module_service_id) REFERENCES organisation_module_service (id) ON DELETE CASCADE
);

CREATE TABLE tag (
                     id character varying(8000) NOT NULL,
                     name character varying(256) NOT NULL,
                     color character varying(7),
                     created_by character varying(256) NOT NULL,
                     updated_by character varying(256),
                     created_at timestamp without time zone NOT NULL,
                     updated_at timestamp without time zone,
                     CONSTRAINT pk_tag PRIMARY KEY (id)
);

CREATE TABLE module_news_file_item (
                                       id character varying(8000) NOT NULL,
                                       file_item_id character varying(8000) NOT NULL,
                                       module_news_id character varying(8000) NOT NULL,
                                       is_featured_image boolean NOT NULL,
                                       order_nr integer,
                                       created_by character varying(256) NOT NULL,
                                       updated_by character varying(256),
                                       created_at timestamp without time zone NOT NULL,
                                       updated_at timestamp without time zone,
                                       CONSTRAINT pk_module_news_file_item PRIMARY KEY (id),
                                       CONSTRAINT fk_module_news_file_item_file_item_file_item_id FOREIGN KEY (file_item_id) REFERENCES file_item (id) ON DELETE CASCADE,
                                       CONSTRAINT fk_module_news_file_item_module_news_module_news_id FOREIGN KEY (module_news_id) REFERENCES module_news (id) ON DELETE CASCADE
);

CREATE TABLE module_news_meta_data (
                                       id character varying(8000) NOT NULL,
                                       meta_data_id character varying(8000) NOT NULL,
                                       module_news_id character varying(8000) NOT NULL,
                                       created_by character varying(256) NOT NULL,
                                       updated_by character varying(256),
                                       created_at timestamp without time zone NOT NULL,
                                       updated_at timestamp without time zone,
                                       CONSTRAINT pk_module_news_meta_data PRIMARY KEY (id),
                                       CONSTRAINT fk_module_news_meta_data_meta_data_meta_data_id FOREIGN KEY (meta_data_id) REFERENCES meta_data (id) ON DELETE CASCADE,
                                       CONSTRAINT fk_module_news_meta_data_module_news_module_news_id FOREIGN KEY (module_news_id) REFERENCES module_news (id) ON DELETE CASCADE
);

CREATE TABLE module_news_tag (
                                 id character varying(8000) NOT NULL,
                                 tag_id character varying(8000) NOT NULL,
                                 module_news_id character varying(8000) NOT NULL,
                                 created_by character varying(256) NOT NULL,
                                 updated_by character varying(256),
                                 created_at timestamp without time zone NOT NULL,
                                 updated_at timestamp without time zone,
                                 CONSTRAINT pk_module_news_tag PRIMARY KEY (id),
                                 CONSTRAINT fk_module_news_tag_module_news_module_news_id FOREIGN KEY (module_news_id) REFERENCES module_news (id) ON DELETE CASCADE,
                                 CONSTRAINT fk_module_news_tag_tag_tag_id FOREIGN KEY (tag_id) REFERENCES tag (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_meta_data_key_value" ON meta_data (key_value);

CREATE INDEX "IX_module_news_organisation_module_service_id" ON module_news (organisation_module_service_id);

CREATE INDEX "IX_module_news_file_item_file_item_id" ON module_news_file_item (file_item_id);

CREATE INDEX "IX_module_news_file_item_module_news_id" ON module_news_file_item (module_news_id);

CREATE INDEX "IX_module_news_meta_data_meta_data_id" ON module_news_meta_data (meta_data_id);

CREATE INDEX "IX_module_news_meta_data_module_news_id" ON module_news_meta_data (module_news_id);

CREATE INDEX "IX_module_news_tag_module_news_id" ON module_news_tag (module_news_id);

CREATE INDEX "IX_module_news_tag_tag_id" ON module_news_tag (tag_id);

CREATE UNIQUE INDEX "IX_tag_name" ON tag (name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240609090231_AddsNewsModule', '8.0.4');

COMMIT;

---
