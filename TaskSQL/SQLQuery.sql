--1
--Напишите запрос, который выводит список заказчиков в виде таблицы, состоящей из двух столбцов CustomerID и CompanyName. 
--Строки таблицы должны быть отсортированы по коду заказчика.
select CustomerID, CompanyName from 
Customers order by CustomerID;


--2
--Напишите запрос, который выводит EmployeeID последнего нанятого компанией сотрудника.
select top 1 EmployeeID from Employee order by EmployeeID desc;


--3
--Напишите запрос, который выводит список всех стран из колонки dbo.Customers.Country.
--Список должен быть отсортирован в алфавитном порядке, должен содержать только уникальные значения и не должен содержать дубликаты.
select distinct dbo.Customers.Country from dbo.Customers order by dbo.Customers.Country; 


--4
--Напишите запрос, который выводит список названий компаний-заказчиков, расположенных в следущих городах: Берлин, Лондон, Мадрид, Брюссель, Париж.
--Список должен быть отсортирован по коду-идентификатору компании в обратном алфавитном порядке.
select CompanyName, CompanyID from Companies where City in('Берлин', 'Лондон','Брюссель', 'Мадрид', 'Париж') order by CompanyID desc


--5
--Напишите запрос, который выводит список идентификаторов компаний, для которых заказы были доставлены 
--(dbo.Orders.RequiredDate) в сентябре 1996 года. Список должен быть отсортирован в алфавитном порядке.
select CompanyID from Companies, Orders where (Companies.CompanyName = Orders.CompanyName and Orders.RequiredDate between '01.09.1996' and '30.09.1996') order by CompanyID


--6
--Напишите запрос, который выводит имя контактного лица компании-заказчика, чей номер телефона начинается 
--с кода "171" и содержит "77", а также номер факса начинается с кода "171" и оканчивается на "50".
select Surname from Employee where  ((Phone like'(171)%' and Phone like '%77%') and (Fax like '(171)%' and Fax like '%50' ));


--7
--Напишите запрос, который выводит количество компаний-заказчиков, которые находятся в городах, принадлежащих 
--трем скандинавским странам. Результирующая таблица должна состоять из двух колонок City и CustomerCount.
select count(CompanyName)[CustomerCount], City from Companies 
where Companies.City in 
(select Countries.City from Countries where Countries.CountryName in('Дания', 'Норвегия', 'Швеция')) 
group by City; 


--8
--Напишите запрос, который выводит количество компаний-заказчиков в странах, в которых есть 10 и более заказчиков.
--Результирующая таблица должна иметь колонки Country и CustomerCount, строки которой должны быть отсортированы в обратном порядке по количеству заказчиков в стране.
select count(Companies.CompanyName)[CustomerCount], Countries.CountryName[Country] 
from Companies inner join Countries 
on Companies.City=Countries.City 
group by Countries.CountryName
having count(Companies.CompanyName)>1
order by CustomerCount desc;

--9!!
--Напишите запрос, который выводит среднюю стоимость фрахта (dbo.Orders.Freight) заказов для компаний-заказчиков,
--которые указывали местом доставки заказа город, принадлежащий Великобритании или Канаде.
--Дополнительным критерием выборки является значение средней стоимости фрахта заказа - больше или равно 100, или меньше 10.
--Результирующая таблица должна иметь колонки CustomerID и FreightAvg, значение средней стоимости должно быть округлено до целого значения, строки должны быть отсортированы в обратном порядке по значению среднего значения 
select round(avg(cast(dbo.Orders.Freight as float(4))),0) [FreightAvg], CustomerID
from Orders inner join Customers 
on Orders.CompanyName=Customers.CompanyName 
where Customers.CountryName in('Канада', 'Великобритания') 
and (FreightAvg >=100 or FreightAvg<10)
order by FreightAvg desc;




--10
--Напишите запрос, который выводит EmployeeID предпоследнего нанятого компанией сотрудника. Используйте подзапрос для исключения последнего нанятого сотрудника.
select top 1 * from
(
 select top 2 *from Employee order by EmployeeID desc
) emp
order by EmployeeID;


--11
--Напишите запрос, который выводит EmployeeID предпоследнего нанятого компанией сотрудника. Используйте ключевые слова OFFSET и FETCH.
Select *
From  Employee
Order by  EmployeeID desc
Offset 1 Rows
Fetch Next 1 Rows Only;

--12
--Напишите запрос, который выводит общую сумму фрахтов заказов для компаний-заказчиков для заказов, стоимость фрахта
--которых больше или равна средней величине стоимости фрахта всех заказов, а также дата отгрузки заказа должна находится
--во второй половине июля 1996 года. Результирующая таблица должна иметь колонки CustomerID и FreightSum,
--строки которой должны быть отсортированы по сумме фрахтов заказов.
select sum(Orders.Freight) from Orders

--13
--Напишите запрос, который выводит 3 заказа с наибольшей стоимостью, которые были созданы после 1 сентября 1997 года
--включительно и были доставлены в страны Южной Америки. Общая стоимость рассчитывается как сумма стоимости деталей
--заказа с учетом дисконта. Результирующая таблица должна иметь колонки CustomerID, ShipCountry и OrderPrice, строки
--которой должны быть отсортированы по стоимости заказа в обратном порядке.


--14
--Перепишите запрос с использованием группировки:
SELECT DISTINCT s.CompanyName,
(SELECT min(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MinPrice,
(SELECT max(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MaxPrice
FROM dbo.Products AS p
INNER JOIN dbo.Suppliers AS s ON p.SupplierID = s.SupplierID
ORDER BY s.CompanyNam

--15
--Напишите запрос, который выводит список компаний-заказчиков из Лондона, которые делали заказы у сотрудников лондонского офиса
--и заказали доставку через службу Speedy Express. Результирующая таблица должна иметь колонки Customer и Employee,
--колонка Employee должна содержать FirstName и LastName сотрудника.


--16
--Напишите запрос, который выводит список продуктов из категорий Beverages и Seafood,
 --которые можно заказать у поставщиков (Discontinued) и которые остались на складе в количестве меньше 20 штук.
  --Результирующая таблица должна иметь колонки ProductName, UnitsInStock, ContactName и Phone поставщика.
   --Строки таблицы должны быть отсортированы по значению складского запаса.