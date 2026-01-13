START TRANSACTION;
ALTER TABLE `FinancialTransactions` ADD `RepeatGroup` int NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250717093057_v5', '9.0.6');

COMMIT;

