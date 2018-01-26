-- Список продуктов с категорией и поставщиком
--SELECT *
--FROM [dbo].[Products] AS P
--JOIN [dbo].[Categories] AS C
--  ON C.CategoryID = P.CategoryID
--JOIN [dbo].[Suppliers] AS S
--  ON S.SupplierID = P.SupplierID

-- Список сотрудников с указанием региона, за который они отвечают
--SELECT DISTINCT E.FirstName, E.LastName, R.RegionDescription
--FROM [dbo].[Employees] AS E
--JOIN [dbo].[EmployeeTerritories] AS ET
--  ON E.EmployeeID = ET.EmployeeID
--JOIN [dbo].[Territories] AS T
--  ON T.TerritoryID = ET.TerritoryID
--JOIN [dbo].[Region] AS R
--  ON R.RegionID = T.RegionID

-- Статистики по регионам: количество сотрудников по регионам
--SELECT DISTINCT R.RegionDescription, COUNT(E.FirstName)
--FROM [dbo].[Employees] AS E
--JOIN [dbo].[EmployeeTerritories] AS ET
--  ON E.EmployeeID = ET.EmployeeID
--JOIN [dbo].[Territories] AS T
--  ON T.TerritoryID = ET.TerritoryID
--JOIN [dbo].[Region] AS R
--  ON R.RegionID = T.RegionID
--GROUP BY R.RegionDescription

-- Список «сотрудник – с какими грузоперевозчиками работал» (на основе заказов)
--SELECT CAST(E.EmployeeID AS VARCHAR(3)) + ' ' + E.FirstName, S.CompanyName
--FROM [dbo].[Employees] AS E
--JOIN [dbo].[Orders] AS O
--  ON O.EmployeeID = E.EmployeeID
--JOIN [dbo].[Shippers] AS S
--  ON S.ShipperID = O.ShipVia
--GROUP BY CAST(E.EmployeeID AS VARCHAR(3)) + ' ' + E.FirstName, S.CompanyName

--SELECT * FROM [dbo].[Territories]
----where TerritoryID = 19713
--Order by TerritoryDescription

--SELECT *
--FROM [dbo].[Employees]
--WHERE FirstName LIKE '%Nast%'

--SELECT * FROM [dbo].[Products] WHERE ProductID = 1;
--SELECT
--  P.[ProductID],
--  P.[ProductName],
--  P.[CategoryID],
--  C.[CategoryName],
--  C.[Description]
--FROM [dbo].[Products] AS P
--JOIN [dbo].[Categories] AS C
--  ON C.CategoryID = P.CategoryID;

-- TASK 3_3
SELECT DISTINCT [ProductName]
FROM [dbo].Products
WHERE [ProductName] LIKE '%First%'
  OR [ProductName] LIKE '%Second%'

SELECT *
FROM [dbo].[Suppliers]
WHERE SupplierID IN (1, 100)
  OR CompanyName LIKE '%Task3_3%';

SELECT *
FROM [dbo].[Categories]
WHERE CategoryID IN (2, 100)
  OR CategoryName LIKE '%Task3_3%';

-- TASK 3_4
--SELECT
--  O.[OrderID],
--  O.[ShippedDate],
--  OD.[ProductID]
--FROM [dbo].[Orders] AS O-- WHERE ProductID = 1;
--JOIN [dbo].[Order Details] AS OD
--  ON OD.OrderID = O.OrderID
--WHERE O.ShippedDate IS NULL
--ORDER BY OD.ProductID

--SELECT ProductID
--FROM [dbo].[Products] AS P
--WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM [dbo].[Order Details])
