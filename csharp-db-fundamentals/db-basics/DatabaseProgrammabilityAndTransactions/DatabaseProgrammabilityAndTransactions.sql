--Problem 1.
CREATE PROC usp_GetEmployeesSalaryAbove35000 AS
  SELECT FirstName AS [First Name], LastName AS [Last Name]
  FROM Employees
  WHERE Salary > 35000

--Problem 2.
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber (@number DECIMAL(18,4) = 10000) AS
SELECT FirstName, LastName FROM Employees
WHERE Salary >= @number

--Problem 3.
CREATE PROCEDURE usp_GetTownsStartingWith (@startsWith VARCHAR(50)) AS
  SELECT [Name] AS Towns FROM Towns
WHERE [Name] LIKE @startsWith + '%'

--Problem 4.
CREATE PROCEDURE usp_GetEmployeesFromTown (@townName VARCHAR(50)) AS
  SELECT E.FirstName, E.LastName FROM Employees E
    INNER JOIN Towns T
        ON T.Name = @townName
    INNER JOIN Addresses A
        ON A.TownID = T.TownID AND A.AddressID = E.AddressID

--Problem 5.
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(10) AS
  BEGIN
    DECLARE @salaryLevel VARCHAR(10)
    IF(@salary < 30000)
      BEGIN
        SET @salaryLevel = 'Low'
      end
    ELSE IF(@salary > 50000)
      BEGIN
        SET @salaryLevel = 'High'
      end
    ELSE
      BEGIN
        SET @salaryLevel = 'Average'
      end
    RETURN @salaryLevel
  END

--Problem 6.
CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(10)) AS
  SELECT FirstName AS [First Name], LastName AS [Last Name]
    FROM Employees
    WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel

--Problem 7.
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(10), @word VARCHAR(50))
RETURNS BIT AS
  BEGIN
    DECLARE @wordLength INT = LEN(@word)
    DECLARE @index INT = 1

    WHILE (@index <= @wordLength)
    BEGIN
      DECLARE @currentLetter VARCHAR(3) = '%' +SUBSTRING(@word, @index, 1) + '%'
      IF(@setOfLetters NOT LIKE @currentLetter)
        BEGIN
          RETURN 0
        END

      SET @index += 1
    end

    RETURN 1
  END

--Problem 8.
CREATE PROC usp_DeleteEmployeesFromDepartment (@departmentId INT) AS
ALTER TABLE Employees
DROP CONSTRAINT FK_Employees_Employees

ALTER TABLE EmployeesProjects
DROP CONSTRAINT FK_EmployeesProjects_Employees

ALTER TABLE EmployeesProjects
ADD CONSTRAINT FK_EmployeesProjects_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID) ON DELETE CASCADE

ALTER TABLE Departments
DROP CONSTRAINT FK_Departments_Employees

ALTER TABLE Departments
ALTER COLUMN ManagerID INT NULL

UPDATE Departments
SET ManagerID = NULL
WHERE DepartmentID = @departmentId

UPDATE Employees
SET ManagerID = NULL
WHERE DepartmentID = @departmentId

DELETE FROM Employees
WHERE DepartmentID = @departmentId AND ManagerID IS NULL

DELETE FROM Departments
WHERE DepartmentID = @departmentId

