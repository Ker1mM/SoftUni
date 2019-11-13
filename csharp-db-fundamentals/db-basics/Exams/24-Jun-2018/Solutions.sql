USE master
DROP DATABASE TripService
CREATE DATABASE TripService
USE TripService

--Problem 1.
CREATE TABLE Cities
(
  Id          INT PRIMARY KEY IDENTITY,
  [Name]      NVARCHAR(20) NOT NULL,
  CountryCode CHAR(2)      NOT NULL
)

CREATE TABLE Hotels
(
  Id            INT PRIMARY KEY IDENTITY,
  [Name]        NVARCHAR(30)   NOT NULL,
  CityId        INT            NOT NULL FOREIGN KEY REFERENCES Cities (Id),
  EmployeeCount INT            NOT NULL,
  BaseRate      DECIMAL(15, 2) NOT NULL
)

CREATE TABLE Rooms
(
  Id      INT PRIMARY KEY IDENTITY,
  Price   DECIMAL(15, 2) NOT NULL,
  Type    NVARCHAR(20)   NOT NULL,
  Beds    INT            NOT NULL,
  HotelId INT            NOT NULL FOREIGN KEY REFERENCES Hotels (Id)
)

CREATE TABLE Trips
(
  Id          INT PRIMARY KEY IDENTITY,
  RoomId      INT  NOT NULL FOREIGN KEY REFERENCES Rooms (Id),
  BookDate    DATE NOT NULL,
  ArrivalDate DATE NOT NULL,
  ReturnDate  DATE NOT NULL,
  CancelDate  DATE,
  CONSTRAINT ck_CheckBookDateBeforeArrivalDate
    CHECK (BookDate < ArrivalDate),
  CONSTRAINT ck_CheckArrivalDateBeforeReturnDate
    CHECK (ArrivalDate < ReturnDate)
)

CREATE TABLE Accounts
(
  Id         INT PRIMARY KEY IDENTITY,
  FirstName  NVARCHAR(50) NOT NULL,
  MiddleName NVARCHAR(20),
  LastName   NVARCHAR(50) NOT NULL,
  CityId     INT          NOT NULL FOREIGN KEY REFERENCES Cities (Id),
  BirthDate  DATE         NOT NULL,
  Email      VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips
(
  AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts (Id),
  TripId    INT NOT NULL FOREIGN KEY REFERENCES Trips (Id),
  Luggage   INT NOT NULL,
  CONSTRAINT PK_AccountsTrips PRIMARY KEY (AccountId, TripId),
  CONSTRAINT ck_LuggageMinZero
    CHECK (Luggage >= 0)
)
GO

--Problem 2.
INSERT INTO Accounts(FirstName, MiddleName, LastName, CityId, BirthDate, Email)
VALUES ('John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'),
       ('Gosho', NULL, 'Petrov', 11, '1978-05-16', 'g_petrov@gmail.com'),
       ('Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
       ('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')


INSERT INTO Trips(RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate)
VALUES (101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),
       (102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
       (103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
       (104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
       (109, '2017-08-07', '2017-08-28', '2017-08-29', NULL)
GO

--Problem 3.
UPDATE Rooms
SET Price *= 1.14
WHERE HotelId IN (5, 7, 9)
GO

--Problem 4.
DELETE
FROM AccountsTrips
WHERE AccountId = 47
GO

--Problem 5.
SELECT Id, [Name]
FROM Cities
WHERE CountryCode = 'BG'
ORDER BY Name

--Problem 18.
CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
  RETURNS NVARCHAR(MAX) AS
BEGIN
  DECLARE @Result NVARCHAR(MAX)
  DECLARE @TotalPrice DECIMAL(15, 2)
  DECLARE @RoomType NVARCHAR(20)
  DECLARE @BedsCount INT
  DECLARE @RoomId INT

  SELECT TOP 1 @RoomId = a.Id,
               @BedsCount = a.Beds,
               @RoomType = a.Type,
               @TotalPrice = a.TotalPrice
  FROM (SELECT r.ID, r.Beds, r.Type, ((h.BaseRate + r.Price) * 2) AS TotalPrice
        FROM Rooms r
               JOIN Hotels h ON h.Id = @HotelId AND r.HotelId = h.Id
               JOIN Trips t ON t.RoomId = r.Id
        WHERE r.Beds >= @People
          AND NOT EXISTS(SELECT rs.ID
                         FROM Rooms rs
                                JOIN Trips ts ON ts.RoomId = rs.Id
                         WHERE @Date BETWEEN ts.ArrivalDate AND ts.ReturnDate
                           AND ts.CancelDate IS NULL
                           AND rs.Id = r.id)) AS a
  ORDER BY a.TotalPrice DESC

  IF (@BedsCount IS NOT NULL)
    BEGIN
      SET @Result = CONCAT('Room ', @RoomId, ': ', @RoomType, ' (', @BedsCount, ' beds) - $', @TotalPrice)
    END
  ELSE
    BEGIN
      SET @Result = 'No rooms available'
    END

  RETURN @Result
END
GO

--Problem 19.
CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT) AS
BEGIN
  BEGIN TRANSACTION
    DECLARE @TripHotelId INT =
      (SELECT r.HotelId
       FROM Rooms r
              JOIN Trips t ON t.RoomId = r.Id AND
                              t.id = @TripId)

    DECLARE @TripPeople INT =
      (SELECT COUNT(a.Id)
       FROM AccountsTrips ats
              JOIN Accounts a ON a.Id = ats.AccountId AND
                                 ats.TripId = @TripId)


    DECLARE @TargetRoomHotelId INT
    DECLARE @TargetRoomBedCount INT

    SELECT @TargetRoomHotelId = r.HotelId,
           @TargetRoomBedCount = r.Beds
    FROM Rooms r
    WHERE r.Id = @TargetRoomId

    UPDATE Trips
    SET RoomId = @TargetRoomId
    WHERE Id = @TripId

    IF (@TripHotelId <> @TargetRoomHotelId)
      BEGIN
        ROLLBACK
        RAISERROR ('Target room is in another hotel!', 16, 1)
        RETURN
      END

    IF (@TargetRoomBedCount < @TripPeople)
      BEGIN
        ROLLBACK
        RAISERROR ('Not enough beds in target room!', 16, 1)
        RETURN
      END

  COMMIT TRANSACTION
END
GO

--Problem 20.
CREATE TRIGGER tr_CancelTripOnDelete
  ON Trips
  INSTEAD OF DELETE AS
BEGIN
  UPDATE Trips
  SET Trips.CancelDate = GETDATE()
  WHERE Trips.Id IN (SELECT id
                     FROM deleted
                     WHERE Trips.CancelDate IS NULL)
end