START TRANSACTION;
ALTER TABLE `FinancialTransactions` MODIFY `RepeatTimes` int NULL;

ALTER TABLE `FinancialTransactions` MODIFY `PaymentDate` datetime NULL;

ALTER TABLE `FinancialTransactions` MODIFY `InterestPenalty` decimal(18,2) NULL;

ALTER TABLE `FinancialTransactions` MODIFY `DueDate` datetime NULL;

ALTER TABLE `FinancialTransactions` MODIFY `Discount` decimal(18,2) NULL;

ALTER TABLE `FinancialTransactions` MODIFY `Amount` decimal(18,2) NULL;

ALTER TABLE `FinancialTransactions` MODIFY `AmoundPaid` decimal(18,2) NULL;

ALTER TABLE `Accounts` MODIFY `BalanceDate` datetime NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250715084508_v3', '9.0.6');

COMMIT;

