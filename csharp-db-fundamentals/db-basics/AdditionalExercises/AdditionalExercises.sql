--Problem 1.
SELECT t.Domain AS [Email Provider] ,COUNT(*) AS [Number Of Users] FROM
  (SELECT
       SUBSTRING(Email, CHARINDEX('@', Email) +1, LEN(Email)-CHARINDEX('@', Email)) AS Domain
  FROM Users ) AS t
GROUP BY Domain
ORDER BY [Number Of Users] DESC, t.Domain ASC

--Problem 2.
SELECT g.Name AS [Game],
       gt.Name AS [Game Type],
       u.Username,
       us.Level,
       us.Cash,
       c.Name
FROM UsersGames us
    INNER JOIN Games g ON us.GameId = g.Id
    INNER JOIN GameTypes gt ON g.GameTypeId = gt.Id
    INNER JOIN Users u ON us.UserId = u.Id
    INNER JOIN Characters c ON us.CharacterId = c.Id
ORDER BY Level DESC, Username, Game

--Problem 3.
SELECT u.Username,
       g.Name AS Game,
       COUNT(ugi.ItemId) AS [Items Count],
       SUM(i.Price) AS [Items Price]
FROM Users u
JOIN UsersGames ug ON u.Id = ug.UserId
JOIN Games g ON g.Id = ug.GameId
JOIN UserGameItems ugi ON ugi.UserGameId = ug.Id
JOIN Items i ON i.Id = ugi.ItemId
GROUP BY Username, g.Name
HAVING COUNT(ugi.ItemId) >= 10
ORDER BY COUNT(ugi.ItemId) DESC, SUM(i.Price) DESC, Username ASC

--Problem 4.
SELECT u.Username,
       g.Name AS Game,
       MAX(c.Name) AS Character,
       SUM(its.Strength) + MAX(gts.Strength) + MAX(cs.Strength) AS Strength,
       SUM(its.Defence) + MAX(gts.Defence) + MAX(cs.Defence) AS Defence,
       SUM(its.Speed) + MAX(gts.Speed) + MAX(cs.Speed) AS Speed,
       SUM(its.Mind) + MAX(gts.Mind) + MAX(cs.Mind) AS Mind,
       SUM(its.Luck) + MAX(gts.Luck) + MAX(cs.Luck) AS Luck
FROM Users u
JOIN UsersGames ug ON ug.UserId = u.Id
JOIN Games g ON g.Id = ug.GameId
JOIN GameTypes gt ON gt.Id = g.GameTypeId
JOIN [Statistics] gts ON gts.id = gt.BonusStatsId
JOIN Characters c ON c.Id = ug.CharacterId
JOIN [Statistics] cs ON cs.Id = c.StatisticId
JOIN UserGameItems ugi ON ugi.UserGameId = ug.Id
JOIN Items i ON ugi.ItemId = i.id
JOIN [Statistics] its ON its.Id = i.StatisticId
GROUP BY u.Username, g.Name
ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC

--Problem 5.
SELECT i.Name, i.Price, i.MinLevel, st.Strength, st.Defence, st.Speed, st.Luck, st.Mind
FROM Items i
JOIN [Statistics] st ON st.Id = i.StatisticId
WHERE st.Mind > (SELECT AVG(Mind) FROM [Statistics]) AND
      st.Luck > (SELECT AVG(Luck) FROM [Statistics]) AND
      st.Speed > (SELECT AVG(Speed) FROM [Statistics])
ORDER BY Name ASC

--Problem 6.
SELECT i.Name AS Item,
       i.Price,
       i.MinLevel,
       gt.Name AS [Forbidden Game Type]
FROM Items i
LEFT JOIN GameTypeForbiddenItems fi ON i.Id = fi.ItemId
LEFT JOIN GameTypes gt ON fi.GameTypeId = gt.Id
ORDER BY [Forbidden Game Type] DESC, Item ASC

--Problem 7.
INSERT INTO UserGameItems VALUES
(51, 235), --756 , 4
(71, 235), --90 , 67
(157, 235), --412 , 21
(184, 235), --726 , 12
(197, 235), --772 , 3
(223, 235) --201 , 46

UPDATE UsersGames
SET Cash -= (756 + 90 + 412 + 726+ 772+201)
WHERE Id = 235

