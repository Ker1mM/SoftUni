CREATE DATABASE Minions

CREATE TABLE Minions (
Id INT PRIMARY KEY,
[Name] NVARCHAR(50),
Age INT
)

CREATE TABLE Towns (
Id INT PRIMARY KEY,
[Name] NVARCHAR(50)
)

ALTER TABLE Minions 
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

INSERT INTO Towns(Id, [Name]) VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

INSERT INTO Minions(Id, [Name], Age, TownId) VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

DELETE FROM Minions

DROP TABLE Minions
DROP TABLE Towns
GO

/*Problem 7.*/
CREATE TABLE People (
Id INT IDENTITY PRIMARY KEY,
[Name] NVARCHAR(200) NOT NULL,
Picture IMAGE,
Height DECIMAL(15,2),
[Weight] DECIMAL(15,2),
Gender CHAR(1) CHECK (Gender = 'm' OR Gender = 'f') NOT NULL,
Birthdate DATETIME NOT NULL,
Biography NVARCHAR(MAX)
)

INSERT INTO People([Name], Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
('Ivancho', NULL, 1.75, 83.00, 'm', '1990-06-12', 'Not Null'),
('Pesho', NULL, 1.98, 91.20, 'm', '1979-01-01', 'This is Null'),
('Gosho', NULL, 1.69, 59.30, 'm', '1999-09-28', NULL),
('Ayse', NULL, 1.55, 45.50, 'f', '2001-01-12', 'She is cool.'),
('Ivanka', NULL, 1.78, 72.90, 'f', '1996-10-27', 'Wanted by Interpol!')

SELECT * FROM People
GO

/*Problem 8.*/
CREATE TABLE Users (
Id INT PRIMARY KEY IDENTITY,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(MAX) CHECK (DATALENGTH(ProfilePicture) <= 900 * 1024),
LastLoginTime DATETIME,
IsDeleted BIT
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
('Petko97', '1234567890', NULL, '2019-01-12 01:12:00 AM', 0),
('GoshoQkiq', '636363', NULL, '2018-03-12 11:12:00 PM', 1),
('PeshoPesho', 'Pesho123', NULL, '2019-01-01 00:00:01 AM', 1),
('KosmonavtBG', 'kosmo122', NULL, '2018-12-31 11:59:59 PM', 0),
('SoftUniIsCool', 'SoftUniTheBest', NULL, '2019-01-14 06:00:00 PM', 1)
GO

/*Problem 9.*/
ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC0788B375E3

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY(Id, Username)
GO

/*Problem 10.*/
ALTER TABLE Users
ADD CONSTRAINT PasswordLength CHECK(LEN(Password) >= 5)
GO

/*Problem 11.*/
ALTER TABLE Users
ADD DEFAULT GETDATE() FOR LastLoginTime
GO

/*Problem 12.*/
ALTER TABLE Users
DROP CONSTRAINT PK_Users

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id)

ALTER TABLE Users
ADD CONSTRAINT UQ_Username
UNIQUE (Username)

ALTER TABLE Users
ADD CONSTRAINT CheckUsername
CHECK (LEN(Username) >= 3)
GO

/*Problem 13.*/
CREATE DATABASE Movies

CREATE TABLE Directors(
Id INT PRIMARY KEY IDENTITY,
DirectorName NVARCHAR(50) NOT NULL UNIQUE,
Notes NVARCHAR(MAX)
)

CREATE TABLE Genres(
Id INT PRIMARY KEY IDENTITY,
GenreName NVARCHAR(50) NOT NULL UNIQUE,
Notes NVARCHAR(MAX)
)

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(50) NOT NULL UNIQUE,
Notes NVARCHAR(MAX)
)

CREATE TABLE Movies(
Id INT PRIMARY KEY IDENTITY,
Title NVARCHAR(50) NOT NULL,
DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
CopyrightYear INT NOT NULL,
[Length] TIME NOT NULL,
GenreId INT FOREIGN KEY REFERENCES Genres(Id),
CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
Rating DECIMAL(3, 1),
Notes NVARCHAR(MAX)
)

INSERT INTO Directors (DirectorName, Notes) VALUES
('Ivan Petrov', NULL),
('Pesho Goshev', 'Very nice guy.'),
('Gosho Peshev','Might have somethign to do with Pesho Goshev.'),
('Tamara Tamareva',NULL),
('Jane Doe','New director. Do not have a clue.')

