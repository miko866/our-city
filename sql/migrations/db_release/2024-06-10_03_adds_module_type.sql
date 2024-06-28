START TRANSACTION;

ALTER TABLE module_service ADD module_type character varying(256) NOT NULL DEFAULT '';

CREATE UNIQUE INDEX "IX_module_service_module_type" ON module_service (module_type);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240610173500_AddsModuleType', '8.0.4');

COMMIT;

