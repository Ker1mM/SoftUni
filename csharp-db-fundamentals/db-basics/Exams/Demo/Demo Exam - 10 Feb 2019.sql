

USE master
DROP DATABASE ColonialJourney
CREATE DATABASE ColonialJourney
USE ColonialJourney

--Problem 1. DONE
CREATE TABLE Planets (
  Id INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Spaceports (
  Id INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50) NOT NULL,
  PlanetId INT NOT NULL FOREIGN KEY REFERENCES Planets(Id)
)

CREATE TABLE Spaceships (
  Id INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50) NOT NULL,
  Manufacturer VARCHAR(30) NOT NULL,
  LightSpeedRate INT DEFAULT(0)
)

CREATE TABLE Colonists (
  Id INT PRIMARY KEY IDENTITY,
  FirstName VARCHAR(20) NOT NULL,
  LastName VARCHAR(20) NOT NULL,
  Ucn VARCHAR(10) NOT NULL UNIQUE,
  BirthDate DATE NOT NULL
)

CREATE TABLE Journeys (
  Id INT PRIMARY KEY IDENTITY,
  JourneyStart DATETIME NOT NULL,
  JourneyEnd DATETIME NOT NULL,
  Purpose VARCHAR(11) NOT NULL,
  DestinationSpaceportId INT NOT NULL FOREIGN KEY REFERENCES Spaceports(Id),
  SpaceshipId INT NOT NULL FOREIGN KEY REFERENCES Spaceships(Id),
  CONSTRAINT ck_CheckPurpose
    CHECK (Purpose = 'Medical' OR
           Purpose = 'Technical' OR
           Purpose = 'Educational' OR
           Purpose = 'Military')
)

CREATE TABLE TravelCards (
  Id INT PRIMARY KEY IDENTITY,
  CardNumber CHAR(10) NOT NULL,
  JobDuringJourney VARCHAR(8) NOT NULL,
  ColonistId INT NOT NULL FOREIGN KEY REFERENCES Colonists(Id),
  JourneyId INT NOT NULL FOREIGN KEY REFERENCES Journeys(Id),
  CONSTRAINT ck_CheckCardNumberLength CHECK (LEN(LTRIM(CardNumber)) = 10),
  CONSTRAINT ck_JobDuringJourney
    CHECK (JobDuringJourney = 'Pilot' OR
           JobDuringJourney = 'Engineer' OR
           JobDuringJourney = 'Trooper' OR
           JobDuringJourney = 'Cleaner' OR
           JobDuringJourney = 'Cook')
)

--Problem 2. DONE
INSERT INTO Planets ([Name]) VALUES
('Mars'),
('Earth'),
('Jupiter'),
('Saturn')

INSERT INTO Spaceships ([Name], Manufacturer, LightSpeedRate) VALUES
('Golf', 'VW', 3),
('WakaWaka', 'Wakanda', 4),
('Falcon9', 'SpaceX', 1),
('Bed', 'Vidolov', 6)

--Problem 3. DONE
UPDATE Spaceships
SET LightSpeedRate += 1
WHERE Id BETWEEN 8 AND 12

--Problem 4. DONE
DELETE FROM TravelCards
WHERE JourneyId IN (1,2,3)

DELETE FROM Journeys
WHERE Id IN (1,2,3)

--Problem 5. DONE
SELECT CardNumber, JobDuringJourney FROM TravelCards
ORDER BY CardNumber ASC

--Problem 6. DONE
SELECT Id, CONCAT(FirstName, ' ', LastName) AS FullName, Ucn FROM Colonists
ORDER BY FirstName ASC, LastName ASC, Id ASC

--Problem 7. DONE
SELECT Id,
       CONVERT(VARCHAR(10),JourneyStart, 103) AS JourneyStar,
       CONVERT(VARCHAR(10), JourneyEnd, 103) AS JourneyEnd
FROM Journeys
WHERE Purpose = 'Military'
ORDER BY JourneyStart ASC

--Problem 8. DONE
SELECT c.Id, CONCAT(c.FirstName, ' ', c.LastName) AS [Full_Name]
FROM Colonists c
JOIN TravelCards tc ON tc.ColonistId = c.Id
WHERE tc.JobDuringJourney = 'Pilot'
ORDER BY Id ASC

--Problem 9. DONE
SELECT COUNT(c.FirstName) AS Count
FROM Colonists c
JOIN TravelCards tc ON tc.ColonistId = c.Id
JOIN Journeys j ON j.Id = tc.JourneyId AND j.Purpose = 'Technical'

--Problem 10.
SELECT TOP 1 sps.Name AS SpaceshipName, spp.Name AS SpaceportName
FROM Journeys j
JOIN Spaceships sps ON sps.Id = j.SpaceshipId
JOIN Spaceports spp ON spp.Id = j.DestinationSpaceportId
ORDER BY sps.LightSpeedRate DESC

--Problem 11. DONE
SELECT sps.Name, sps.Manufacturer
FROM Spaceships sps
JOIN Journeys j ON j.SpaceshipId = sps.Id
JOIN TravelCards tc ON tc.JourneyId = j.Id
JOIN Colonists c ON c.Id = tc.ColonistId
WHERE JobDuringJourney = 'Pilot' AND DATEADD(YEAR, 30,c.BirthDate) > '2019-01-01'
ORDER BY Name ASC

