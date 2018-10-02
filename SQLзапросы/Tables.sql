use VPDB
go
CREATE TABLE AdminLogin
(
id int identity(1, 1) PRIMARY KEY,
login varchar(100) NOT NULL,
password varchar(100) NOT NULL
)
CREATE TABLE Employees
(
id int identity(1, 1) PRIMARY KEY,
fullName varchar(150) NOT NULL,
departmentID int NOT NULL,
address varchar(200) NOT NULL,
countryID int NOT NULL,
cityID int NOT NULL,
email varchar(100) NOT NULL,
phoneNumber varchar(20) NOT NULL,
login varchar(100) NOT NULL,
password varchar(100) NOT NULL,
vacationStatus int NOT NULL,
workbegin date NOT NULL
)
CREATE TABLE FiredEmployees
(
id int identity(1, 1) PRIMARY KEY,
idEmp int NOT NULL,
login varchar(100) NOT NULL,
password varchar(100) NOT NULL,
AdminMessage varchar(1000) NOT NULL
)
CREATE TABLE Vacations
(
id int identity(1, 1) PRIMARY KEY,
idEmp int NOT NULL,
dateBegin date,
dateEnd date,
daysCount int NOT NULL,
status int NOT NULL
)
CREATE TABLE Messages
(
id int identity(1, 1) PRIMARY KEY,
fromID int NOT NULL,
toID int NOT NULL,
title varchar(100) NOT NULL,
message varchar(1000) NOT NULL,
MessageTime datetime NOT NULL
)
CREATE TABLE Countries
(
id int identity(1, 1) PRIMARY KEY,
name varchar(80) NOT NULL,
description varchar(200)
)
CREATE TABLE Cities
(
id int identity(1, 1) PRIMARY KEY,
name varchar(80) NOT NULL,
description varchar(200),
countryID int NOT NULL
)
CREATE TABLE Departments
(
id int identity(1, 1) PRIMARY KEY,
name varchar(80) NOT NULL,
description varchar(200)
)
CREATE TABLE Requests
(
id int identity(1, 1) PRIMARY KEY,
fromID int NOT NULL,
message varchar(500) NOT NULL,
status int NOT NULL,
type int NOT NULL
)