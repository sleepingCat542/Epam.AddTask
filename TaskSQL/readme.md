����������� ���� ������ � SQL
������������ �������
������������ �������
������� 1
�������� ������, ������� ������� ������ ���������� � ���� �������, ��������� �� ���� �������� CustomerID � CompanyName. ������ ������� ������ ���� ������������� �� ���� ���������.
������� 2
�������� ������, ������� ������� EmployeeID ���������� �������� ��������� ����������.
������� 3
�������� ������, ������� ������� ������ ���� ����� �� ������� dbo.Customers.Country. ������ ������ ���� ������������ � ���������� �������, ������ ��������� ������ ���������� �������� � �� ������ ��������� ���������.
������� 4
�������� ������, ������� ������� ������ �������� ��������-����������, ������������� � �������� �������: ������, ������, ������, ��������, �����. ������ ������ ���� ������������ �� ����-�������������� �������� � �������� ���������� �������.
������� 5
�������� ������, ������� ������� ������ ��������������� ��������, ��� ������� ������ ���� ���������� (dbo.Orders.RequiredDate) � �������� 1996 ����. ������ ������ ���� ������������ � ���������� �������.
������� 6
�������� ������, ������� ������� ��� ����������� ���� ��������-���������, ��� ����� �������� ���������� � ���� "171" � �������� "77", � ����� ����� ����� ���������� � ���� "171" � ������������ �� "50".
������� 7
�������� ������, ������� ������� ���������� ��������-����������, ������� ��������� � �������, ������������� ���� ������������� �������. �������������� ������� ������ �������� �� ���� ������� City � CustomerCount.
������� 8
�������� ������, ������� ������� ���������� ��������-���������� � �������, � ������� ���� 10 � ����� ����������. �������������� ������� ������ ����� ������� Country � CustomerCount, ������ ������� ������ ���� ������������� � �������� ������� �� ���������� ���������� � ������.
������� 9
�������� ������, ������� ������� ������� ��������� ������ (dbo.Order.Freight) ������� ��� ��������-����������, ������� ��������� ������ �������� ������ �����, ������������� �������������� ��� ������. �������������� ��������� ������� �������� �������� ������� ��������� ������ ������ - ������ ��� ����� 100, ��� ������ 10. �������������� ������� ������ ����� ������� CustomerID � FreightAvg, �������� ������� ��������� ������ ���� ��������� �� ������ ��������, ������ ������ ���� ������������� � �������� ������� �� �������� �������� �������� ������.
������� 10
�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� ��������� ��� ���������� ���������� �������� ����������.
������� 11
�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� �������� ����� OFFSET � FETCH.
������� 12
�������� ������, ������� ������� ����� ����� ������� ������� ��� ��������-���������� ��� �������, ��������� ������ ������� ������ ��� ����� ������� �������� ��������� ������ ���� �������, � ����� ���� �������� ������ ������ ��������� �� ������ �������� ���� 1996 ����. �������������� ������� ������ ����� ������� CustomerID � FreightSum, ������ ������� ������ ���� ������������� �� ����� ������� �������.
������� 13
�������� ������, ������� ������� 3 ������ � ���������� ����������, ������� ���� ������� ����� 1 �������� 1997 ���� ������������ � ���� ���������� � ������ ����� �������. ����� ��������� �������������� ��� ����� ��������� ������� ������ � ������ ��������. �������������� ������� ������ ����� ������� CustomerID, ShipCountry � OrderPrice, ������ ������� ������ ���� ������������� �� ��������� ������ � �������� �������.
������� 14
���������� ������ � �������������� �����������:
SELECT DISTINCT s.CompanyName,
(SELECT min(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MinPrice,
(SELECT max(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MaxPrice
FROM dbo.Products AS p
INNER JOIN dbo.Suppliers AS s ON p.SupplierID = s.SupplierID
ORDER BY s.CompanyName

������� 15
�������� ������, ������� ������� ������ ��������-���������� �� �������, ������� ������ ������ � ����������� ����������� ����� � �������� �������� ����� ������ Speedy Express. �������������� ������� ������ ����� ������� Customer � Employee, ������� Employee ������ ��������� FirstName � LastName ����������.
������� 16
�������� ������, ������� ������� ������ ��������� �� ��������� Beverages � Seafood, ������� ����� �������� � ����������� (Discontinued) � ������� �������� �� ������ � ���������� ������ 20 ����. �������������� ������� ������ ����� ������� ProductName, UnitsInStock, ContactName � Phone ����������. ������ ������� ������ ���� ������������� �� �������� ���������� ������.