SELECT u.Username,
       g.Name,
       ug.Cash,
       i.Name AS [Item Name]
FROM Users u
JOIN UsersGames ug ON ug.GameId = 221 AND u.Id = ug.UserId
JOIN Games g ON ug.GameId = g.Id
JOIN UserGameItems ugi ON ugi.UserGameId = ug.Id
JOIN Items i ON i.Id = ugi.ItemId
ORDER BY [Item Name]

--Problem 8.
SELECT p.PeakName, mt.MountainRange AS Mountain, p.Elevation
FROM Peaks p
JOIN Mountains mt ON p.MountainId = mt.Id
ORDER BY  Elevation DESC, PeakName

--Problem 9.
SELECT p.PeakName,
       mt.MountainRange AS Mountain,
       c.CountryName,
       ct.ContinentName
FROM Peaks p
JOIN Mountains mt ON mt.Id = p.MountainId
JOIN MountainsCountries mc ON mc.MountainId = mt.Id
JOIN Countries c ON mc.CountryCode = c.CountryCode
JOIN Continents ct ON ct.ContinentCode = c.ContinentCode
ORDER BY PeakName ASC, CountryName

--Problem 10.
SELECT c.CountryName,
       ct.ContinentName,
       COUNT(r.RiverName) AS RiversCount,
       ISNULL(SUM(r.Length), 0) AS TotalLength
FROM Countries c
LEFT JOIN Continents ct ON c.ContinentCode = ct.ContinentCode
LEFT JOIN CountriesRivers cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers r ON r.Id = cr.RiverId
GROUP BY CountryName, ContinentName
ORDER BY RiversCount DESC, TotalLength DESC, CountryName

--Problem 11.
SELECT cc.CurrencyCode,
       cc.Description AS Currency,
       COUNT(c.CountryName) AS NumberOfCountries
FROM Currencies cc
LEFT JOIN Countries c ON cc.CurrencyCode = c.CurrencyCode
GROUP BY cc.CurrencyCode, Description
ORDER BY NumberOfCountries DESC, Currency

--Problem 12.
SELECT ct.ContinentName,
       SUM(CAST(c.AreaInSqKm AS BIGINT)) AS CountriesArea,
       SUM(CAST(c.Population AS BIGINT)) AS CountriesPopulation
FROM Continents ct
JOIN Countries c ON c.ContinentCode = ct.ContinentCode
GROUP BY ContinentName
ORDER BY CountriesPopulation DESC

--Problem 13.
CREATE TABLE Monasteries (
  Id INT PRIMARY KEY IDENTITY,
  [Name] NVARCHAR(MAX) NOT NULL,
  CountryCode CHAR(2) NOT NULL FOREIGN KEY REFERENCES Countries(CountryCode)
)

INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'),
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')

ALTER TABLE Countries
ADD IsDeleted BIT NOT NULL DEFAULT (0)

UPDATE Countries
SET IsDeleted = 1
WHERE (
  SELECT COUNT(*)
  FROM Rivers r
  JOIN CountriesRivers cr ON cr.RiverId = r.Id
  WHERE cr.CountryCode = Countries.CountryCode) > 3

SELECT mon.Name AS Monastery,
       c.CountryName AS Country
FROM Monasteries mon
JOIN Countries c ON mon.CountryCode = c.CountryCode
WHERE c.IsDeleted = 0
ORDER BY Name

--Problem 14.
UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

INSERT INTO Monasteries (Name, CountryCode)
SELECT 'Hanga Abbey', c.CountryCode FROM Countries c
WHERE c.CountryName = 'Tanzania'

INSERT INTO Monasteries (Name, CountryCode)
SELECT 'Myin-Tin-Daik', c.CountryCode FROM Countries c
WHERE c.CountryName = 'Myanmar'


SELECT ct.ContinentName,
       c.CountryName,
       COUNT(mon.Name) AS MonasteriesCount
FROM Continents ct
LEFT JOIN Countries c ON ct.ContinentCode = c.ContinentCode
LEFT JOIN Monasteries mon ON mon.CountryCode = c.CountryCode
WHERE c.IsDeleted = 0
GROUP BY ContinentName, CountryName
ORDER BY MonasteriesCount DESC, CountryName