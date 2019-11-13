--Problem 1.
SELECT TOP 5 E.EmployeeID, E.JobTitle, E.AddressID , A.AddressText
FROM Employees AS E
INNER JOIN Addresses AS A
ON E.AddressID = A.AddressID
ORDER BY A.AddressID

--Problem 2.
SELECT TOP 50 E.FirstName, E.LastName, T.Name, A.AddressText
FROM Employees E
INNER JOIN Addresses A
  ON E.AddressID = A.AddressID
INNER JOIN Towns T
  ON A.TownID = T.TownID
ORDER BY FirstName ASC, LastName

--Problem 3.
SELECT E.EmployeeID, E.FirstName, E.LastName, D.Name
FROM Employees E
INNER JOIN Departments D
     ON E.DepartmentID = D.DepartmentID
WHERE Name = 'Sales'
ORDER BY EmployeeID ASC

--Problem 4.
SELECT TOP 5 E.EmployeeID, E.FirstName, E.Salary, D.Name
FROM Employees E
INNER JOIN Departments D
  ON E.DepartmentID = D.DepartmentID
WHERE Salary > 15000
ORDER BY D.DepartmentID ASC

--Problem 5.
SELECT DISTINCT TOP 3 E.EmployeeID, E.FirstName
FROM Employees E
LEFT JOIN EmployeesProjects EP
  ON E.EmployeeID = EP.EmployeeID
WHERE EP.EmployeeID Is NULL

--Problem 6.
SELECT E.FirstName, E.LastName, E.HireDate, D.Name AS DeptName
FROM Employees E
INNER JOIN Departments D
  ON E.DepartmentID = D.DepartmentID
WHERE D.Name IN ('Sales', 'Finance') AND HireDate > '1999-01-01'
ORDER BY HireDate ASC

--Problem 7.
SELECT TOP 5 E.EmployeeID, E.FirstName, P.Name AS ProjectName
FROM Employees E
JOIN EmployeesProjects EP
  ON EP.EmployeeID = e.EmployeeID
JOIN Projects P
  ON P.ProjectID = EP.ProjectID
WHERE P.StartDate > '2002-08-13' AND P.EndDate IS NULL
ORDER BY E.EmployeeID ASC

--Problem 8.
SELECT E.EmployeeID, E.FirstName,
       CASE
          WHEN DATEPART(YEAR , P.StartDate) >= 2005 THEN NULL
          ELSE P.Name
       END AS ProjectName
FROM Employees E
INNER JOIN EmployeesProjects EP
 ON EP.EmployeeID = E.EmployeeID
INNER JOIN Projects P
  ON P.ProjectID = EP.ProjectID
WHERE E.EmployeeID = 24

--Problem 9.
SELECT E.EmployeeID, E.FirstName, E.ManagerID, E2.FirstName AS ManagerName
FROM Employees E
JOIN Employees E2
  ON E2.EmployeeID = E.ManagerID AND E.ManagerID IN(3, 7)
ORDER BY E.EmployeeID ASC

--Problem 10.
SELECT DISTINCT TOP 50 E.EmployeeID,
       (E.FirstName + ' ' + E.LastName) AS EmployeeName,
       (E2.FirstName+ ' ' +E2.LastName) AS ManagerName,
       D.Name AS DepartmentName
FROM Employees E
JOIN Employees E2
  ON E2.EmployeeID = E.ManagerID
JOIN Departments D
  ON E.DepartmentID = D.DepartmentID
ORDER BY E.EmployeeID

--Problem 11.
  SELECT MIN(AvergaeSalaries) AS MinAverageSalary FROM (
SELECT AVG(Salary) AS AvergaeSalaries FROM Employees
GROUP BY DepartmentID
) AS AV

--Problem 12.
SELECT C.CountryCode, M.MountainRange, P.PeakName, P.Elevation
FROM Countries C
      INNER JOIN MountainsCountries MC
          ON MC.CountryCode = 'BG' AND MC.CountryCode = C.CountryCode
      INNER JOIN Mountains M
          ON M.ID = MC.MountainId
      INNER JOIN Peaks P
          ON P.MountainId = M.Id AND P.Elevation > 2835
ORDER BY P.Elevation DESC

--Problem 13.
SELECT MC.CountryCode, COUNT(M.MountainRange) AS MountainRanges
FROM MountainsCountries MC
      INNER JOIN Mountains M
          ON MC.CountryCode IN ('BG','US', 'RU')
               AND MC.MountainId = M.Id
GROUP BY MC.CountryCode

