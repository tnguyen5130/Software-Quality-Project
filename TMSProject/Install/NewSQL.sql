-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema projectslinger
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `projectslinger` ;

-- -----------------------------------------------------
-- Schema projectslinger
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `projectslinger` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `projectslinger` ;

-- -----------------------------------------------------
-- Table `customer`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `customer` ;

CREATE TABLE IF NOT EXISTS `customer` (
  `customerID` VARCHAR(50) NOT NULL DEFAULT '',
  `customerName` VARCHAR(35) NOT NULL DEFAULT '',
  `customerCity` VARCHAR(30) NOT NULL DEFAULT '',
  `telno` VARCHAR(15) NOT NULL DEFAULT '',
  `address` VARCHAR(30) NOT NULL DEFAULT '',
  `zipcode` VARCHAR(10) NOT NULL DEFAULT '',
  `customerCompany` VARCHAR(45) NOT NULL,
  `customerProvince` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`customerID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `idx_customer_customerID` ON `customer` (`customerID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `employee`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `employee` ;

CREATE TABLE IF NOT EXISTS `employee` (
  `employeeID` VARCHAR(15) NOT NULL DEFAULT '' PRIMARY KEY,
  `firstName` VARCHAR(20) NOT NULL DEFAULT '',
  `lastName` VARCHAR(20) NOT NULL DEFAULT '',
  `employeeType` ENUM('BUYER', 'PLANNER', 'ADMIN') NOT NULL DEFAULT 'BUYER')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_employee_customerID_idx` ON `employee` (`customerID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `admin`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `admin` ;

CREATE TABLE IF NOT EXISTS `admin` (
  `adminEmployeeID` VARCHAR(15) NOT NULL DEFAULT '',
  `adminPassword` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`adminEmployeeID`),
  CONSTRAINT `fk_buyer_adminEmployeeID`
    FOREIGN KEY (`adminEmployeeID`)
    REFERENCES `employee` (`employeeID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `city`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `city` ;

CREATE TABLE IF NOT EXISTS `city` (
  `cityID` VARCHAR(10) NOT NULL DEFAULT '',
  `cityName` VARCHAR(30) NOT NULL DEFAULT '',
  PRIMARY KEY (`cityID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `idx_city_cityID` ON `city` (`cityID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `carrier`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `carrier` ;

CREATE TABLE IF NOT EXISTS `carrier` (
  `carrierID` VARCHAR(20) NOT NULL DEFAULT '',
  `depotCity` VARCHAR(10) NULL DEFAULT '',
  `carrierName` VARCHAR(20) NULL DEFAULT '',
  `ftlAvail` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  `ltlAvail` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  `ftlRate` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  `ltlRate` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  `reeferCharge` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`carrierID`),
  CONSTRAINT `fk_carrier_depotCity`
    FOREIGN KEY (`depotCity`)
    REFERENCES `city` (`cityID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_carrier_depotCity` ON `carrier` (`depotCity` ASC) VISIBLE;

CREATE INDEX `idx_carrier_carrierID` ON `carrier` (`carrierID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `contract_market_place`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `contract_market_place` ;

CREATE TABLE IF NOT EXISTS `contract_market_place` (
  `customerID` VARCHAR(20) NOT NULL DEFAULT '',
  `contractID` VARCHAR(20) NOT NULL DEFAULT '',
  `jobType` ENUM('LTL', 'FTL') NOT NULL DEFAULT 'LTL',
  `quantity` INT(11) NOT NULL DEFAULT '0',
  `origin` VARCHAR(10) NOT NULL,
  `destination` VARCHAR(10) NOT NULL,
  `vanType` ENUM('F', 'R') NOT NULL DEFAULT 'F',
  PRIMARY KEY (`customerID`, `contractID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `idx_contract_market_place_customerID` ON `contract_market_place` (`customerID` ASC) VISIBLE;

CREATE INDEX `idx_contract_market_place_contractID` ON `contract_market_place` (`contractID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `contract`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `contract` ;

CREATE TABLE IF NOT EXISTS `contract` (
  `contractID` VARCHAR(20) NOT NULL DEFAULT '',
  `InitiateBy` VARCHAR(20) NOT NULL,
  `startDate` VARCHAR(10) NOT NULL,
  `endDate` VARCHAR(10) NOT NULL,
  `completeStatus` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`contractID`),
  CONSTRAINT `fk_contract_contractID`
    FOREIGN KEY (`contractID`)
    REFERENCES `contract_market_place` (`contractID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `idx_contract_contractID` ON `contract` (`contractID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `ordering`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ordering` ;

CREATE TABLE IF NOT EXISTS `ordering` (
  `orderID` VARCHAR(20) NOT NULL DEFAULT '',
  `contractID` VARCHAR(20) NOT NULL DEFAULT '',
  `customerID` VARCHAR(20) NOT NULL DEFAULT '',
  `orderDate` VARCHAR(10) NOT NULL,
  `jobType` INT(11) NULL DEFAULT NULL,
  `quantity` INT(11) NOT NULL DEFAULT '0',
  `vanType` INT(11) NULL DEFAULT NULL,
  `originalCityID` VARCHAR(10) NOT NULL,
  `desCityID` VARCHAR(10) NOT NULL,
  `carrierID` VARCHAR(20) NOT NULL,
  `orderStatus` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`orderID`),
  CONSTRAINT `fk_ordering_carrierID`
    FOREIGN KEY (`carrierID`)
    REFERENCES `carrier` (`carrierID`),
  CONSTRAINT `fk_ordering_contractID`
    FOREIGN KEY (`contractID`)
    REFERENCES `contract` (`contractID`),
  CONSTRAINT `fk_ordering_desCityID`
    FOREIGN KEY (`desCityID`)
    REFERENCES `city` (`cityID`),
  CONSTRAINT `fk_ordering_originalCityID`
    FOREIGN KEY (`originalCityID`)
    REFERENCES `city` (`cityID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_ordering_contractID` ON `ordering` (`contractID` ASC) VISIBLE;

CREATE INDEX `fk_ordering_originalCityID` ON `ordering` (`originalCityID` ASC) VISIBLE;

CREATE INDEX `fk_ordering_desCityID` ON `ordering` (`desCityID` ASC) VISIBLE;

CREATE INDEX `fk_ordering_carrierID` ON `ordering` (`carrierID` ASC) VISIBLE;

CREATE INDEX `idx_ordering_orderID` ON `ordering` (`orderID` ASC) VISIBLE;

CREATE INDEX `idx_trip_orderID` ON `ordering` (`orderID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `planinfo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `planinfo` ;

CREATE TABLE IF NOT EXISTS `planinfo` (
  `planID` VARCHAR(20) NOT NULL DEFAULT '',
  `orderID` VARCHAR(20) NOT NULL DEFAULT '',
  `startCityID` VARCHAR(20) NOT NULL DEFAULT '',
  `endCityID` VARCHAR(10) NOT NULL DEFAULT '',
  `workingTime` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  `distance` FLOAT(5,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`planID`),
  CONSTRAINT `fk_planinfo_endCityID`
    FOREIGN KEY (`endCityID`)
    REFERENCES `city` (`cityID`),
  CONSTRAINT `fk_planinfo_orderID`
    FOREIGN KEY (`orderID`)
    REFERENCES `ordering` (`orderID`),
  CONSTRAINT `fk_planinfo_startCityID`
    FOREIGN KEY (`startCityID`)
    REFERENCES `city` (`cityID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_planinfo_orderID` ON `planinfo` (`orderID` ASC) VISIBLE;

CREATE INDEX `fk_planinfo_startCityID` ON `planinfo` (`startCityID` ASC) VISIBLE;

CREATE INDEX `fk_planinfo_endCityID` ON `planinfo` (`endCityID` ASC) VISIBLE;

CREATE INDEX `idx_planinfo_planID` ON `planinfo` (`planID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `billing`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `billing` ;

CREATE TABLE IF NOT EXISTS `billing` (
  `billingID` VARCHAR(20) NOT NULL DEFAULT '',
  `orderID` VARCHAR(20) NOT NULL DEFAULT '',
  `planID` VARCHAR(20) NOT NULL DEFAULT '',
  `customerID` VARCHAR(20) NOT NULL DEFAULT '',
  `totalAmount` DOUBLE NOT NULL DEFAULT '0',
  PRIMARY KEY (`billingID`),
  CONSTRAINT `fk_billing_customerID`
    FOREIGN KEY (`customerID`)
    REFERENCES `customer` (`customerID`),
  CONSTRAINT `fk_billing_orderID`
    FOREIGN KEY (`orderID`)
    REFERENCES `ordering` (`orderID`),
  CONSTRAINT `fk_billing_planID`
    FOREIGN KEY (`planID`)
    REFERENCES `planinfo` (`planID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_billing_orderID` ON `billing` (`orderID` ASC) VISIBLE;

CREATE INDEX `fk_billing_planID` ON `billing` (`planID` ASC) VISIBLE;

CREATE INDEX `fk_billing_customerID` ON `billing` (`customerID` ASC) VISIBLE;

CREATE INDEX `idx_billing_billingID` ON `billing` (`billingID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `buyer`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `buyer` ;

CREATE TABLE IF NOT EXISTS `buyer` (
  `buyerEmployeeID` VARCHAR(15) NOT NULL DEFAULT '',
  `buyerPassword` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`buyerEmployeeID`),
  CONSTRAINT `fk_buyer_buyerEmployeeID`
    FOREIGN KEY (`buyerEmployeeID`)
    REFERENCES `employee` (`employeeID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `invoice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `invoice` ;

CREATE TABLE IF NOT EXISTS `invoice` (
  `invoiceID` VARCHAR(20) NOT NULL DEFAULT '',
  `billingID` VARCHAR(20) NOT NULL,
  `contractID` VARCHAR(20) NOT NULL,
  `customerID` VARCHAR(20) NOT NULL,
  `completeStatus` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`invoiceID`),
  CONSTRAINT `fk_invoice_billingID`
    FOREIGN KEY (`billingID`)
    REFERENCES `billing` (`billingID`),
  CONSTRAINT `fk_invoice_contractID`
    FOREIGN KEY (`contractID`)
    REFERENCES `contract` (`contractID`),
  CONSTRAINT `fk_invoice_customerID`
    FOREIGN KEY (`customerID`)
    REFERENCES `customer` (`customerID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_invoice_billingID` ON `invoice` (`billingID` ASC) VISIBLE;

CREATE INDEX `fk_invoice_contractID` ON `invoice` (`contractID` ASC) VISIBLE;

CREATE INDEX `fk_invoice_customerID` ON `invoice` (`customerID` ASC) VISIBLE;

CREATE INDEX `idx_invoice_invoiceID` ON `invoice` (`invoiceID` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `mileage`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mileage` ;

CREATE TABLE IF NOT EXISTS `mileage` (
  `mileageID` VARCHAR(15) NOT NULL DEFAULT '',
  `startCityID` VARCHAR(10) NOT NULL DEFAULT '',
  `endCityID` VARCHAR(10) NOT NULL DEFAULT '',
  `distance` FLOAT NOT NULL DEFAULT '0',
  `workingTime` FLOAT NOT NULL DEFAULT '0',
  PRIMARY KEY (`mileageID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `planner`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `planner` ;

CREATE TABLE IF NOT EXISTS `planner` (
  `plannerEmployeeID` VARCHAR(15) NOT NULL DEFAULT '',
  `plannerPassword` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`plannerEmployeeID`),
  CONSTRAINT `fk_buyer_plannerEmployeeID`
    FOREIGN KEY (`plannerEmployeeID`)
    REFERENCES `employee` (`employeeID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trip`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `trip` ;

CREATE TABLE IF NOT EXISTS `trip` (
  `tripID` VARCHAR(20) NOT NULL DEFAULT '',
  `orderID` VARCHAR(20) NOT NULL DEFAULT '',
  `startCity` VARCHAR(10) NOT NULL DEFAULT '',
  `endCity` VARCHAR(10) NOT NULL DEFAULT '',
  `tripStatus` VARCHAR(10) NOT NULL DEFAULT '',
  PRIMARY KEY (`tripID`),
  CONSTRAINT `fk_trip_orderID`
    FOREIGN KEY (`orderID`)
    REFERENCES `ordering` (`orderID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_trip_orderID` ON `trip` (`orderID` ASC) VISIBLE;

USE `projectslinger` ;

/* INSERT SOME DATA */
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY001', 'Windsor');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY002', 'London');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY003', 'Hamilton');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY004', 'Toronto');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY005', 'Oshawa');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY006', 'Belleville');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY007', 'Kingston');
INSERT INTO `projectslinger`.`city` (`cityID`, `cityName`) VALUES ('CITY008', 'Ottawa');

INSERT INTO `projectslinger`.`employee` (`employeeID`, `firstName`, `lastName`, `employeeType`) VALUES ('BY001', 'Jose', 'Mouth', 'BUYER');
INSERT INTO `projectslinger`.`employee` (`employeeID`, `firstName`, `lastName`, `employeeType`) VALUES ('PL001', 'John', 'Luke', 'PLANNER');
INSERT INTO `projectslinger`.`employee` (`employeeID`, `firstName`, `lastName`, `employeeType`) VALUES ('AD001', 'MIke', 'Tyler', 'ADMIN');

INSERT INTO `projectslinger`.`planner` (`plannerEmployeeID`, `plannerPassword`) VALUES ('PL001', '123');
INSERT INTO `projectslinger`.`buyer` (`buyerEmployeeID`, `buyerPassword`) VALUES ('BY001', '123');
INSERT INTO `projectslinger`.`admin` (`adminEmployeeID`, `adminPassword`) VALUES ('AD001', '123');

INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR001', 'CITY001', 'Planet Express', '50', '640', '5.21', '0.3621', '0.08');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR002', 'CITY003', 'Planet Express', '50', '640', '5.21', '0.3621', '0.08');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR003', 'CITY005', 'Planet Express', '50', '640', '5.21', '0.3621', '0.08');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR004', 'CITY006', 'Planet Express', '50', '640', '5.21', '0.3621', '0.08');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR005', 'CITY008', 'Planet Express', '50', '640', '5.21', '0.3621', '0.08');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR006', 'CITY002', 'Schooner\'s', '18', '98', '5.05', '0.3434', '0.07');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR007', 'CITY004', 'Schooner\'s', '18', '98', '5.05', '0.3434', '0.07');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR008', 'CITY007', 'Schooner\'s', '18', '98', '5.05', '0.3434', '0.07');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR009', 'CITY001', 'Tillman Transport', '24', '35', '5.11', '0.3012', '0.09');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR010', 'CITY002', 'Tillman Transport', '18', '45', '5.11', '0.3012', '0.09');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR011', 'CITY003', 'Tillman Transport', '18', '45', '5.11', '0.3012', '0.09');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR012', 'CITY008', 'We Haul', '11', '0', '5.2', '0', '0.065');
INSERT INTO `projectslinger`.`carrier` (`carrierID`, `depotCity`, `carrierName`, `ftlAvail`, `ltlAvail`, `ftlRate`, `ltlRate`, `reeferCharge`) VALUES ('CAR013', 'CITY004', 'We Haul', '11', '0', '5.2', '0', '0.065');


-- -----------------------------------------------------
-- procedure GET_CUS_NAME
-- -----------------------------------------------------

USE `projectslinger`;
DROP procedure IF EXISTS `GET_CUS_NAME`;

DELIMITER $$
USE `projectslinger`$$
CREATE DEFINER=`trung`@`%` PROCEDURE `GET_CUS_NAME`()
BEGIN
Set @startCityName = (SELECT cityName From mileage inner join city on mileage.startCityID = city.cityID);
SELECT @startCityName as startCityName;

END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
