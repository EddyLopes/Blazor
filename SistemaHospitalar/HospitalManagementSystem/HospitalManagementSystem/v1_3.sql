START TRANSACTION;
ALTER TABLE `SystemCode` DROP CONSTRAINT `FK_SystemCode_AspNetUsers_CreatedById`;

ALTER TABLE `SystemCodeDetail` DROP COLUMN `CreateById`;

ALTER TABLE `SystemCode` DROP COLUMN `CreateById`;

ALTER TABLE `SystemCode` MODIFY `CreatedById` varchar(255) NOT NULL DEFAULT '';

CREATE TABLE `Departaments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Code` longtext NOT NULL,
    `Name` longtext NOT NULL,
    `ModifiedById` varchar(255) NULL,
    `ModifiedOn` datetime(6) NULL,
    `CreatedById` varchar(255) NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Departaments_AspNetUsers_CreatedById` FOREIGN KEY (`CreatedById`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Departaments_AspNetUsers_ModifiedById` FOREIGN KEY (`ModifiedById`) REFERENCES `AspNetUsers` (`Id`)
);

CREATE INDEX `IX_Departaments_CreatedById` ON `Departaments` (`CreatedById`);

CREATE INDEX `IX_Departaments_ModifiedById` ON `Departaments` (`ModifiedById`);

ALTER TABLE `SystemCode` ADD CONSTRAINT `FK_SystemCode_AspNetUsers_CreatedById` FOREIGN KEY (`CreatedById`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250621095116_v1_3', '9.0.6');

COMMIT;

