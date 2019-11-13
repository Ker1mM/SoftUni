CREATE DATABASE Supermarket
USE Supermarket

--Problem 1.
CREATE TABLE Categories
(
  Id     INT PRIMARY KEY IDENTITY,
  [Name] NVARCHAR(30) NOT NULL,
)

CREATE TABLE Items
(
  Id         INT PRIMARY KEY IDENTITY,
  [Name]     NVARCHAR(30)   NOT NULL,
  Price      DECIMAL(15, 2) NOT NULL,
  CategoryId INT            NOT NULL FOREIGN KEY REFERENCES Categories (Id)
)

CREATE TABLE Employees
(
  Id        INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(50)   NOT NULL,
  LastName  NVARCHAR(50)   NOT NULL,
  Phone     CHAR(12)       NOT NULL,
  Salary    DECIMAL(15, 2) NOT NULL
)

CREATE TABLE Orders
(
  Id         INT PRIMARY KEY IDENTITY,
  DateTime   DATETIME NOT NULL,
  EmployeeId INT      NOT NULL FOREIGN KEY REFERENCES Employees (Id)
)

CREATE TABLE OrderItems
(
  OrderId  INT NOT NULL FOREIGN KEY REFERENCES Orders (Id),
  ItemId   INT NOT NULL FOREIGN KEY REFERENCES Items (Id),
  Quantity INT NOT NULL,
  CONSTRAINT PK_OrderItems PRIMARY KEY (OrderId, ItemId),
  CONSTRAINT ck_CheckQuantityMin CHECK (Quantity >= 1)
)

CREATE TABLE Shifts
(
  Id         INT IDENTITY,
  EmployeeId INT FOREIGN KEY REFERENCES Employees (Id),
  CheckIn    DATETIME NOT NULL,
  CheckOut   DATETIME NOT NULL,
  CONSTRAINT PK_Shifts PRIMARY KEY (Id, EmployeeId),
  CONSTRAINT ck_CheckOutAfterCheckIn CHECK (CheckIn < CheckOut)
)

--Problem 18.
CREATE FUNCTION udf_GetPromotedProducts(@CurrentDate DATETIME, @StartDate DATETIME, @EndDate DATETIME,
                                        @Discount DECIMAL(15, 2), @FirstItemId INT, @SecondItemId INT, @ThirdItemId INT)
  RETURNS NVARCHAR(MAX) AS
BEGIN
  DECLARE @Result NVARCHAR(MAX)
  DECLARE @ItemsCount INT = (SELECT COUNT(i.Id)
                             FROM Items i
                             WHERE i.Id IN (@FirstItemId, @SecondItemId, @ThirdItemId))

  IF (@ItemsCount < 3)
    BEGIN
      SET @Result = 'One of the items does not exists!'
    END

  IF (@CurrentDate NOT BETWEEN @StartDate AND @EndDate AND @Result IS NULL)
    BEGIN
      SET @Result = 'The current date is not within the promotion dates!'
    END


  IF (@Result IS NULL)
    BEGIN

      DECLARE @FirstItemName NVARCHAR(50)
      DECLARE @SecondItemName NVARCHAR(50)
      DECLARE @ThirdItemName NVARCHAR(50)
      DECLARE @FirstItemPrice DECIMAL(15, 2)
      DECLARE @SecondItemPrice DECIMAL(15, 2)
      DECLARE @ThirdItemPrice DECIMAL(15, 2)

      SELECT @FirstItemName = i.Name,
             @FirstItemPrice = i.Price * (1 - (@Discount / 100))
      FROM Items i
      WHERE i.Id = @FirstItemId

      SELECT @SecondItemName = i.Name,
             @SecondItemPrice = i.Price * (1 - (@Discount / 100))
      FROM Items i
      WHERE i.Id = @SecondItemId

      SELECT @ThirdItemName = i.Name,
             @ThirdItemPrice = i.Price * (1 - (@Discount / 100))
      FROM Items i
      WHERE i.Id = @ThirdItemId

      SET @Result = CONCAT(@FirstItemName,
                           ' price: ',
                           @FirstItemPrice,
                           ' <-> ',
                           @SecondItemName,
                           ' price: ',
                           @SecondItemPrice,
                           ' <-> ',
                           @ThirdItemName,
                           ' price: ',
                           @ThirdItemPrice)
    END
  RETURN @Result
END
GO

--Problem 19.
CREATE PROCEDURE usp_CancelOrder(@OrderId INT, @CancelDate DATETIME)
AS
BEGIN
  BEGIN TRANSACTION
    DECLARE @OrderDate DATETIME = (SELECT DateTime FROM Orders WHERE Id = @OrderId)

    DELETE
    FROM OrderItems
    WHERE OrderId = @OrderId

    DELETE
    FROM Orders
    WHERE Id = @OrderId

    IF (@@ROWCOUNT = 0)
      BEGIN
        ROLLBACK
        RAISERROR ('The order does not exist!', 16, 1)
        RETURN
      end

    IF (DATEADD(DAY, 3, @OrderDate) < @CancelDate)
      BEGIN
        ROLLBACK
        RAISERROR ('You cannot cancel the order!', 16, 1)
        RETURN
      end

  COMMIT TRANSACTION
END
GO

--Problem 20.
CREATE TABLE DeletedOrders(
  OrderId INT PRIMARY KEY,
  ItemId INT NOT NULL,
  ItemQuantity INT NOT NULL
)

CREATE TRIGGER tr_DeletedOrders ON OrderItems AFTER DELETE AS
  BEGIN
    INSERT  INTO DeletedOrders (OrderId, ItemId, ItemQuantity)
    SELECT oi.OrderId, oi.ItemId, oi.Quantity
    FROM deleted oi
  end


