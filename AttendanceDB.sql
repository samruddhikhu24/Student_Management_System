CREATE DATABASE AttendanceDB;
GO

USE AttendanceDB;
GO

CREATE TABLE Course1
(
    CID INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100)
);