IF OBJECT_ID('[Employees].[FK_Employees_Employees]') IS NULL
    ALTER TABLE [Employees] WITH NOCHECK
        ADD CONSTRAINT [FK_Employees_Employees] FOREIGN KEY ([ManagerID]) REFERENCES [Employees]([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION

IF OBJECT_ID('[Departments].[FK_Departments_Employees]') IS NULL
    ALTER TABLE [Departments] WITH NOCHECK
        ADD CONSTRAINT [FK_Departments_Employees] FOREIGN KEY ([ManagerID]) REFERENCES [Employees]([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION

SELECT COUNT(*) FROM Employees
WHERE DepartmentID = @departmentId

--Problem 9.
CREATE PROCEDURE usp_GetHoldersFullName AS
  SELECT CONCAT(FirstName, ' ', LastName) AS [Full Name]
  FROM AccountHolders

--Problem 10.
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@number DECIMAL(16, 2)) AS
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
FROM Accounts a
JOIN AccountHolders ah
    ON a.AccountHolderId = ah.Id
GROUP BY FirstName, LastName
HAVING SUM(a.Balance) > @number
ORDER BY ah.FirstName, ah.LastName

--Problem 11.
CREATE FUNCTION ufn_CalculateFutureValue
  (@sum DECIMAL(18,4), @yearlyInterest FLOAT, @yearsCount INT)
  RETURNS DECIMAL(18, 4) AS
  BEGIN
    DECLARE @result DECIMAL(18, 4)
    SET @result = @sum * (POWER(1+@yearlyInterest, @yearsCount))

    RETURN @result
  END

--Problem 12.
CREATE PROCEDURE usp_CalculateFutureValueForAccount(@accountId INT, @interest FLOAT) AS
  SELECT TOP 1 ah.Id AS [Account Id],
         ah.FirstName AS [First Name],
         ah.LastName AS [Last Name],
         a.Balance AS [Current Balance],
         dbo.ufn_CalculateFutureValue(a.Balance, @interest, 5) AS [Balance in 5 years]
  FROM AccountHolders ah
      JOIN Accounts a ON ah.Id = a.AccountHolderId
  WHERE ah.Id = @accountId

--Problem 13.
CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(MAX))
RETURNS TABLE AS
    RETURN SELECT SUM(t.Cash) AS SumCash FROM
      (SELECT g.Name, u.Cash, ROW_NUMBER() over (ORDER BY u.Cash DESC) AS RowNumber
        FROM UsersGames u
        JOIN Games g ON u.GameId = g.Id AND g.Name =  @gameName
      ) AS t
    WHERE t.RowNumber % 2 = 1

--Problem 14.
CREATE TABLE Logs (
  LogId INT IDENTITY  PRIMARY KEY ,
  AccountId INT,
  OldSum DECIMAL(15,2),
  NewSum DECIMAL(15,2)
)

CREATE TRIGGER tr_AccountsLog ON Accounts FOR UPDATE AS
  BEGIN
    IF UPDATE (Balance)
    BEGIN
      INSERT INTO Logs
      (AccountId, OldSum, NewSum)
      SELECT i.Id, d.Balance, i.Balance
      FROM inserted i
      JOIN deleted d on i.id = d.Id
    END
  END

--Problem 15.
CREATE TABLE NotificationEmails (
  Id INT IDENTITY PRIMARY KEY,
  Recipient INT,
  Subject VARCHAR(MAX),
  Body VARCHAR(MAX)
)

CREATE TRIGGER tr_Logs_Notification ON Logs FOR INSERT AS
  BEGIN
    INSERT INTO NotificationEmails
    (Recipient, Subject, Body)
    SELECT AccountId,
           'Balance change for account: '+ cast(AccountId AS VARCHAR(20)),
           'On '+CONVERT(VARCHAR(50), GETDATE(), 100)+' your balance was changed from '+cast(OldSum AS varchar(20))+' to '+cast(NewSum as VARCHAR(20))+'.'
           FROM inserted
  END

--Problem 16.
CREATE PROCEDURE usp_DepositMoney (@accountId INT, @moneyAmount DECIMAL(17,4)) AS
  BEGIN
    BEGIN TRANSACTION
      IF(@moneyAmount > 0)
      BEGIN
        UPDATE Accounts
        SET Balance += @moneyAmount
        WHERE Id= @accountId
      END
    COMMIT
  END

--Problem 17.
CREATE PROCEDURE usp_WithdrawMoney (@accountId INT, @moneyAmount DECIMAL(17,4)) AS
  BEGIN
    BEGIN TRANSACTION
      IF(@moneyAmount > 0)
      BEGIN
        UPDATE Accounts
        SET Balance -= @moneyAmount
        WHERE Id = @accountId
      END
    COMMIT
  END

--Problem 18.
CREATE PROCEDURE usp_TransferMoney (@senderId INT, @receiverId INT, @amount INT) AS
  BEGIN
    BEGIN TRANSACTION
    EXEC usp_WithdrawMoney @senderId, @amount
    EXEC usp_DepositMoney @receiverId, @amount

    COMMIT
  END

--Problem 19.
CREATE TRIGGER tr_UserGameItems ON UserGameItems INSTEAD OF INSERT AS
BEGIN
	INSERT INTO UserGameItems
	SELECT i.Id, ug.Id FROM inserted
	JOIN UsersGames AS ug
	ON UserGameId = ug.Id
	JOIN Items AS i
	ON ItemId = i.Id
	WHERE ug.Level >= i.MinLevel
END
GO

UPDATE UsersGames
SET Cash += 50000
FROM UsersGames AS ug
JOIN Users AS u
ON ug.UserId = u.Id
JOIN Games AS g
ON ug.GameId = g.Id
WHERE g.Name = 'Bali' AND u.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
GO

CREATE PROC usp_BuyItems(@Username VARCHAR(100)) AS
BEGIN
	DECLARE @UserId INT = (SELECT Id FROM Users WHERE Username = @Username)
	DECLARE @GameId INT = (SELECT Id FROM Games WHERE Name = 'Bali')
	DECLARE @UserGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
	DECLARE @UserGameLevel INT = (SELECT Level FROM UsersGames WHERE Id = @UserGameId)

	DECLARE @counter INT = 251

	WHILE(@counter <= 539)
	BEGIN
		DECLARE @ItemId INT = @counter
		DECLARE @ItemPrice MONEY = (SELECT Price FROM Items WHERE Id = @ItemId)
		DECLARE @ItemLevel INT = (SELECT MinLevel FROM Items WHERE Id = @ItemId)
		DECLARE @UserGameCash MONEY = (SELECT Cash FROM UsersGames WHERE Id = @UserGameId)

		IF(@UserGameCash >= @ItemPrice AND @UserGameLevel >= @ItemLevel)
		BEGIN
			UPDATE UsersGames
			SET Cash -= @ItemPrice
			WHERE Id = @UserGameId

			INSERT INTO UserGameItems VALUES
			(@ItemId, @UserGameId)
		END

		SET @counter += 1

		IF(@counter = 300)
		BEGIN
			SET @counter = 501
		END
	END
END

EXEC usp_BuyItems 'baleremuda'
EXEC usp_BuyItems 'loosenoise'
EXEC usp_BuyItems 'inguinalself'
EXEC usp_BuyItems 'buildingdeltoid'
EXEC usp_BuyItems 'monoxidecos'
GO

SELECT * FROM Users AS u
JOIN UsersGames AS ug
ON u.Id = ug.UserId
JOIN Games AS g
ON ug.GameId = g.Id
JOIN UserGameItems AS ugi
ON ug.Id = ugi.UserGameId
JOIN Items AS i
ON ugi.ItemId = i.Id
WHERE g.Name = 'Bali'
ORDER BY u.Username, i.Name
GO

--Problem 20.
DECLARE @UserId INT = (SELECT Id FROM Users WHERE Username = 'Stamat')
DECLARE @GameId INT = (SELECT Id FROM Games WHERE Name = 'Safflower')
DECLARE @UserGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
DECLARE @UserGameLevel INT = (SELECT Level FROM UsersGames WHERE Id = @UserGameId)
DECLARE @ItemStartLevel INT = 11
DECLARE @ItemEndLevel INT = 12
DECLARE @AllItemsPrice MONEY = (SELECT SUM(Price) FROM Items WHERE (MinLevel BETWEEN @ItemStartLevel AND @ItemEndLevel))
DECLARE @StamatCash MONEY = (SELECT Cash FROM UsersGames WHERE Id = @UserGameId)

IF(@StamatCash >= @AllItemsPrice)
BEGIN
	BEGIN TRAN
		UPDATE UsersGames
		SET Cash -= @AllItemsPrice
		WHERE Id = @UserGameId

		INSERT INTO UserGameItems
		SELECT i.Id, @UserGameId  FROM Items AS i
		WHERE (i.MinLevel BETWEEN @ItemStartLevel AND @ItemEndLevel)
	COMMIT
END

SET @ItemStartLevel = 19
SET @ItemEndLevel = 21
SET @AllItemsPrice = (SELECT SUM(Price) FROM Items WHERE (MinLevel BETWEEN @ItemStartLevel AND @ItemEndLevel))
SET @StamatCash = (SELECT Cash FROM UsersGames WHERE Id = @UserGameId)

IF(@StamatCash >= @AllItemsPrice)
BEGIN
	BEGIN TRAN
		UPDATE UsersGames
		SET Cash -= @AllItemsPrice
		WHERE Id = @UserGameId

		INSERT INTO UserGameItems
		SELECT i.Id, @UserGameId  FROM Items AS i
		WHERE (i.MinLevel BETWEEN @ItemStartLevel AND @ItemEndLevel)
	COMMIT
END

SELECT i.Name AS [Item Name] FROM Users AS u
JOIN UsersGames AS ug
ON u.Id = ug.UserId
JOIN Games AS g
ON ug.GameId = g.Id
JOIN UserGameItems AS ugi
ON ug.Id = ugi.UserGameId
JOIN Items AS i
ON ugi.ItemId = i.Id
WHERE u.Username = 'Stamat' AND g.Name = 'Safflower'
ORDER BY i.Name


--Problem 21.

CREATE PROCEDURE usp_AssignProject(@employeeId INT, @projectId INT) AS
  BEGIN
    BEGIN TRANSACTION

      INSERT INTO EmployeesProjects VALUES
      (@employeeId, @projectId)

      DECLARE @projectsCount INT
      SELECT @projectsCount = COUNT(*) FROM EmployeesProjects
      WHERE EmployeeId = @employeeId

      IF(@projectsCount > 3)
      BEGIN
        ROLLBACK
        RAISERROR ('The employee has too many projects!',16, 1)
        RETURN
      END
    COMMIT
  END

--Problem 22.
CREATE TABLE Deleted_Employees (
  EmployeeId INT PRIMARY KEY IDENTITY ,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  MiddleName VARCHAR(50) NOT NULL,
  JobTitle VARCHAR(50) NOT NULL,
  DepartmentId INT NOT NULL,
  Salary DECIMAL(17,4)
)
GO

CREATE TRIGGER tr_FiredEmployees ON Employees AFTER DELETE AS
    INSERT INTO Deleted_Employees
    SELECT FirstName,
           LastName,
           MiddleName,
           JobTitle,
           DepartmentID,
           Salary FROM deleted


