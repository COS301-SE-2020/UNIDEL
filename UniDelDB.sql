create database unidel;
use unidel;

select * from Vehicle;
select * from User;

create table User (
	UserID int primary key auto_increment not null,
    UserEmail varchar(255) not null,
    UserPassword varchar(255) not null,
    UserProfilePic blob,
    UserType varchar(255)
);

create table Client (
	ClientID int primary key auto_increment not null,
    ClientName varchar(255) not null,
    ClientTelephone varchar(20) not null,
    ClientAddress varchar(255),
    UserID int not null,
    constraint fk_user foreign key(UserID) references User(UserID) 
    on delete cascade on update cascade
);

create table Driver (
	DriverID int primary key auto_increment not null,
    DriverName varchar(255) not null,
    DriverSurname varchar(255),
    DriverRating float,
    UserID int not null,
    DriverCellphone varchar(20) not null,
	constraint fk_driveruser foreign key(UserID) references User(UserID) 
    on delete cascade on update cascade
);

create table FleetManager (
	FleetManagerID int primary key auto_increment not null,
    FleetManagerName varchar(255) not null,
    FleetManagerCellphone varchar(20),
    UserID int not null,
	constraint fk_fleetmanageruser foreign key(UserID) references User(UserID) 
    on delete cascade on update cascade
);

create table Vehicle (
	VehicleID int primary key auto_increment not null,
    VehicleMake varchar(255) not null,
    VehicleModel varchar(255) not null,
    VehicleVIN varchar(255) not null,
    VehicleMileage int not null,
    VehicleLicensePlate varchar(255) not null,
    VehicleLicenseDiskExpiry date not null,
    VehicleLastService date,
    VehicleNextMileageService int,
    VehicleNextDateService date
);
insert into Vehicle values (102,'Mercedes-Benz','V220','IBdwdFGEB',20000,'EBD234GP','2021-02-20','2020-04-30',35000,'2020-10-20');
select * from vehicle;

create table CourierCompany (
	CourierCompanyID int primary key auto_increment not null,
    CourierCompanyName varchar(255) not null,
    CourierCompanyTelephone varchar(20) not null,
    UserID int not null,
	constraint fk_companyuser foreign key(UserID) references User(UserID) 
    on delete cascade on update cascade
);

create table Delivery (
	DeliveryID int primary key auto_increment not null,
    DeliveryDate date not null,
    DeliveryPickupLocation varchar(255) not null,
    DeliveryState varchar(255) not null,
    DeliveryPicture blob,
    DriverID int not null,
    VehicleID int not null,
    ClientID int not null,
    CourierCompanyID int not null,
	constraint fk_deliverydriver foreign key(DriverID) references Driver(DriverID) on update cascade,
    constraint fk_deliveryvehicle foreign key(VehicleID) references Vehicle(VehicleID) on update cascade,
    constraint fk_deliveryclient foreign key(ClientID) references Client(ClientID) on update cascade,
    constraint fk_deliverycouriercompany foreign key(CourierCompanyID) references CourierCompany(CourierCompanyID) on update cascade
);

create table CompanyVehicle (
	CompanyVehicleID int primary key auto_increment not null,
	CourierCompanyID int not null,
    VehicleID int not null,
	constraint fk_companyvehicle foreign key(CourierCompanyID) references CourierCompany(CourierCompanyID)
    on delete cascade on update cascade,
    constraint fk_vehiclecompany foreign key(VehicleID) references Vehicle(VehicleID) 
    on delete cascade on update cascade
);

create table CompanyDelivery (
	CompanyDeliveryID int primary key auto_increment not null,
	CourierCompanyID int not null,
    DeliveryID int not null,
	constraint fk_companydelivery foreign key(CourierCompanyID) references CourierCompany(CourierCompanyID)
    on delete cascade on update cascade,
	constraint fk_deliverycompanyassoc foreign key(DeliveryID) references Delivery(DeliveryID) 
    on delete cascade on update cascade
);






















