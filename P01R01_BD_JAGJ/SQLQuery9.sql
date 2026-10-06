USE Northwind
GO

SELECT EmployeeID, YEAR(OrderDate) AS OrderYear
FROM Orders
WHERE CustomerID = '5'
GROUP BY EmployeeID, YEAR(OrderDate)
HAVING COUNT(*)>1
ORDER BY EmployeeID, OrderYear;


SELECT DISTINCT c.Country
FROM Customers AS c


SELECT DISTINCT 
e.City, e.Country
FROM Customers AS c
JOIN Employees AS e
ON
c.City = e.City
AND c.Country = e.Country

-------------------------------------------------
SELECT
o.OrderID, o.CustomerID, o.OrderDate
FROM Orders AS o
ORDER BY OrderDate;

SELECT
o.OrderID, o.CustomerID, YEAR(OrderDate) AS
OrderYear
FROM Orders AS o
ORDER BY OrderYear;

SELECT *
FROM Customers
ORDER BY 9 ;
GO
---------------------------------------------------------
SELECT
c.ContactName, c.Country
FROM Customers AS c
WHERE Country = N'Spain'


SELECT
o.OrderID, o.OrderDate
FROM Orders AS o
WHERE OrderDate > '19951010';


SELECT
o.OrderID, o.OrderDate, o.CustomerID
FROM Orders AS o
WHERE OrderDate > '19951010' AND OrderDate < '19971010';

SELECT TOP 10 OrderDate FROM Orders;


SELECT 
c.CustomerID, c.City, c.Region, c.Country
FROM Customers AS c
WHERE Region IS NOT NULL

SELECT 
c.CustomerID, c.City, c.Region, c.Country
FROM Customers AS c
WHERE Region IS NULL

SELECT 
c.CustomerID, c.City, c.Region, c.Country
FROM Customers AS c
WHERE Region = NULL


