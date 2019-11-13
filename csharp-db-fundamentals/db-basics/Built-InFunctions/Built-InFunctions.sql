USE SoftUni
--Problem 1.
SELECT FirstName, LastName FROM Employees
WHERE FirstName LIKE 'SA%'

--Problem 2.
SELECT FirstName, LastName FROM Employees
WHERE LastName LIKE '%ei%'

--Problem 3.
SELECT FirstName FROM  Employees
WHERE DepartmentID IN (3, 10) AND
  HireDate BETWEEN '1994' and '2006'

--Problem 4.
SELECT FirstName, LastName FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'

--Problem 5.
SELECT [Name] FROM Towns
WHERE LEN([Name]) IN (5,6)
ORDER BY [Name]

--Problem 6.
SELECT TownID, [Name] FROM Towns
WHERE Name LIKE '[MKBE]%'
ORDER BY [Name]

--Problem 7.
SELECT TownID, [Name] FROM Towns
WHERE Name NOT LIKE '[RBD]%'
ORDER BY [Name]

--Problem 8.
CREATE VIEW V_EmployeesHiredAfter2000 AS
  SELECT FirstName, LastName FROM Employees
  WHERE HireDate >= '2001'

--Problem 9.
SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5
GO

--Problem 10.
SELECT EmployeeID, FirstName, LastName, Salary,
       DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS Rank
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC

--Problem 11.
  SELECT * FROM (
SELECT EmployeeID, FirstName, LastName, Salary,
       DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS Rank
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000) a
WHERE a.Rank = 2
ORDER BY a.Salary DESC


USE Geography
--Problem 12.
SELECT CountryName AS [Country Name], IsoCode AS [ISO Code] FROM Countries
WHERE CountryName LIKE '%A%A%A%'
ORDER BY [ISO Code]

--Problem 13.
SELECT PeakName, RiverName, LOWER(PeakName + SUBSTRING(RiverName, 2, LEN(RiverName) -1)) AS  Mix
FROM Peaks
JOIN Rivers
ON RIGHT(PeakName, 1) = LEFT(RiverName, 1)
ORDER BY Mix

USE Diablo
--Problem 14.
SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS Start FROM Games
WHERE Start BETWEEN '2011' AND '2013'
ORDER BY Start, Name

--Problem 15.
SELECT Username, SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email) - CHARINDEX('@', Email)) AS [Email Provider] FROM Users
ORDER BY [Email Provider], Username

--Problem 16.
SELECT Username, IpAddress AS [IP Address] FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username

--Problem 17.
SELECT [Name] AS Game,
       [Part of the Day] =
        CASE
          WHEN DATEPART(HOUR , Start) < 12 THEN 'Morning'
          WHEN DATEPART(HOUR , Start) < 18 THEN 'Afternoon'
          ELSE 'Evening'
        END,
       Duration =
        CASE
          WHEN Duration <= 3 THEN 'Extra Short'
          WHEN Duration <= 6 THEN 'Short'
          WHEN Duration > 6 THEN 'Long'
          ELSE 'Extra Long'
        END
FROM Games
ORDER BY [Name], Duration

USE Orders
--Problem 18.
SELECT ProductName, OrderDate,
       (DATEADD(DAY, 3, OrderDate)) AS [Pay Due],
       (DATEADD(MONTH , 1, OrderDate)) AS [Deliver Due]
FROM Orders

