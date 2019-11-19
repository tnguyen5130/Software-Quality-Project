-- customer
create DATABASE projectslinger;
USE projectslinger ;

-- customer
create table customer
-- create table if not exists table customer
(
    customerID      varchar(20) default '' not null primary key,
    customerName    varchar(35) default '' not null,
    customerCity    varchar(30) default '' not null,
    telno           varchar(15)  default '' not null,
    address         varchar(30) default '' not null,
    zipcode         varchar(10) default ''  not null
)
    charset=utf8;

create index idx_customer_customerID
    on customer (customerID);

-- contract_market_place
create table contract_market_place
(
    customerID          varchar(20) default '' not null,
    contractID          varchar(20) default '' not null,
    jobType             enum('LTL','FTL') default 'LTL' not null,
    quantity            int not null,
    origin              varchar(10) not null,
    destination         varchar(10) not null,
    vanType             enum('F', 'R') default 'F' not null,
	primary key(customerID, contractID),
    constraint fk_contract_market_place_customerID
        foreign key (customerID) references customer (customerID)
)
    charset=utf8;

create index idx_contract_market_place_customerID
    on contract_market_place (customerID);

create index idx_contract_market_place_contractID
    on contract_market_place (contractID);

-- city
create table city
(
    cityID                  varchar(10) default '' not null primary key,
    cityName                varchar(30) default '' not null
)
    charset=utf8;

create index idx_city_cityID
    on city (cityID);

-- carrier
create table carrier
(
    carrierID               varchar(20) default '' primary key,
    depotCity               varchar(10) default '',
    carrierName             varchar(20) default '',
    ftlAvail                float(5,2)  default 0.00 not null,
    ltlAvail                float(5,2)  default 0.00 not null,
    ftlRate                 float(5,2)  default 0.00 not null,
    ltlRate                 float(5,2)  default 0.00 not null,
    reeferCharge            float(5,2)  default 0.00 not null,
    constraint fk_carrier_depotCity
        foreign key (depotCity) references city (cityID)
)
    charset=utf8;

create index idx_carrier_carrierID
    on carrier (carrierID);

-- contract
create table contract
(
    contractID          varchar(20) default '' not null primary key,
    InitiateBy          varchar(20) not null,
    startDate           varchar(10) not null,
    endDate             varchar(10) not null,
    completeStatus      varchar(10) not null,
    constraint fk_contract_contractID
        foreign key (contractID) references contract_market_place (contractID)
)
    charset=utf8;
create index idx_contract_contractID
    on contract (contractID);

-- order
create table ordering
(
    orderID             varchar(20) default '' not null primary key,
    contractID          varchar(20) default '' not null,
    orderDate           varchar(10) not null,
    originalCityID      varchar(10) not null,
    desCityID           varchar(10) not null,
    carrierID           varchar(20)  not null,
    orderStatus         varchar(10)  not null,
    constraint fk_ordering_contractID
        foreign key (contractID) references contract (contractID),
    constraint fk_ordering_originalCityID
        foreign key (originalCityID) references city (cityID),
    constraint fk_ordering_desCityID
        foreign key (desCityID) references city (cityID),
    constraint fk_ordering_carrierID
        foreign key (carrierID) references carrier (carrierID)
)
    charset=utf8;

create index idx_ordering_orderID
    on ordering (orderID);

-- trip
create table trip
(
    tripID                  varchar(20) default '' not null primary key,
    orderID                 varchar(20) default '' not null,
    tripStatus              varchar(10) default '' not null,
    constraint fk_trip_orderID
        foreign key (orderID) references ordering (orderID)
)
    charset=utf8;

create index idx_trip_orderID
    on ordering (orderID);

-- planinfo
create table planinfo
(
    planID                  varchar(20) default '' not null primary key,
    tripID                  varchar(20) default '' not null,
    startCityID             varchar(20) default '' not null,
    endCityID               varchar(10) default '' not null,
    workingTime             float(5,2)  default 0.00 not null,
    distance                float(5,2)  default 0.00 not null,
    constraint fk_planinfo_tripID
        foreign key (tripID) references trip (tripID),
    constraint fk_planinfo_startCityID
        foreign key (startCityID) references city (cityID),
    constraint fk_planinfo_endCityID
        foreign key (endCityID) references city (cityID)

)
    charset=utf8;
create index idx_planinfo_tripID
    on planinfo (tripID);

-- billing
create table billing
(
    billingID               varchar(20) default '' not null primary key,
    orderID                 varchar(20) default '' not null,
    planID                  varchar(20) default '' not null,
    customerID              varchar(20) default '' not null,
    totalAmount             float(5,2)  default 0.00 not null,
    constraint fk_billing_orderID
        foreign key (orderID) references ordering (orderID),
    constraint fk_billing_planID
        foreign key (planID) references planinfo (planID),
    constraint fk_billing_customerID
        foreign key (customerID) references customer (customerID)

)
    charset=utf8;

create index idx_billing_billingID
    on billing (billingID);

-- invoice
create table invoice
(
    invoiceID               varchar(20) default '' not null primary key,
    billingID               varchar(20)  not null,
    contractID              varchar(20)  not null,
    customerID              varchar(20)  not null,
    completeStatus          varchar(10)  not null,
    constraint fk_invoice_billingID
        foreign key (billingID) references billing (billingID),
    constraint fk_invoice_contractID
        foreign key (contractID) references contract (contractID),
    constraint fk_invoice_customerID
        foreign key (customerID) references customer (customerID)

)
    charset=utf8;

create index idx_invoice_invoiceID
    on invoice (invoiceID);

-- employee
create table employee
-- create table if not exists table customer
(
    employeeID      varchar(15) default '' not null primary key,
    firstName       varchar(20) default '' not null,
    lastName        varchar(20) default '' not null,
    employeeType    enum('BUYER', 'PLANNER', 'ADMIN')  default 'BUYER' not null
)
    charset=utf8;

-- buyer
create table buyer
(
    buyerEmployeeID     varchar(15) default '' not null primary key,
	constraint fk_buyer_buyerEmployeeID
        foreign key (buyerEmployeeID) references employee (employeeID)
)
    charset=utf8;

-- planner
create table planner
(
    plannerEmployeeID     varchar(15) default '' not null primary key,
	constraint fk_buyer_plannerEmployeeID
        foreign key (plannerEmployeeID) references employee (employeeID)
)
    charset=utf8;

-- admin
create table admin
(
    adminEmployeeID     varchar(15) default '' not null primary key,
	constraint fk_buyer_adminEmployeeID
        foreign key (adminEmployeeID) references employee (employeeID)
)
    charset=utf8;
    
ALTER TABLE `projectslinger`.`buyer` 
ADD COLUMN `buyerPassword` VARCHAR(45) NOT NULL AFTER `buyerEmployeeID`;
ALTER TABLE `projectslinger`.`admin` 
ADD COLUMN `adminPassword` VARCHAR(45) NULL AFTER `adminEmployeeID`;
ALTER TABLE `projectslinger`.`planner` 
CHANGE COLUMN `plannerEmployeePassword` `plannerPassword` VARCHAR(45) NULL DEFAULT NULL ;

