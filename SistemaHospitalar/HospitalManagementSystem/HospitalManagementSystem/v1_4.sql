START TRANSACTION;
CREATE TABLE `Countries` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Code` longtext NOT NULL,
    `Name` longtext NOT NULL,
    `ModifiedById` varchar(255) NULL,
    `ModifiedOn` datetime(6) NULL,
    `CreatedById` varchar(255) NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Countries_AspNetUsers_CreatedById` FOREIGN KEY (`CreatedById`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Countries_AspNetUsers_ModifiedById` FOREIGN KEY (`ModifiedById`) REFERENCES `AspNetUsers` (`Id`)
);

CREATE INDEX `IX_Countries_CreatedById` ON `Countries` (`CreatedById`);

CREATE INDEX `IX_Countries_ModifiedById` ON `Countries` (`ModifiedById`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250621095509_v1_4', '9.0.6');

COMMIT;

