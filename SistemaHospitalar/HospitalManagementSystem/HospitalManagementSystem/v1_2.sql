START TRANSACTION;
ALTER TABLE `SystemCodeDetail` MODIFY `ModifiedOn` datetime(6) NULL;

ALTER TABLE `SystemCodeDetail` MODIFY `ModifiedById` varchar(255) NULL;

ALTER TABLE `SystemCode` MODIFY `ModifiedOn` datetime(6) NULL;

ALTER TABLE `SystemCode` MODIFY `ModifiedById` varchar(255) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250619213304_v1_2', '9.0.6');

COMMIT;