--Problem 14.
SELECT DISTINCT TOP 5 C.CountryName, R.RiverName
FROM Countries C
        INNER JOIN Continents CO
          ON CO.ContinentName = 'Africa'
               AND CO.ContinentCode = C.ContinentCode
        LEFT JOIN CountriesRivers CR
          ON CR.CountryCode = C.CountryCode
        LEFT JOIN Rivers R
          ON R.Id = CR.RiverId
ORDER BY C.CountryName ASC

--Problem 15.
WITH CTE_ContinentMax (ContinentCode, CurrencyUsage) AS
(
	SELECT ContinentCode, MAX(c) AS CurrencyUsage FROM
	(SELECT ContinentCode, CurrencyCode, COUNT(CurrencyCode) AS c FROM Countries
	GROUP BY ContinentCode, CurrencyCode) AS k
	GROUP BY ContinentCode
),

CTE_AllCurrCont (ContinentCode, CurrencyCode, CurrencyUsage) AS
(
	SELECT * FROM
	(SELECT ContinentCode, CurrencyCode, MAX(c) AS CurrencyUsage FROM
		(SELECT ContinentCode, CurrencyCode, COUNT(CurrencyCode) AS c FROM Countries
		GROUP BY ContinentCode, CurrencyCode) AS t
	GROUP BY ContinentCode, CurrencyCode) AS m
)

SELECT acc.ContinentCode, acc.CurrencyCode, acc.CurrencyUsage FROM CTE_AllCurrCont AS acc
JOIN CTE_ContinentMax AS cm
ON cm.ContinentCode = acc.ContinentCode AND cm.CurrencyUsage = acc.CurrencyUsage
WHERE acc.CurrencyUsage > 1
ORDER BY acc.ContinentCode

--Problem 16.
SELECT COUNT(1)-COUNT(K.MountainId) AS CountryCount FROM
  (SELECT  M.MountainId
FROM Countries C
  LEFT JOIN MountainsCountries M
      ON M.CountryCode = C.CountryCode) AS K

--Problem 17.
WITH CTE_CountryHighestPeak AS
(
	SELECT c.CountryName, MAX(p.Elevation) AS HighestPeakElevation FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc
	ON mc.CountryCode = c.CountryCode
	LEFT JOIN Peaks AS p
	ON p.MountainId = mc.MountainId
	GROUP BY c.CountryName
),

CTE_CountryLongestRiver AS
(
	SELECT c.CountryName, MAX(r.Length) AS LongestRiverLength FROM Countries AS c
	LEFT JOIN CountriesRivers AS cr
	ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r
	ON r.Id = cr.RiverId
	GROUP BY c.CountryName
)

SELECT TOP(5) hp.CountryName, hp.HighestPeakElevation, lr.LongestRiverLength FROM CTE_CountryHighestPeak AS hp
JOIN CTE_CountryLongestRiver AS lr
ON lr.CountryName = hp.CountryName
ORDER BY hp.HighestPeakElevation DESC, lr.LongestRiverLength DESC

--Problem 18.
WITH CTE_CountriesHighestElevation AS
(
	SELECT c.CountryName,
		CASE
			WHEN MAX(p.Elevation) IS NULL THEN 0
			ELSE MAX(p.Elevation)
		END AS [Highest Peak Elevation] FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc
	ON mc.CountryCode = c.CountryCode
	LEFT JOIN Peaks As p
	ON p.MountainId = mc.MountainId
	GROUP BY c.CountryName
),

CTE_MountainHighestElevation AS
(
	SELECT m.Id, MAX(p.Elevation) AS mhe FROM Mountains AS m
	JOIN Peaks AS p
	ON p.MountainId = m.Id
	GROUP BY m.Id
)

SELECT TOP 5
	he.CountryName AS Country,
	CASE
		WHEN p.PeakName IS NULL THEN '(no highest peak)'
		ELSE p.PeakName
	END AS [Highest Peak Name],

	he.[Highest Peak Elevation],
	CASE
		WHEN m.MountainRange IS NULL THEN '(no mountain)'
		ELSE m.MountainRange
	END AS Mountain

FROM CTE_CountriesHighestElevation As he
JOIN Countries AS c
ON c.CountryName = he.CountryName
LEFT JOIN MountainsCountries AS mc
ON mc.CountryCode = c.CountryCode
LEFT JOIN CTE_MountainHighestElevation AS mh
ON mh.Id = mc.MountainId AND mh.mhe = [Highest Peak Elevation]
LEFT JOIN Peaks AS p
ON p.Elevation = mh.mhe
LEFT JOIN Mountains AS m
ON mc.MountainId = m.Id
WHERE he.[Highest Peak Elevation] = p.Elevation OR he.[Highest Peak Elevation] = 0
ORDER BY he.CountryName