INSERT INTO Genres (GenreName, Notes) VALUES
('Horror','Very frightening, much scare. WOW!'),
('Empty',NULL),
('Comedy','Every laugh counts.'),
('Drama','We had enough of these!'),
('Anime','Is that even a real thing?')

INSERT INTO Categories (CategoryName, Notes) VALUES
('Popular','Only most popular movies allowed.'),
('Weird','Totally worth watching.'),
('MehHh',NULL),
('4K Movies','Super resolution, much quality. WOW!'),
('Other','Not sure what to put here!')

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes) VALUES
('Adventures of Pesho', 2, 2010, '4:50', 3, 2, NULL, 'Really weird story.'),
('Spongebob Cpants', 1, 2017, '1:15', 1, 5, 9.2, 'So nice.'),
('Programmer-man', 3, 2019, '5:20', 4, 4, 10.0, NULL),
('Chonicles of SoftUni', 5, 1999, '1:20', 5, 1, 8.8, NULL),
('The Empty', 5, 2000, '0:01', 2, 3, 6.6, 'A little short')
GO

/*Problem 14*/
CREATE DATABASE CarRental

CREATE TABLE Categories (
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(50) NOT NULL,
DailyRate DECIMAL(15,2) NOT NULL,
WeeklyRate DECIMAL(15,2) NOT NULL,
MonthlyRate DECIMAL(15,2) NOT NULL,
WeekendRate DECIMAL(15,2) NOT NULL
)

CREATE TABLE Cars (
Id INT PRIMARY KEY IDENTITY,
PlateNumber NVARCHAR(50) NOT NULL UNIQUE,
Manufacturer NVARCHAR(50) NOT NULL,
Model NVARCHAR(50) NOT NULL,
CarYear INT NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
Doors INT,
Picture VARBINARY(MAX),
Condition NVARCHAR(50),
Available BIT NOT NULL
)

CREATE TABLE Employees (
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Title NVARCHAR(50),
Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
Id INT PRIMARY KEY IDENTITY,
DriverLicenceNumber NVARCHAR(20) NOT NULL,
FullName NVARCHAR(100) NOT NULL,
[Address] NVARCHAR(300) NOT NULL,
City NVARCHAR(100) NOT NULL,
ZIPCore NVARCHAR(20),
Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders (
Id INT PRIMARY KEY IDENTITY,
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
CarId INT FOREIGN KEY REFERENCES Cars(Id),
TankLevel INT NOT NULL,
KilometrageStart INT NOT NULL,
KilometrageEnd INT NOT NULL,
TotalKilometrage AS KilometrageEnd - KilometrageStart,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
RateApplied DECIMAL(15,2) NOT NULL,
TaxRate AS RateApplied* 0.2,
OrderStatus NVARCHAR(300) NOT NULL,
Notes NVARCHAR(MAX)
)

INSERT INTO Categories VALUES
('Sedan', 65, 350, 1350, 120),
('Urban-SUV', 85, 500, 1800, 160),
('Electric', 40, 230, 850, 70)

INSERT INTO Cars VALUES
('B8877PP', 'Audi', 'A6', 2001, 1, 4, NULL, 'Good', 1),
('GH17GH78', 'Tesla', 'Model 3', 2018, 3, 5, NULL, 'Very good', 0),
('CT17754GT', 'VW', 'T-Roc', 2018, 2, 5, NULL, 'Zufrieden', 1)

INSERT INTO Employees VALUES
('Peshko', 'Mihaylov', NULL, NULL),
('Doncho', 'Ivanov', NULL, NULL),
('Master', 'Snoke', 'Supreme Leader', 'Come to Dark Side')

INSERT INTO Customers(DriverLicenceNumber, FullName, Address, City) VALUES
('AZ18555PO', 'Michael Smith', 'Medley str. 25', 'Chikago'),
('LJ785554478', 'Sergey Ivankov', 'Shtaigich 37', 'Perm'),
('LK8555478', 'Franc Joshua', 'Dorcel str. 56', 'Paris')

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
StartDate, EndDate, RateApplied, OrderStatus) VALUES
(1, 2, 3, 45, 18005, 19855, '2007-08-08', '2007-08-10', 250, 1),
(3, 2, 1, 50, 55524, 56984, '2009-09-06', '2009-09-28', 1500, 0),
(2, 2, 1, 18, 36005, 38547, '2017-05-08', '2017-06-09', 850, 0)
GO

/*Problem 15.*/
CREATE DATABASE Hotel

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(500)
)

CREATE TABLE Customers (
	AccountNumber INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	PhoneNumber NVARCHAR(30),
	EmergencyName NVARCHAR(30),
	EmergencyNumber NVARCHAR(30),
	Notes NVARCHAR(500) 
)

