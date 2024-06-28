START TRANSACTION;

ALTER TABLE module_simple_page ADD name character varying(256) NOT NULL DEFAULT '';

CREATE UNIQUE INDEX "IX_module_simple_page_name" ON module_simple_page (name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240612204105_AddsNameIntoSimplePage', '8.0.6');

COMMIT;

