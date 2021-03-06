--SELECT TOP (1000) [OrderID]
--      ,[CustomerID]
--      ,[EmployeeID]
--      ,[OrderDate]
--      ,[RequiredDate]
--      ,[ShippedDate]
--      ,[ShipVia]
--      ,[Freight]
--      ,[ShipName]
--      ,[ShipAddress]
--      ,[ShipCity]
--      ,[ShipRegion]
--      ,[ShipPostalCode]
--      ,[ShipCountry]
--  FROM [Northwind].[dbo].[Orders]

USE [Northwind]
-- TASK 1.1
  SELECT OrderID, ShippedDate, ShipVia
  FROM [Northwind].[dbo].[Orders]
  WHERE ShippedDate >= '1998-05-06 00:00:00.000'
  AND ShipVia >= 2;

  --SELECT
  --  OrderID,
  --  CASE
  --  WHEN ShippedDate IS NULL THEN 'Not Shipped'
  --  END ShippedDate
  --FROM [Northwind].[dbo].[Orders]
  --WHERE ShippedDate IS NULL;

  --SELECT
  --  OrderID AS 'Order Number',
  --  ISNULL(CONVERT(VARCHAR(30), ShippedDate, 120), 'Not Shipped') AS 'Shipped Date'
  --FROM [Northwind].[dbo].[Orders]
  --WHERE CONVERT(DATE, ShippedDate) > '1996-09-04'
  --  OR ShippedDate IS NULL;

-- TASK 1.2
SELECT
  ContactName
  ,Country
FROM dbo.Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country;

--SELECT
--  ContactName
--  ,Country
--FROM dbo.Customers
--WHERE Country IN (SELECT Country FROM dbo.Customers WHERE Country != 'USA' OR Country != 'Canada')
--ORDER BY ContactName;

--BEGIN TRANSACTION
--  INSERT INTO dbo.Customers (CustomerID, CompanyName, ContactName, Country) VALUES ('ROME1', 'Romero y tomillo', 'Alejandra Camino', 'Germany');
--ROLLBACK TRANSACTION

--SELECT DISTINCT Country
--FROM dbo.Customers
--ORDER BY Country; --DESC;

---- TASK 1.3
--SELECT DISTINCT
--  [OrderID]
--  --,[Quantity]
--FROM [dbo].[Order Details]
--WHERE Quantity BETWEEN 3 AND 10

--SELECT DISTINCT CustomerID, Country--, ASCII(Country), ASCII('B'), ASCII('G')
--FROM dbo.Customers
--WHERE ASCII(Country) BETWEEN ASCII('B') AND ASCII('G')
--ORDER BY Country;

--SELECT DISTINCT CustomerID, CompanyName, Country--, ASCII(Country), ASCII('B'), ASCII('G')
--FROM dbo.Customers
--WHERE ASCII(Country) >= ASCII('B')
--  AND ASCII(Country) <= ASCII('G')
--ORDER BY Country;

---- TASK 1.4
--SELECT ProductName
--FROM dbo.Products
--WHERE ProductName LIKE '%cho_olade%';

---- TASK 2.1
----SELECT CAST(SUM((UnitPrice - UnitPrice * Discount) * Quantity) AS DECIMAL(10, 2)) AS 'Totals',
----FROM dbo.[Order Details];

--SELECT
--  COUNT(*) - COUNT(ShippedDate) AS 'NON Shipped Orders',
--  COUNT(*) AS 'Total Orders',
--  COUNT(ShippedDate) AS 'Shipped Orders'
--FROM dbo.Orders;

--SELECT COUNT(DISTINCT CustomerID) AS 'Customers COUNT'
--FROM dbo.Orders;

---- TASK 2.2
--SELECT
--  YEAR (ShippedDate) AS 'Year',
--  COUNT(OrderID) AS 'Total'
--FROM dbo.Orders
--GROUP BY YEAR(ShippedDate);

SELECT
  EmployeeID AS 'Seller ID',
  (SELECT LastName + ' ' + FirstName FROM dbo.Employees WHERE EmployeeID = O.EmployeeID) AS 'Seller',
  COUNT([OrderID]) AS 'Amount'
FROM dbo.Orders AS O
GROUP BY EmployeeID
ORDER BY COUNT(OrderID) DESC;

SELECT
  EmployeeID AS 'Seller ID',
  (SELECT LastName + ' ' + FirstName FROM dbo.Employees WHERE EmployeeID = O.EmployeeID) AS 'Seller',
  COUNT([OrderID]) AS 'Orders Amount',
  (SELECT CustomerID + ' ' + CompanyName FROM dbo.Customers WHERE CustomerID = O.CustomerID) AS 'Customer'
FROM dbo.Orders AS O
GROUP BY EmployeeID, CustomerID
ORDER BY EmployeeID, COUNT(OrderID) DESC;



-- TASK 2.3

SELECT DISTINCT E.[EmployeeID], E.[FirstName], E.[LastName]
from [dbo].[Employees] AS E
JOIN [dbo].[EmployeeTerritories] AS ET
  ON ET.[EmployeeID] = E.[EmployeeID]
JOIN [dbo].[Territories] AS T
  ON T.TerritoryID = ET.TerritoryID
JOIN [dbo].[Region] AS R
  ON R.[RegionID] = T.[RegionID]
WHERE R.[RegionDescription] = 'Western';

--SELECT DISTINCT
--  C.[CustomerID]
--  ,C.[CompanyName]
--  ,C.[ContactName]
--  ,COUNT(O.[OrderID]) AS 'Orders Count'
--  FROM [Northwind].[dbo].[Customers] AS C
--JOIN [dbo].[Orders] AS O
--  ON O.[CustomerID] = C.[CustomerID]
--GROUP BY C.[CustomerID],C.[CompanyName],C.[ContactName]

-- TASK 2.4
SELECT S.[SupplierID]
      ,S.[CompanyName]
  FROM [Northwind].[dbo].[Suppliers] AS S
  where S.[SupplierID] IN (select [SupplierID] from [dbo].[Products] where [UnitsInStock] = 0 )





-- Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.
SELECT
  [EmployeeID]
  ,[LastName]
  ,[FirstName]
FROM [Northwind].[dbo].[Employees]
WHERE [EmployeeID] IN (SELECT [EmployeeID] FROM [dbo].[Orders] GROUP BY [EmployeeID] HAVING COUNT([OrderID]) > 150)