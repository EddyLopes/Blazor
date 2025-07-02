CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) NOT NULL,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` longtext NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) NOT NULL,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext NULL,
    `SecurityStamp` longtext NULL,
    `ConcurrencyStamp` longtext NULL,
    `PhoneNumber` longtext NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) NOT NULL,
    `ProviderKey` varchar(255) NOT NULL,
    `ProviderDisplayName` longtext NULL,
    `UserId` varchar(255) NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) NOT NULL,
    `RoleId` varchar(255) NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `Value` longtext NULL,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Companies` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `LegalName` longtext NOT NULL,
    `TradeName` longtext NOT NULL,
    `TaxId` longtext NOT NULL,
    `PostalCode` longtext NOT NULL,
    `State` longtext NOT NULL,
    `City` longtext NOT NULL,
    `Neighborhood` longtext NOT NULL,
    `Address` longtext NOT NULL,
    `Complement` longtext NOT NULL,
    `CreatedAt` datetime NOT NULL,
    `UserId` varchar(255) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Companies_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Accounts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext NOT NULL,
    `Balance` decimal(18,2) NOT NULL,
    `BalanceDate` datetime(6) NOT NULL,
    `CompanyId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Accounts_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`)
);

CREATE TABLE `Categories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NOT NULL,
    `CompanyId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Categories_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`)
);

CREATE TABLE `FinancialTransactions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TypeFinancialTransaction` longtext NOT NULL,
    `Description` longtext NOT NULL,
    `ReferenceDate` datetime NOT NULL,
    `DueDate` datetime NOT NULL,
    `Amount` decimal(18,2) NOT NULL,
    `Repeat` longtext NOT NULL,
    `RepeatTimes` int NOT NULL,
    `InterestPenalty` decimal(18,2) NOT NULL,
    `Discount` decimal(18,2) NOT NULL,
    `PaymentDate` datetime NOT NULL,
    `AmoundPaid` decimal(18,2) NOT NULL,
    `Observation` longtext NULL,
    `CreatedAt` datetime NOT NULL,
    `CompanyId` int NULL,
    `AccountId` int NULL,
    `CategoryId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_FinancialTransactions_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `Accounts` (`Id`),
    CONSTRAINT `FK_FinancialTransactions_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`),
    CONSTRAINT `FK_FinancialTransactions_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`)
);

CREATE TABLE `Documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Path` longtext NOT NULL,
    `FinancialTransactionId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Documents_FinancialTransactions_FinancialTransactionId` FOREIGN KEY (`FinancialTransactionId`) REFERENCES `FinancialTransactions` (`Id`)
);

CREATE INDEX `IX_Accounts_CompanyId` ON `Accounts` (`CompanyId`);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_Categories_CompanyId` ON `Categories` (`CompanyId`);

CREATE INDEX `IX_Companies_UserId` ON `Companies` (`UserId`);

CREATE INDEX `IX_Documents_FinancialTransactionId` ON `Documents` (`FinancialTransactionId`);

CREATE INDEX `IX_FinancialTransactions_AccountId` ON `FinancialTransactions` (`AccountId`);

CREATE INDEX `IX_FinancialTransactions_CategoryId` ON `FinancialTransactions` (`CategoryId`);

CREATE INDEX `IX_FinancialTransactions_CompanyId` ON `FinancialTransactions` (`CompanyId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250702091802_v1', '9.0.6');

COMMIT;

