USE `MS-Demo-Products-db`;

CREATE TABLE `Tbl_Products` (
            `Id` CHAR(36) NOT NULL,
            `Name` VARCHAR(150) NOT NULL,
            `Description` VARCHAR(500) NOT NULL,
            `Stock` int NOT NULL,
            `Price` int NOT NULL,
            `Created` DATETIME NOT NULL,
            `CreatedBy` VARCHAR(50) NOT NULL,
            `LastModified` DATETIME,
            `LastModifiedBy` VARCHAR(50),
            PRIMARY KEY (`Id`)
);

INSERT INTO `MS-Demo-Products-db`.`Tbl_Products` (`Id`, `Name`, `Description`, `Stock`, `Price`, `Created`, `CreatedBy`) VALUES ('6004ca3d-6f85-4694-b3e9-c9e1f82e923c', 'Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops', 'Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday', 15, 109, NOW(), 'admin@corre.com');
INSERT INTO `MS-Demo-Products-db`.`Tbl_Products` (`Id`, `Name`, `Description`, `Stock`, `Price`, `Created`, `CreatedBy`) VALUES ('94ed6f48-d1e6-49f1-a9f6-220dfc8b016a', 'Mens Casual Premium Slim Fit T-Shirts', 'Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing', 20, 40, NOW(), 'admin@corre.com');
INSERT INTO `MS-Demo-Products-db`.`Tbl_Products` (`Id`, `Name`, `Description`, `Stock`, `Price`, `Created`, `CreatedBy`) VALUES ('7f0ac083-115d-4384-87f4-c0d00d47bd72', 'Mens Cotton Jacket', 'great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors', 100, 55, NOW(), 'admin@corre.com');
