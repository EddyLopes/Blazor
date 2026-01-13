START TRANSACTION;
ALTER TABLE `Documents` ADD `Name` longtext NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250715160145_v4', '9.0.6');

COMMIT;

