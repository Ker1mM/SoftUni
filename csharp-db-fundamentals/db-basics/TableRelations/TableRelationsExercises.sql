USE SoftUni

--Problem 1.
CREATE TABLE Persons
(
  PersonID INT NOT NULL,
  FirstName NVARCHAR(50) NOT NULL,
  Salary DECIMAL(15, 2) NOT NULL,
  PassportID INT NOT NULL
)

CREATE TABLE Passports
(
  PassportID INT NOT NULL,
  PassportNumber NVARCHAR(50) NOT NULL
)

INSERT INTO Persons VALUES
(1, 'Roberto', 43300.00, 102),
(2, 'Tom', 56100.00, 103),
(3, 'Yana', 60200.00, 101)

INSERT INTO Passports VALUES
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

ALTER TABLE Persons
ADD CONSTRAINT PK_Persons PRIMARY KEY (PersonID)

ALTER TABLE Passports
ADD CONSTRAINT PK_PassportID PRIMARY KEY (PassportID)

ALTER TABLE Persons
ADD CONSTRAINT FK_PassportID
FOREIGN KEY (PassportID) REFERENCES Passports (PassportID)

--Problem 2.
CREATE TABLE Models
(
  ModelID INT NOT NULL,
  [Name] NVARCHAR(50) NOT NULL,
  ManufacturerID INT NOT NULL
)

CREATE TABLE Manufacturers
(
  ManufacturerID INT NOT NULL IDENTITY,
  [Name] NVARCHAR(50) NOT NULL,
  EstablishedOn DATETIME NOT NULL
)

INSERT INTO Manufacturers VALUES
('BMW', '1916-03-07'),
('Tesla', '2003-01-01'),
('Lada', '1966-05-01')

INSERT INTO Models VALUES
(101, 'X1', 1),
(102, 'i6', 1),
(103, 'Model S', 2),
(104, 'Model X', 2),
(105, 'Model 3', 2),
(106, 'Nova', 3)

ALTER TABLE Models
ADD CONSTRAINT PK_ModelID
PRIMARY KEY (ModelID)

ALTER TABLE Manufacturers
ADD CONSTRAINT PK_ManufacturerID
PRIMARY KEY (ManufacturerID)

ALTER TABLE Models
ADD CONSTRAINT FK_ManifacturerID_Manufacturers
FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)

--Problem 3.
CREATE TABLE Students
(
  StudentID INT NOT NULL,
  [Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Exams
(
  ExamID INT NOT NULL,
  [Name] NVARCHAR(50)
)

CREATE TABLE StudentsExams
(
  StudentID INT NOT NULL,
  ExamID INT NOT NULL
)

INSERT INTO Students VALUES
(1, 'Mila'),
(2, 'Toni'),
(3, 'Ron')

INSERT INTO Exams VALUES
(101, 'SpringMVC'),
(102, 'Neo4j'),
(103, 'Oracle 11g')

INSERT INTO StudentsExams VALUES
(1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)

ALTER TABLE Students
ADD CONSTRAINT PK_StudentID
PRIMARY KEY (StudentID)

ALTER TABLE Exams
ADD CONSTRAINT PK_Exams
PRIMARY KEY (ExamID)

ALTER TABLE StudentsExams
ADD CONSTRAINT PK_StudentID_ExamID
PRIMARY KEY (StudentID, ExamID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_StudentID_Students
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_ExamID_Exams
FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)

--Problem 4.
CREATE TABLE Teachers
(
  TeacherID INT NOT NULL,
  [Name] NVARCHAR(50) NOT NULL,
  ManagerID INT
)

INSERT INTO Teachers VALUES
(101, 'John', NULL),
(102, 'Maya', 106),
(103, 'Silvia', 106),
(104, 'Ted', 105),
(105, 'Mark', 101),
(106, 'Greta', 101)

ALTER TABLE Teachers
ADD CONSTRAINT PK_TeacherID
PRIMARY KEY (TeacherID)

ALTER TABLE Teachers
ADD CONSTRAINT FK_ManagerID_TeacherID
FOREIGN KEY (ManagerID) REFERENCES Teachers(TeacherID)

--Problem 5.
CREATE TABLE Cities
(
  CityID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50)
)

CREATE TABLE Customers
(
  CustomerID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50),
  Birthday DATE,
  CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)

CREATE TABLE ItemTypes
(
  ItemTypeID INT IDENTITY PRIMARY KEY,
  [Name] VARCHAR(50)
)

CREATE TABLE Items
(
  ItemID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50),
  ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE Orders
(
  OrderID INT PRIMARY KEY IDENTITY,
  CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE OrderItems
(
  OrderID INT,
  ItemID INT,
  PRIMARY KEY (OrderID, ItemID),
  FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
  FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
)

--Problem 6.
CREATE DATABASE University
USE University

CREATE TABLE Majors
(
  MajorID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Students
(
  StudentID INT PRIMARY KEY IDENTITY,
  StudentNumber VARCHAR(50) NOT NULL,
  StudentName VARCHAR(50) NOT NULL,
  MajorID INT FOREIGN KEY REFERENCES Majors(MajorID)
)

CREATE TABLE Payments
(
  PaymentID INT PRIMARY KEY IDENTITY,
  PaymentDate DATE NOT NULL,
  PaymentAmount DECIMAL(15, 2) NOT NULL,
  StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
)

CREATE TABLE Subjects
(
  SubjectID INT PRIMARY KEY IDENTITY,
  SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Agenda
(
  StudentID INT NOT NULL,
  SubjectID INT NOT NULL,
  PRIMARY KEY (StudentID, SubjectID),
  FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
  FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
)

--Problem 9.
SELECT M.MountainRange, P.PeakName, P.Elevation
FROM Mountains AS M
JOIN Peaks P on M.Id = P.MountainId
WHERE M.MountainRange = 'Rila'
ORDER BY P.Elevation DESC