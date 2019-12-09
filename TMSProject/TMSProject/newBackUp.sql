-- MySqlBackup.NET 2.3.1
-- Dump Time: 2019-12-09 16:21:27
-- --------------------------------------
-- Server version 8.0.18 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of city
-- 

DROP TABLE IF EXISTS `city`;
CREATE TABLE IF NOT EXISTS `city` (
  `cityID` varchar(10) NOT NULL DEFAULT '',
  `cityName` varchar(30) NOT NULL DEFAULT '',
  PRIMARY KEY (`cityID`),
  KEY `idx_city_cityID` (`cityID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table city
-- 

/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city`(`cityID`,`cityName`) VALUES
('C001','Windsor'),
('C002','London'),
('C003','Hamilton'),
('C004','Toronto'),
('C005','Oshawa'),
('C006','Belleville'),
('C007','Kingston'),
('C008','Ottawa');
/*!40000 ALTER TABLE `city` ENABLE KEYS */;

-- 
-- Definition of carrier
-- 

DROP TABLE IF EXISTS `carrier`;
CREATE TABLE IF NOT EXISTS `carrier` (
  `carrierID` varchar(20) NOT NULL DEFAULT '',
  `depotCity` varchar(10) DEFAULT '',
  `carrierName` varchar(20) DEFAULT '',
  `ftlAvail` float(5,2) NOT NULL DEFAULT '0.00',
  `ltlAvail` float(5,2) NOT NULL DEFAULT '0.00',
  `ftlRate` float(5,2) NOT NULL DEFAULT '0.00',
  `ltlRate` float(5,2) NOT NULL DEFAULT '0.00',
  `reeferCharge` float(5,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`carrierID`),
  KEY `fk_carrier_depotCity` (`depotCity`),
  KEY `idx_carrier_carrierID` (`carrierID`),
  CONSTRAINT `fk_carrier_depotCity` FOREIGN KEY (`depotCity`) REFERENCES `city` (`cityID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table carrier
-- 

/*!40000 ALTER TABLE `carrier` DISABLE KEYS */;
INSERT INTO `carrier`(`carrierID`,`depotCity`,`carrierName`,`ftlAvail`,`ltlAvail`,`ftlRate`,`ltlRate`,`reeferCharge`) VALUES
('CA20190101001','C001','Planet Express',50,640,5.21,0.36,0.08),
('CA20190101002','C003','Planet Express',50,640,5.21,0.36,0.08),
('CA20190101003','C005','Planet Express',50,640,5.21,0.36,0.08),
('CA20190101004','C006','Planet Express',50,640,5.21,0.36,0.08),
('CA20190101005','C008','Planet Express',50,640,5.21,0.36,0.08),
('CA20190101006','C002','Schooner''s',18,98,5.05,0.34,0.07),
('CA20190101007','C004','Schooner''s',18,98,5.05,0.34,0.07),
('CA20190101008','C007','Schooner''s',18,98,5.05,0.34,0.07),
('CA20190101009','C001','Tillman Transport',24,35,5.11,0.3,0.09),
('CA20190101010','C002','Tillman Transport',18,45,5.11,0.3,0.09),
('CA20190101011','C003','Tillman Transport',18,45,5.11,0.3,0.09),
('CA20190101012','C008','We Haul',11,0,5.2,0,0.06),
('CA20190101013','C004','We Haul',11,0,5.2,0,0.06);
/*!40000 ALTER TABLE `carrier` ENABLE KEYS */;

-- 
-- Definition of contract_market_place
-- 

DROP TABLE IF EXISTS `contract_market_place`;
CREATE TABLE IF NOT EXISTS `contract_market_place` (
  `customerID` varchar(20) NOT NULL DEFAULT '',
  `contractID` varchar(20) NOT NULL DEFAULT '',
  `jobType` int(11) NOT NULL DEFAULT '0',
  `quantity` int(11) NOT NULL DEFAULT '0',
  `origin` varchar(10) NOT NULL,
  `destination` varchar(10) NOT NULL,
  `vanType` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`customerID`,`contractID`),
  KEY `idx_contract_market_place_customerID` (`customerID`),
  KEY `idx_contract_market_place_contractID` (`contractID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table contract_market_place
-- 

/*!40000 ALTER TABLE `contract_market_place` DISABLE KEYS */;
INSERT INTO `contract_market_place`(`customerID`,`contractID`,`jobType`,`quantity`,`origin`,`destination`,`vanType`) VALUES
('CUS201901010001','CONT201901010001',1,6,'Oshawa','Ottawa',0),
('CUS201901010002','CONT201901010002',0,0,'Ottawa','Belleville',1),
('CUS201901010003','CONT201901010003',0,0,'Kingston','London',1),
('CUS201901010004','CONT201901010002',0,0,'Windsor','Toronto',1),
('CUS201901010005','CONT201901010004',0,0,'Belleville','Oshawa',1),
('CUS201901010006','CONT201901010005',1,6,'Ottawa','London',1),
('CUS201901010007','CONT201901010006',0,0,'Oshawa','London',0),
('CUS201901010008','CONT201901010007',0,0,'Ottawa','Toronto',0),
('CUS201901010009','CONT201901010008',0,0,'Toronto','Windsor',1),
('CUS201901010010','CONT201901010009',1,32,'Ottawa','Hamilton',0),
('CUS201901010011','CONT201901010010',1,23,'London','Belleville',1);
/*!40000 ALTER TABLE `contract_market_place` ENABLE KEYS */;

-- 
-- Definition of contract
-- 

DROP TABLE IF EXISTS `contract`;
CREATE TABLE IF NOT EXISTS `contract` (
  `contractID` varchar(20) NOT NULL DEFAULT '',
  `InitiateBy` varchar(20) NOT NULL,
  `startDate` varchar(10) NOT NULL,
  `endDate` varchar(10) NOT NULL,
  `completeStatus` varchar(10) NOT NULL,
  PRIMARY KEY (`contractID`),
  KEY `idx_contract_contractID` (`contractID`),
  CONSTRAINT `fk_contract_contractID` FOREIGN KEY (`contractID`) REFERENCES `contract_market_place` (`contractID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table contract
-- 

/*!40000 ALTER TABLE `contract` DISABLE KEYS */;
INSERT INTO `contract`(`contractID`,`InitiateBy`,`startDate`,`endDate`,`completeStatus`) VALUES
('CONT201901010001','BUYER','2019-01-01','2019-12-31','Regular'),
('CONT201901010002','BUYER','2019-01-01','2019-12-31','Regular'),
('CONT201901010003','BUYER','2019-01-01','2019-12-31','Regular'),
('CONT201901010004','BUYER','2019-01-01','2019-12-31','Regular'),
('CONT201901010005','BUYER','2019-01-01','2019-12-31','Regular'),
('CONT201901010006','BUYER','2019-12-09','2019-12-18','UNPAID'),
('CONT201901010007','BUYER','2019-12-09','2019-12-11','UNPAID'),
('CONT201901010008','BUYER','2019-12-09','2019-12-18','UNPAID'),
('CONT201901010009','BUYER','2019-12-09','2019-12-16','UNPAID'),
('CONT201901010010','BUYER','2019-12-09','2019-12-20','UNPAID');
/*!40000 ALTER TABLE `contract` ENABLE KEYS */;

-- 
-- Definition of customer
-- 

DROP TABLE IF EXISTS `customer`;
CREATE TABLE IF NOT EXISTS `customer` (
  `customerID` varchar(50) NOT NULL DEFAULT '',
  `customerName` varchar(35) NOT NULL DEFAULT '',
  `customerCity` varchar(30) NOT NULL DEFAULT '',
  `telno` varchar(15) NOT NULL DEFAULT '',
  `address` varchar(30) NOT NULL DEFAULT '',
  `zipcode` varchar(10) NOT NULL DEFAULT '',
  `customerCompany` varchar(45) NOT NULL,
  `customerProvince` varchar(45) NOT NULL,
  PRIMARY KEY (`customerID`),
  KEY `idx_customer_customerID` (`customerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table customer
-- 

/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer`(`customerID`,`customerName`,`customerCity`,`telno`,`address`,`zipcode`,`customerCompany`,`customerProvince`) VALUES
('CUS201901010001','Abdullah','Waterloo','222-000-0000','415 Kidden AVE','N2V2T5','Hardware Depot','Ontario'),
('CUS201901010002','Trung','Kitchener','222-000-0000','415 Kidden AVE','N2V2T5','Malmart','Ontario'),
('CUS201901010003','Maconnic','Ottawa','222-000-0000','415 Kidden AVE','N2V 2T5','MacDongles','Ontario'),
('CUS201901010004','Tracy','London','222-000-0000','415 Kidden AVE','N2V 2T5','Malmart','Ontario'),
('CUS201901010005','Nick','Kingston','222-000-0000','415 Kidden AVE','N2V 2T5','Wallys World','Ontario'),
('CUS201901010006','Timocy','Windsor','222-000-0000','415 Kidden AVE','N2V 2T5','Sushi Noodle','Ontario'),
('CUS201901010007','Farm Supply Co','Hamilton','546688786','111 Westmount Place','N2P 0C9','DMS','Alberta(AB)'),
('CUS201901010008','Hardware Depot','Ottawa','1555565465','11 Egfdg fgfh','N2P 0C4','HTL','Saskatchewan(SK)'),
('CUS201901010010','Malmart','Oshawa','25365768','156 tEST STREET','N2P 0C8','DHFG','Saskatchewan(SK)'),
('CUS201901010011','Rockomax Engineering','London','3534665679','105 ABC STREET','J8P 8U9','GHGNJHGJ','Quebec(QC)'),
('CUS20191209001','Atlantis Railway','Waterloo','46544868664','11 East Street','N2L 0C7','DHY','Ontario(ON)');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;

-- 
-- Definition of employee
-- 

DROP TABLE IF EXISTS `employee`;
CREATE TABLE IF NOT EXISTS `employee` (
  `employeeID` varchar(15) NOT NULL DEFAULT '',
  `firstName` varchar(20) NOT NULL DEFAULT '',
  `lastName` varchar(20) NOT NULL DEFAULT '',
  `employeeType` enum('BUYER','PLANNER','ADMIN') NOT NULL DEFAULT 'BUYER',
  PRIMARY KEY (`employeeID`),
  KEY `fk_employee_customerID_idx` (`employeeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table employee
-- 

/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee`(`employeeID`,`firstName`,`lastName`,`employeeType`) VALUES
('AD001','MIke','Tyler','ADMIN'),
('BY001','Jose','Mouth','BUYER'),
('PL001','John','Luke','PLANNER');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;

-- 
-- Definition of admin
-- 

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `adminEmployeeID` varchar(15) NOT NULL DEFAULT '',
  `adminPassword` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`adminEmployeeID`),
  CONSTRAINT `fk_buyer_adminEmployeeID` FOREIGN KEY (`adminEmployeeID`) REFERENCES `employee` (`employeeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table admin
-- 

/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin`(`adminEmployeeID`,`adminPassword`) VALUES
('AD001','123');
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;

-- 
-- Definition of buyer
-- 

DROP TABLE IF EXISTS `buyer`;
CREATE TABLE IF NOT EXISTS `buyer` (
  `buyerEmployeeID` varchar(15) NOT NULL DEFAULT '',
  `buyerPassword` varchar(45) NOT NULL,
  PRIMARY KEY (`buyerEmployeeID`),
  CONSTRAINT `fk_buyer_buyerEmployeeID` FOREIGN KEY (`buyerEmployeeID`) REFERENCES `employee` (`employeeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table buyer
-- 

/*!40000 ALTER TABLE `buyer` DISABLE KEYS */;
INSERT INTO `buyer`(`buyerEmployeeID`,`buyerPassword`) VALUES
('BY001','123');
/*!40000 ALTER TABLE `buyer` ENABLE KEYS */;

-- 
-- Definition of invoice
-- 

DROP TABLE IF EXISTS `invoice`;
CREATE TABLE IF NOT EXISTS `invoice` (
  `invoiceID` varchar(20) NOT NULL DEFAULT '',
  `billingID` varchar(20) NOT NULL,
  `contractID` varchar(20) NOT NULL,
  `customerID` varchar(20) NOT NULL,
  `completeStatus` varchar(10) NOT NULL,
  PRIMARY KEY (`invoiceID`),
  KEY `fk_invoice_billingID` (`billingID`),
  KEY `fk_invoice_contractID` (`contractID`),
  KEY `fk_invoice_customerID` (`customerID`),
  KEY `idx_invoice_invoiceID` (`invoiceID`),
  CONSTRAINT `fk_invoice_billingID` FOREIGN KEY (`billingID`) REFERENCES `billing` (`billingID`),
  CONSTRAINT `fk_invoice_contractID` FOREIGN KEY (`contractID`) REFERENCES `contract` (`contractID`),
  CONSTRAINT `fk_invoice_customerID` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table invoice
-- 

/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;

/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;

-- 
-- Definition of mileage
-- 

DROP TABLE IF EXISTS `mileage`;
CREATE TABLE IF NOT EXISTS `mileage` (
  `mileageID` varchar(15) NOT NULL DEFAULT '',
  `startCityID` varchar(10) NOT NULL DEFAULT '',
  `endCityID` varchar(10) NOT NULL DEFAULT '',
  `distance` float NOT NULL DEFAULT '0',
  `workingTime` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`mileageID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table mileage
-- 

/*!40000 ALTER TABLE `mileage` DISABLE KEYS */;
INSERT INTO `mileage`(`mileageID`,`startCityID`,`endCityID`,`distance`,`workingTime`) VALUES
('MILE001','C001','C001',0,2),
('MILE002','C002','C001',191,2.5),
('MILE003','C002','C002',0,2),
('MILE004','C001','C002',128,1.75),
('MILE005','C003','C002',128,1.75),
('MILE006','C003','C003',0,2),
('MILE007','C002','C003',68,1.25),
('MILE008','C004','C003',68,1.25),
('MILE009','C004','C004',0,2),
('MILE010','C003','C004',60,1.3),
('MILE011','C005','C004',60,1.3),
('MILE012','C005','C005',0,2),
('MILE013','C004','C005',134,1.65),
('MILE014','C006','C005',134,1.65),
('MILE015','C006','C006',0,2),
('MILE016','C005','C006',82,1.2),
('MILE017','C007','C006',82,1.2),
('MILE018','C007','C007',0,2),
('MILE019','C006','C007',196,2.5),
('MILE020','C008','C007',196,2.5),
('MILE021','C008','C008',0,2);
/*!40000 ALTER TABLE `mileage` ENABLE KEYS */;

-- 
-- Definition of ordering
-- 

DROP TABLE IF EXISTS `ordering`;
CREATE TABLE IF NOT EXISTS `ordering` (
  `orderID` varchar(20) NOT NULL DEFAULT '',
  `contractID` varchar(20) NOT NULL DEFAULT '',
  `customerID` varchar(20) NOT NULL DEFAULT '',
  `orderDate` varchar(10) NOT NULL,
  `jobType` int(11) DEFAULT NULL,
  `quantity` int(11) NOT NULL DEFAULT '0',
  `vanType` int(11) DEFAULT NULL,
  `originalCityID` varchar(10) NOT NULL,
  `desCityID` varchar(10) NOT NULL,
  `carrierID` varchar(20) NOT NULL,
  `orderStatus` varchar(10) NOT NULL,
  PRIMARY KEY (`orderID`),
  KEY `fk_ordering_contractID` (`contractID`),
  KEY `fk_ordering_originalCityID` (`originalCityID`),
  KEY `fk_ordering_desCityID` (`desCityID`),
  KEY `fk_ordering_carrierID` (`carrierID`),
  KEY `idx_ordering_orderID` (`orderID`),
  KEY `idx_trip_orderID` (`orderID`),
  CONSTRAINT `fk_ordering_carrierID` FOREIGN KEY (`carrierID`) REFERENCES `carrier` (`carrierID`),
  CONSTRAINT `fk_ordering_contractID` FOREIGN KEY (`contractID`) REFERENCES `contract` (`contractID`),
  CONSTRAINT `fk_ordering_desCityID` FOREIGN KEY (`desCityID`) REFERENCES `city` (`cityID`),
  CONSTRAINT `fk_ordering_originalCityID` FOREIGN KEY (`originalCityID`) REFERENCES `city` (`cityID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table ordering
-- 

/*!40000 ALTER TABLE `ordering` DISABLE KEYS */;
INSERT INTO `ordering`(`orderID`,`contractID`,`customerID`,`orderDate`,`jobType`,`quantity`,`vanType`,`originalCityID`,`desCityID`,`carrierID`,`orderStatus`) VALUES
('ORD12092019001','CONT201901010007','CUS201901010008','2019-12-09',0,56,0,'C008','C004','CA20190101005','ACTIVE'),
('ORD12092019002','CONT201901010009','CUS201901010010','2020-02-03',0,10,0,'C008','C003','CA20190101005','ACITVE'),
('ORD12092019003','CONT201901010010','CUS201901010011','2019-12-29',0,15,1,'C002','C006','CA20190101006','ACTIVE');
/*!40000 ALTER TABLE `ordering` ENABLE KEYS */;

-- 
-- Definition of planinfo
-- 

DROP TABLE IF EXISTS `planinfo`;
CREATE TABLE IF NOT EXISTS `planinfo` (
  `planID` varchar(20) NOT NULL DEFAULT '',
  `orderID` varchar(20) NOT NULL DEFAULT '',
  `startCityID` varchar(20) NOT NULL DEFAULT '',
  `endCityID` varchar(10) NOT NULL DEFAULT '',
  `workingTime` float(5,2) NOT NULL DEFAULT '0.00',
  `distance` float(5,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`planID`),
  KEY `fk_planinfo_orderID` (`orderID`),
  KEY `fk_planinfo_startCityID` (`startCityID`),
  KEY `fk_planinfo_endCityID` (`endCityID`),
  KEY `idx_planinfo_planID` (`planID`),
  CONSTRAINT `fk_planinfo_endCityID` FOREIGN KEY (`endCityID`) REFERENCES `city` (`cityID`),
  CONSTRAINT `fk_planinfo_orderID` FOREIGN KEY (`orderID`) REFERENCES `ordering` (`orderID`),
  CONSTRAINT `fk_planinfo_startCityID` FOREIGN KEY (`startCityID`) REFERENCES `city` (`cityID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table planinfo
-- 

/*!40000 ALTER TABLE `planinfo` DISABLE KEYS */;

/*!40000 ALTER TABLE `planinfo` ENABLE KEYS */;

-- 
-- Definition of billing
-- 

DROP TABLE IF EXISTS `billing`;
CREATE TABLE IF NOT EXISTS `billing` (
  `billingID` varchar(20) NOT NULL DEFAULT '',
  `orderID` varchar(20) NOT NULL DEFAULT '',
  `planID` varchar(20) NOT NULL DEFAULT '',
  `customerID` varchar(20) NOT NULL DEFAULT '',
  `totalAmount` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`billingID`),
  KEY `fk_billing_orderID` (`orderID`),
  KEY `fk_billing_planID` (`planID`),
  KEY `fk_billing_customerID` (`customerID`),
  KEY `idx_billing_billingID` (`billingID`),
  CONSTRAINT `fk_billing_customerID` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`),
  CONSTRAINT `fk_billing_orderID` FOREIGN KEY (`orderID`) REFERENCES `ordering` (`orderID`),
  CONSTRAINT `fk_billing_planID` FOREIGN KEY (`planID`) REFERENCES `planinfo` (`planID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table billing
-- 

/*!40000 ALTER TABLE `billing` DISABLE KEYS */;

/*!40000 ALTER TABLE `billing` ENABLE KEYS */;

-- 
-- Definition of planner
-- 

DROP TABLE IF EXISTS `planner`;
CREATE TABLE IF NOT EXISTS `planner` (
  `plannerEmployeeID` varchar(15) NOT NULL DEFAULT '',
  `plannerPassword` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`plannerEmployeeID`),
  CONSTRAINT `fk_buyer_plannerEmployeeID` FOREIGN KEY (`plannerEmployeeID`) REFERENCES `employee` (`employeeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table planner
-- 

/*!40000 ALTER TABLE `planner` DISABLE KEYS */;
INSERT INTO `planner`(`plannerEmployeeID`,`plannerPassword`) VALUES
('PL001','123');
/*!40000 ALTER TABLE `planner` ENABLE KEYS */;

-- 
-- Definition of trip
-- 

DROP TABLE IF EXISTS `trip`;
CREATE TABLE IF NOT EXISTS `trip` (
  `tripID` varchar(20) NOT NULL DEFAULT '',
  `orderID` varchar(20) NOT NULL DEFAULT '',
  `startCity` varchar(10) NOT NULL DEFAULT '',
  `endCity` varchar(10) NOT NULL DEFAULT '',
  `tripStatus` varchar(10) NOT NULL DEFAULT '',
  PRIMARY KEY (`tripID`),
  KEY `fk_trip_orderID` (`orderID`),
  CONSTRAINT `fk_trip_orderID` FOREIGN KEY (`orderID`) REFERENCES `ordering` (`orderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table trip
-- 

/*!40000 ALTER TABLE `trip` DISABLE KEYS */;

/*!40000 ALTER TABLE `trip` ENABLE KEYS */;

-- 
-- Dumping procedures
-- 

DROP PROCEDURE IF EXISTS `GET_CUS_NAME`;
DELIMITER |
CREATE PROCEDURE `GET_CUS_NAME`()
BEGIN
Set @startCityName = (SELECT cityName From mileage inner join city on mileage.startCityID = city.cityID);
SELECT @startCityName as startCityName;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `LOAD_CARRIER`;
DELIMITER |
CREATE PROCEDURE `LOAD_CARRIER`()
BEGIN
select * from carrier;
END |
DELIMITER ;

DROP PROCEDURE IF EXISTS `LOAD_MILEAGE`;
DELIMITER |
CREATE PROCEDURE `LOAD_MILEAGE`()
BEGIN
select * from mileage;
END |
DELIMITER ;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2019-12-09 16:21:28
-- Total time: 0:0:0:0:280 (d:h:m:s:ms)