CREATE TABLE RoomStatus (
	RoomStatus NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(500)
)

CREATE TABLE RoomTypes (
	RoomType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(500)
)

CREATE TABLE BedTypes (
	BedType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(500)
)

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY NOT NULL,
	RoomType NVARCHAR(50) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL,
	BedType NVARCHAR(50) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL,
	Rate DECIMAL(6,2) NOT NULL,
	RoomStatus BIT NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE Payments (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	PaymentDate DATETIME NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
	AmountCharged DECIMAL(7, 2) NOT NULL,
	TaxRate DECIMAL(6,2) NOT NULL,
	TaxAmount AS AmountCharged * TaxRate,
	PaymentTotal AS AmountCharged + AmountCharged * TaxRate,
	Notes NVARCHAR(1500)
)

CREATE TABLE Occupancies (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	DateOccupied DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber) NOT NULL,
	RateApplied DECIMAL(7, 2) NOT NULL,
	PhoneCharge DECIMAL(8, 2) NOT NULL,
	Notes NVARCHAR(1000)
)

INSERT INTO Employees(FirstName, LastNAme) VALUES
('Galin', 'Zhelev'),
('Stoyan', 'Ivanov'),
('Petar', 'Ikonomov')

INSERT INTO Customers(FirstName, LastName, PhoneNumber) VALUES
('Monio', 'Ushev', '+359888666555'),
('Gancho', 'Stoykov', '+359866444222'),
('Genadi', 'Dimchov', '+35977555333')

INSERT INTO RoomStatus(RoomStatus) VALUES
('occupied'),
('non occupied'),
('repairs')

INSERT INTO RoomTypes(RoomType) VALUES
('single'),
('double'),
('appartment')

INSERT INTO BedTypes(BedType) VALUES
('single'),
('double'),
('couch')

INSERT INTO Rooms(RoomNumber, RoomType, BedType, Rate, RoomStatus) VALUES
(201, 'single', 'single', 40.0, 1),
(205, 'double', 'double', 70.0, 0),
(208, 'appartment', 'double', 110.0, 1)

INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate) VALUES
(1, '2011-11-25', 2, '2017-11-30', '2017-12-04', 250.0, 0.2),
(3, '2014-06-03', 3, '2014-06-06', '2014-06-09', 340.0, 0.2),
(3, '2016-02-25', 2, '2016-02-27', '2016-03-04', 500.0, 0.2)

INSERT INTO Occupancies(EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge) VALUES
(2, '2011-02-04', 3, 205, 70.0, 12.54),
(2, '2015-04-09', 1, 201, 40.0, 11.22),
(3, '2012-06-08', 2, 208, 110.0, 10.05)
GO

/*Problem 16.*/
CREATE DATABASE SoftUni

CREATE TABLE Towns (
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses(
Id INT PRIMARY KEY IDENTITY,
AddressText NVARCHAR(300) NOT NULL,
TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments (
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees (
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
MiddleName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
JobTitle NVARCHAR(100) NOT NULL,
DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
HireDate DATE NOT NULL,
Salary DECIMAL(15,2) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)
GO

/*Problem 17.*/
/*Done*/
GO

/*Problem 18.*/
INSERT INTO Towns ([Name]) VALUES
('Sofia'),
('Plovidv'),
('Varna'),
('Burgas')

INSERT INTO Departments VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Addresses VALUES
('Tam tam 12', 1),
('Tam tuk 22', 2),
('Tuk tuk 21', 3),
('Tuk tam', 4)

INSERT INTO Employees VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00, 1),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00, 2),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25, 3),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00, 4),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88, 1)
GO

/*Problem 19.*/
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees
GO

/*Problem 20.*/
SELECT * FROM Towns
ORDER BY [Name]

SELECT * FROM Departments
ORDER BY [Name]

SELECT * FROM Employees
ORDER BY Salary DESC
GO

/*Problem 21.*/
SELECT [Name] FROM Towns
ORDER BY [Name]

SELECT [Name] FROM Departments
ORDER BY [Name]

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC
GO

/*Problem 22.*/
UPDATE Employees
SET Salary = Salary * 1.10

SELECT Salary FROM Employees
GO

/*Problem 23.*/
USE Hotel

UPDATE Payments
SET TaxRate = TaxRate * 0.97

SELECT TaxRate From Payments
Go

/*Problem 24.*/
DELETE FROM Occupancies

SELECT * FROM Occupancies