--Problem 12. DONE
SELECT p.Name AS PlanetName, spp.Name AS SpaceportName
FROM Spaceports spp
JOIN Planets p ON p.Id = spp.PlanetId
JOIN Journeys j ON j.DestinationSpaceportId = spp.Id
WHERE j.Purpose = 'Educational'
ORDER BY SpaceportName DESC

--Problem 13. DONE
SELECT p.Name AS PlanetName, COUNT(j.Id) AS JourneysCount
FROM Planets p
JOIN Spaceports spp ON spp.PlanetId = p.Id
JOIN Journeys j ON j.DestinationSpaceportId = spp.Id
GROUP BY p.Name
ORDER BY JourneysCount DESC, PlanetName ASC

--Problem 14. DONE
SELECT TOP 1 j.Id,
             p.Name AS PlanetName,
             spp.Name AS SpaceportName,
             j.Purpose AS JourneyPurpose
FROM Journeys j
JOIN Spaceports spp ON j.DestinationSpaceportId = spp.Id
JOIN Planets p ON spp.PlanetId = p.Id
ORDER BY DATEDIFF(DAY, j.JourneyStart, j.JourneyEnd) DESC

SELECT j.JourneyStart, J.JourneyEnd, spp.Name AS SpaceportName
FROM Journeys j
JOIN Spaceports spp ON j.DestinationSpaceportId = spp.Id

--Problem 15. DONE
SELECT TOP 1 a.Id AS JourneyId,
       tc.JobDuringJourney AS JobName
FROM (
  SELECT TOP 1 j.Id
  from Journeys j
  ORDER BY DATEDIFF(DAY, j.JourneyStart, j.JourneyEnd) DESC) AS a
JOIN TravelCards tc ON a.Id = tc.JourneyId
GROUP BY tc.JobDuringJourney, a.Id
ORDER BY COUNT(tc.JobDuringJourney) ASC

--Problem 16. DONE
SELECT * FROM
  (SELECT tc.JobDuringJourney,
        CONCAT(c.FirstName, ' ', c.LastName) AS FullName,
        RANK() over (PARTITION BY tc.JobDuringJourney ORDER BY c.BirthDate ASC) AS JobRank
  FROM TravelCards tc
  JOIN Colonists c ON tc.ColonistId = c.Id) AS a
WHERE a.JobRank = 2

--Problem 17. DONE
SELECT p.Name,
       COUNT(spp.Id) AS Count
FROM Planets p
LEFT JOIN Spaceports spp ON spp.PlanetId = p.Id
GROUP BY p.Name
ORDER BY Count DESC, p.Name ASC

--Problem 18. DONE
CREATE FUNCTION udf_GetColonistsCount(@PlanetName VARCHAR(30))
RETURNS INT AS
BEGIN
  DECLARE @Result INT
      SELECT @Result = COUNT(c.Id)
      FROM Planets p
      JOIN Spaceports spp ON p.Id = spp.PlanetId
      JOIN Journeys j ON j.DestinationSpaceportId = spp.Id
      JOIN TravelCards tc ON tc.JourneyId = j.Id
      JOIN Colonists c ON c.Id = tc.JourneyId
      WHERE p.Name = @PlanetName
      GROUP BY p.Name

  IF(@Result IS NULL)
    SET @Result = 0

  RETURN @Result
END

DROP FUNCTION udf_GetColonistsCount
SELECT dbo.udf_GetColonistsCount('Otroyphus')

--Problem 19. DONE
CREATE PROCEDURE usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(11)) AS
  BEGIN
    BEGIN TRANSACTION

      DECLARE @OldPurpose VARCHAR(11)
      SELECT @OldPurpose = j.Purpose FROM Journeys j
        WHERE j.Id = @JourneyId

      UPDATE Journeys
      SET Purpose = @NewPurpose
      WHERE Id = @JourneyId

      IF(@@ROWCOUNT = 0)
        BEGIN
          ROLLBACK
          RAISERROR ('The journey does not exist!', 16, 1)
        END

      IF(@OldPurpose = @NewPurpose)
        BEGIN
          ROLLBACK
          RAISERROR ('You cannot change the purpose!', 16, 1)
        END

    COMMIT TRANSACTION
  END
GO

--Problem 20.
CREATE TABLE DeletedJourneys (
  Id INT PRIMARY KEY,
  JourneyStart DATETIME NOT NULL,
  JourneyEnd DATETIME NOT NULL,
  Purpose VARCHAR(11) NOT NULL,
  DestinationSpaceportId INT NOT NULL FOREIGN KEY REFERENCES Spaceports(Id),
  SpaceshipId INT NOT NULL FOREIGN KEY REFERENCES Spaceships(Id),
  CONSTRAINT ck_CheckPurposeOnDeletedJourneys
    CHECK (Purpose = 'Medical' OR
           Purpose = 'Technical' OR
           Purpose = 'Educational' OR
           Purpose = 'Military')
)

CREATE TRIGGER tr_DeletedJourneysInsert ON Journeys AFTER DELETE AS
  BEGIN
    INSERT INTO DeletedJourneys (Id, JourneyStart, JourneyEnd, Purpose, DestinationSpaceportId, SpaceshipId)
    SELECT de.Id,
           de.JourneyStart,
           de.JourneyEnd,
           de.Purpose,
           de.DestinationSpaceportId,
           de.SpaceshipId
    FROM deleted de
  END

