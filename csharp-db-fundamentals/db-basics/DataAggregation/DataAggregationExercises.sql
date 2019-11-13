USE Gringotts

--Problem 1.
SELECT COUNT(e.Id) AS Count
FROM WizzardDeposits AS e

--Problem 2.
SELECT MAX(e.MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits AS e

--Problem 3.
SELECT e.DepositGroup, MAX(e.MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits AS e
GROUP BY e.DepositGroup

--Problem 4.
SELECT TOP (2) e.DepositGroup
FROM WizzardDeposits AS e
GROUP BY e.DepositGroup
ORDER BY AVG(e.MagicWandSize) ASC

--Problem 5.
SELECT e.DepositGroup, SUM(e.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS e
GROUP BY e.DepositGroup

--Problem 6.
SELECT e.DepositGroup, SUM(e.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS e
WHERE e.MagicWandCreator = 'Ollivander family'
GROUP BY e.DepositGroup

--Problem 7.
SELECT e.DepositGroup, SUM(e.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS e
WHERE e.MagicWandCreator = 'Ollivander family'
GROUP BY e.DepositGroup
HAVING SUM(e.DepositAmount) < 150000
ORDER BY TotalSum DESC

--Problem 8.
SELECT e.DepositGroup, e.MagicWandCreator,
       MIN(e.DepositCharge) AS [MinDepositCharge]
FROM WizzardDeposits AS e
GROUP BY e.DepositGroup, e.MagicWandCreator
ORDER BY e.MagicWandCreator ASC , e.DepositGroup ASC

--Problem 9.
SELECT *, COUNT(*) AS [WizardCount] FROM (
SELECT
       CASE
         WHEN e.Age BETWEEN 0 AND 10 THEN '[0-10]'
         WHEN e.Age BETWEEN 11 AND 20 THEN '[11-20]'
         WHEN e.Age BETWEEN 21 AND 30 THEN '[21-30]'
         WHEN e.Age BETWEEN 31 AND 40 THEN '[31-40]'
         WHEN e.Age BETWEEN 41 AND 50 THEN '[41-50]'
         WHEN e.Age BETWEEN 51 AND 60 THEN '[51-60]'
         ELSE '[61+]'
       END AS [AgeGroup]
FROM WizzardDeposits AS e ) AS Temp
GROUP BY AgeGroup
ORDER BY AgeGroup

--Problem 10.
SELECT DISTINCT LEFT(e.FirstName, 1) AS [FirstLetter]
FROM WizzardDeposits AS e
WHERE e.DepositGroup = 'Troll Chest'
GROUP BY LEFT(e.FirstName, 1)

--Problem 11.
SELECT e.DepositGroup, e.IsDepositExpired, AVG(e.DepositInterest) AS [AverageInterest]
FROM WizzardDeposits AS e
WHERE e.DepositStartDate > '1985-01-01'
GROUP BY e.DepositGroup, e.IsDepositExpired
ORDER BY e.DepositGroup DESC, e.IsDepositExpired

--Problem 12.
SELECT SUM(Difference) AS [SumDifference] FROM (
  SELECT e.FirstName AS [Host Wizard],
       e.DepositAmount AS [Host Wizard Deposit],
       r.FirstName AS [Guest Wizard],
       r.DepositAmount AS [Guest Wizard Deposit],
       (e.DepositAmount - r.DepositAmount) AS [Difference]
  FROM WizzardDeposits AS e
  INNER JOIN WizzardDeposits AS r
  ON e.Id + 1 = r.Id
) AS t

USE SoftUni
--Problem 13.
SELECT e.DepartmentID, SUM(e.Salary) AS [TotalSalary]
FROM Employees AS e
GROUP BY e.DepartmentID
ORDER BY e.DepartmentID

--Problem 14.
SELECT t.DepartmentID, MIN(t.Salary)
FROM Employees AS t
WHERE t.HireDate > '2000-01-01' AND t.DepartmentID IN (2, 5, 7)
GROUP BY t.DepartmentID

--Problem 15.
SELECT *
INTO AverageSalary
FROM Employees
WHERE Salary > 30000

DELETE FROM AverageSalary
WHERE ManagerID = 42

UPDATE AverageSalary
SET Salary = Salary + 5000
WHERE DepartmentID = 1

SELECT r.DepartmentID, AVG(Salary) AS [AverageSalary]
FROM AverageSalary AS r
GROUP BY r.DepartmentID

--Problem 16.
SELECT r.DepartmentID, MAX(r.Salary)
FROM Employees AS r
GROUP BY r.DepartmentID
HAVING NOT MAX(r.Salary) BETWEEN 30000 AND 70000

--Problem 17.
SELECT COUNT(r.Salary) AS [Count]
FROM Employees AS r
WHERE ManagerID IS NULL

--Problem 18.
SELECT a.DepartmentID,
       ( SELECT DISTINCT b.Salary FROM Employees AS b
          WHERE b.DepartmentID = a.DepartmentID
          ORDER BY Salary DESC
          OFFSET 2 ROWS
          FETCH NEXT 1 ROWS ONLY ) AS [ThirdHighestSalary]
FROM Employees AS a
WHERE (
	SELECT DISTINCT b.Salary FROM Employees AS b
	WHERE b.DepartmentID = a.DepartmentId
	ORDER BY Salary DESC
	OFFSET 2 ROWS
	FETCH NEXT 1 ROWS ONLY
) IS NOT NULL
GROUP BY a.DepartmentID

--Problem 19.
SELECT TOP (10) a.FirstName, a.LastName, a.DepartmentID
FROM Employees AS a
WHERE a.Salary >
      ( SELECT AVG(a2.Salary)
        FROM Employees AS a2
        WHERE a.DepartmentID = a2.DepartmentID)
ORDER BY DepartmentID


