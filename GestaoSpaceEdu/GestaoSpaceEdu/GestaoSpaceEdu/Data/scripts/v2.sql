START TRANSACTION;
ALTER TABLE `Accounts` DROP CONSTRAINT `FK_Accounts_Companies_CompanyId`;

ALTER TABLE `FinancialTransactions` ADD `DeletedAt` datetime NULL;

ALTER TABLE `Documents` ADD `CreatedAt` datetime NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00.000';

ALTER TABLE `Documents` ADD `DeletedAt` datetime NULL;

ALTER TABLE `Companies` MODIFY `TaxId` varchar(255) NOT NULL;

ALTER TABLE `Companies` ADD `DeletedAt` datetime NULL;

ALTER TABLE `Categories` ADD `CreatedAt` datetime NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00.000';

ALTER TABLE `Categories` ADD `DeletedAt` datetime NULL;

ALTER TABLE `AspNetUsers` ADD `DeletedAt` datetime NULL;

ALTER TABLE `Accounts` ADD `CreatedAt` datetime NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00.000';

ALTER TABLE `Accounts` ADD `DeletedAt` datetime NULL;

CREATE UNIQUE INDEX `IX_Companies_TaxId` ON `Companies` (`TaxId`);

ALTER TABLE `Accounts` ADD CONSTRAINT `FK_Accounts_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250708105108_v2', '9.0.6');

COMMIT;

