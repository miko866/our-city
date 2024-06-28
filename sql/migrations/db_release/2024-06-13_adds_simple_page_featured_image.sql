START TRANSACTION;

ALTER TABLE module_simple_page_file_item ADD is_featured_image boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240613184515_AddsSimplePageFeaturedImage', '8.0.6');

COMMIT;