 use MuhammadSharjeelFarzadDB;

go
-- =======Look Up Tables========== --

create table visitTypes(
typeID int primary key,
typeName varchar(200) unique
 
)
go
create table loggers(
loggerID int primary key,
name varchar(100)
)
go
create table loggerActivities(
ActivityID int primary key Identity(1,1),
activityDescription varchar(2000),
loggerID int, 
foreign key(loggerID) references loggers(loggerID)

)
go
alter table loggerActivities
add ActivityDate date, ActivityTime time;
go
create table feeSchedule(
feeID int primary key,
VisitType varchar(100),
feePerMinute Decimal(10,2)
)



-- =============================== --

go

create table Visits(
visitID int primary key,
visitType varchar(200),
VisittypeID int,
visitDuration int,
visitDate date,
visitTime time,
visitFee decimal(10,2),
foreign key(VisittypeID) references visitTypes(typeID),
)

 go
create table patients(
patientID int primary key,
visitID int unique,
patientName varchar(200),
patientEmail varchar(200),
patientPhone varchar(200),
patientDescription varchar(1000),
Foreign key(visitID) references visits(visitID)
)
go

create table doctors(
doctorID int primary key,
visitID int,
doctorName varchar(200),
doctorEmail varchar(200),
doctorPhone varchar(200),
specialization varchar(1000),
Foreign key(visitID) references visits(visitID)

)

go





ALTER TABLE patients
DROP CONSTRAINT UQ__patients__0D1361E94BAD5104;


CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    PasswordSalt VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL
);



CREATE TABLE VisitAssignments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VisitID INT FOREIGN KEY REFERENCES Visits(visitID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(doctorID)
);




CREATE TABLE PatientVisits (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VisitID INT FOREIGN KEY REFERENCES Visits(visitID),
    PatientID INT FOREIGN KEY REFERENCES Patients(patientID)
);


INSERT INTO Users (Email, PasswordHash, PasswordSalt, Role)
VALUES (
    'admin@system.com',
    'yxRndjiv6jftaFeSp60nberSbqLqBxsWc43jQivDGMo8pmIa6RIG+cTDLzL5VGKEVrt8AnafQ2DqQQ92owKWpA==',
    'PvNxrllp3RzZBaBHyPeyibuCK/SjFnVQsAb61TEXMhMQxNg9lxyah0olVcbPn+Cm0MlfncvmJShudbvggB90Gh4YCGiOiEEqu4tNRScexYSBPGI5hvn3UV6igM0wkHMjJtfMqrkRkE7P96KlS/2B2QClhzi4NAD1Xtu8U6q9u8U=',
    'Admin'
);

ALTER TABLE Users
ADD CreatedAt DATETIME NOT NULL DEFAULT(GETDATE()),
    IsActive BIT NOT NULL DEFAULT(1);

select * from Users

delete from users