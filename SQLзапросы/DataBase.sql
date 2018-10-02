use master
go
CREATE DATABASE VPDB
ON
(
NAME = 'VacationPlusDataBase',
FILENAME = 'D:\Databases\VacationPlusDB\VPDB.mdf',
SIZE = 8MB,
MAXSIZE = 150MB,
FILEGROWTH = 2MB
)
LOG ON
(
NAME = 'VacationPlusDB_log',
FILENAME = 'D:\Databases\VacationPlusDB\VPDB_log.ldf',
SIZE = 4MB,
MAXSIZE = 100MB,
FILEGROWTH = 1MB
)