-- Creates tables (compatible with your provided schema) but turns IDs into IDENTITY for easier inserts.

IF DB_ID('PatientVisitDb') IS NULL
BEGIN
  CREATE DATABASE PatientVisitDb;
END
GO
USE PatientVisitDb;
GO

IF OBJECT_ID('visitTypes') IS NULL
CREATE TABLE visitTypes(
  typeID INT IDENTITY(1,1) PRIMARY KEY,
  typeName VARCHAR(200) UNIQUE
);
GO

IF OBJECT_ID('loggers') IS NULL
CREATE TABLE loggers(
  loggerID INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100)
);
GO

IF OBJECT_ID('loggerActivities') IS NULL
CREATE TABLE loggerActivities(
  ActivityID INT IDENTITY(1,1) PRIMARY KEY,
  activityDescription VARCHAR(2000),
  loggerID INT NULL,
  ActivityDate DATE NULL,
  ActivityTime TIME NULL,
  FOREIGN KEY(loggerID) REFERENCES loggers(loggerID)
);
GO

IF OBJECT_ID('feeSchedule') IS NULL
CREATE TABLE feeSchedule(
  feeID INT IDENTITY(1,1) PRIMARY KEY,
  VisitType VARCHAR(100),
  feePerMinute DECIMAL(10,2)
);
GO

IF OBJECT_ID('Visits') IS NULL
CREATE TABLE Visits(
  visitID INT IDENTITY(1,1) PRIMARY KEY,
  visitType VARCHAR(200),
  VisittypeID INT NULL,
  visitDuration INT,
  visitDate DATE,
  visitTime TIME,
  visitFee DECIMAL(10,2),
  FOREIGN KEY(VisittypeID) REFERENCES visitTypes(typeID)
);
GO

IF OBJECT_ID('Patients') IS NULL
CREATE TABLE Patients(
  patientID INT IDENTITY(1,1) PRIMARY KEY,
  visitID INT NULL,
  patientName VARCHAR(200),
  patientEmail VARCHAR(200),
  patientPhone VARCHAR(200),
  patientDescription VARCHAR(1000),
  FOREIGN KEY(visitID) REFERENCES Visits(visitID)
);
GO

IF OBJECT_ID('Doctors') IS NULL
CREATE TABLE Doctors(
  doctorID INT IDENTITY(1,1) PRIMARY KEY,
  visitID INT NULL,
  doctorName VARCHAR(200),
  doctorEmail VARCHAR(200),
  doctorPhone VARCHAR(200),
  specialization VARCHAR(1000),
  FOREIGN KEY(visitID) REFERENCES Visits(visitID)
);
GO

IF OBJECT_ID('Users') IS NULL
CREATE TABLE Users (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Email VARCHAR(255) NOT NULL UNIQUE,
  PasswordHash VARCHAR(255) NOT NULL,
  PasswordSalt VARCHAR(255) NOT NULL,
  Role VARCHAR(50) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT(GETDATE()),
  IsActive BIT NOT NULL DEFAULT(1)
);
GO



-- Pre-seed an Admin user
DECLARE @Salt VARBINARY(16) = 0x5A1F3C9B7E4D8A23;  -- fixed demo salt (hex)
DECLARE @Password NVARCHAR(100) = 'Admin@123';

-- Compute SHA2_256 hash
DECLARE @Hash VARBINARY(64) = HASHBYTES('SHA2_256', CONVERT(VARBINARY(100), @Password) + @Salt);

INSERT INTO Users (Email, PasswordHash, PasswordSalt, Role, CreatedAt, IsActive)
VALUES (
    'admin@system.com',
    CONVERT(VARCHAR(128), @Hash, 2),   -- store hash as hex string
    CONVERT(VARCHAR(128), @Salt, 2),   -- store salt as hex string
    'Admin',
    GETDATE(),
    1
);

delete from Users
select * from Users
use MuhammadSharjeelFarzadDB
use PatientVisitDb

SELECT TOP 1 * FROM dbo.Users;
