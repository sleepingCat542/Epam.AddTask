--1
--�������� ������, ������� ������� ������ ���������� � ���� �������, ��������� �� ���� �������� CustomerID � CompanyName. 
--������ ������� ������ ���� ������������� �� ���� ���������.
select CustomerID, CompanyName from 
Customers order by CustomerID;


--2
--�������� ������, ������� ������� EmployeeID ���������� �������� ��������� ����������.
select top 1 EmployeeID from Employee order by EmployeeID desc;


--3
--�������� ������, ������� ������� ������ ���� ����� �� ������� dbo.Customers.Country.
--������ ������ ���� ������������ � ���������� �������, ������ ��������� ������ ���������� �������� � �� ������ ��������� ���������.
select distinct dbo.Customers.Country from dbo.Customers order by dbo.Customers.Country; 


--4
--�������� ������, ������� ������� ������ �������� ��������-����������, ������������� � �������� �������: ������, ������, ������, ��������, �����.
--������ ������ ���� ������������ �� ����-�������������� �������� � �������� ���������� �������.
select CompanyName, CompanyID from Companies where City in('������', '������','��������', '������', '�����') order by CompanyID desc


--5
--�������� ������, ������� ������� ������ ��������������� ��������, ��� ������� ������ ���� ���������� 
--(dbo.Orders.RequiredDate) � �������� 1996 ����. ������ ������ ���� ������������ � ���������� �������.
select CompanyID from Companies, Orders where (Companies.CompanyName = Orders.CompanyName and Orders.RequiredDate between '01.09.1996' and '30.09.1996') order by CompanyID


--6
--�������� ������, ������� ������� ��� ����������� ���� ��������-���������, ��� ����� �������� ���������� 
--� ���� "171" � �������� "77", � ����� ����� ����� ���������� � ���� "171" � ������������ �� "50".
select Surname from Employee where  ((Phone like'(171)%' and Phone like '%77%') and (Fax like '(171)%' and Fax like '%50' ));


--7
--�������� ������, ������� ������� ���������� ��������-����������, ������� ��������� � �������, ������������� 
--���� ������������� �������. �������������� ������� ������ �������� �� ���� ������� City � CustomerCount.
select count(CompanyName)[CustomerCount], City from Companies 
where Companies.City in 
(select Countries.City from Countries where Countries.CountryName in('�����', '��������', '������')) 
group by City; 


--8
--�������� ������, ������� ������� ���������� ��������-���������� � �������, � ������� ���� 10 � ����� ����������.
--�������������� ������� ������ ����� ������� Country � CustomerCount, ������ ������� ������ ���� ������������� � �������� ������� �� ���������� ���������� � ������.
select count(Companies.CompanyName)[CustomerCount], Countries.CountryName[Country] 
from Companies inner join Countries 
on Companies.City=Countries.City 
group by Countries.CountryName
having count(Companies.CompanyName)>1
order by CustomerCount desc;

--9
--�������� ������, ������� ������� ������� ��������� ������ (dbo.Orders.Freight) ������� ��� ��������-����������,
--������� ��������� ������ �������� ������ �����, ������������� �������������� ��� ������.
--�������������� ��������� ������� �������� �������� ������� ��������� ������ ������ - ������ ��� ����� 100, ��� ������ 10.
--�������������� ������� ������ ����� ������� CustomerID � FreightAvg, �������� ������� ��������� ������ ���� ��������� �� ������ ��������, ������ ������ ���� ������������� � �������� ������� �� �������� �������� �������� 
select round(avg(cast(dbo.Orders.Freight as float(4))),0) [FreightAvg], CustomerID
from Orders inner join Customers 
on Orders.CompanyName=Customers.CompanyName 
where (Customers.CountryName in('������', '��������������') 
and (FreightAvg >=100 or FreightAvg<10))
order by FreightAvg desc;



--10
--�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� ��������� ��� ���������� ���������� �������� ����������.
select top 1 * from
(
 select top 2 *from Employee order by EmployeeID desc
) emp
order by EmployeeID;


--11
--�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� �������� ����� OFFSET � FETCH.
Select *
From  Employee
Order by  EmployeeID desc
Offset 1 Rows
Fetch Next 1 Rows Only;

--12
--�������� ������, ������� ������� ����� ����� ������� ������� ��� ��������-���������� ��� �������, ��������� ������
--������� ������ ��� ����� ������� �������� ��������� ������ ���� �������, � ����� ���� �������� ������ ������ ���������
--�� ������ �������� ���� 1996 ����. �������������� ������� ������ ����� ������� CustomerID � FreightSum,
--������ ������� ������ ���� ������������� �� ����� ������� �������.
select sum(Orders.Freight)[FrightSum], CustomerID 
from Orders inner join Customers on Orders.CompanyName=Customers.CompanyName
where (Orders.Freight>=avg(Orders.Freight) and Orders.ShippingDate between '1996-07-15' and '1996-07-31')
order by sum(Orders.Freight)



--13
--�������� ������, ������� ������� 3 ������ � ���������� ����������, ������� ���� ������� ����� 1 �������� 1997 ����
--������������ � ���� ���������� � ������ ����� �������. ����� ��������� �������������� ��� ����� ��������� �������
--������ � ������ ��������. �������������� ������� ������ ����� ������� CustomerID, ShipCountry � OrderPrice, ������
--������� ������ ���� ������������� �� ��������� ������ � �������� �������.
select top (3) CustomerID, DeliveryCountry[ShipCountry], OrderPrice  
from Orders inner join Customers on  Orders.CompanyName=Customers.CompanyName
inner join Countries on Customers.Country=Countries.CountryName
where (CreateData>='1997-09-01' and Countries.CountryName in ('��������', '����', '����')) order by OrderPrice desc


--14!!!
--���������� ������ � �������������� �����������:


--15
--�������� ������, ������� ������� ������ ��������-���������� �� �������, ������� ������ ������ � ����������� ����������� �����
--� �������� �������� ����� ������ Speedy Express. �������������� ������� ������ ����� ������� Customer � Employee,
--������� Employee ������ ��������� FirstName � LastName ����������.
select CompanyName[Customer], (select CONCAT(Employee.FirstName, ' ', Employee.Surname))[Employee] from 
Employee inner join Companies on Employee.EmployeeID=Companies.Director
inner join Customers on Customers.CompanyName=Companies.CompanyName
where Orders.Service='Speedy Express' and Companies.CompanyName=Customers.CompanyName and Companies.City='������'



--16
--�������� ������, ������� ������� ������ ��������� �� ��������� Beverages � Seafood,
 --������� ����� �������� � ����������� (Discontinued) � ������� �������� �� ������ � ���������� ������ 20 ����.
  --�������������� ������� ������ ����� ������� ProductName, UnitsInStock, ContactName � Phone ����������.
   --������ ������� ������ ���� ������������� �� �������� ���������� ������.
select ProductName, UnitsInStock, Surname[ContactName], Phone 
from Employee inner join Companies on Employee.EmployeeID=Companies.Director
inner join Products on Companies.CompanyName=Products.Discontinued 
where Products.Categories in ('Beverages', 'Seafood') and UnitsInStock>20 
order by UnitsInStock