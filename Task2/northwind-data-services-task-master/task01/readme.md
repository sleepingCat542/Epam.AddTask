# Northwind Data Services

## Задача 1 - Northwind OData Service

Цели:
* Изучить модель базы данных Northwind.
* Изучить основы протокола HTTP.
* Научиться использовать Postman для выполнения запросов к внешним REST API.
* Научиться использовать язык запросов OData для доступа к данным Northwind OData Service.
* Научиться создавать клиентское приложение для сервисов OData.


### Модель Northwind

В качестве основной модели данных в задаче используется модель учебной базы данных Northwind, схема которой находится в документе [Northwind Sample Database](https://www.zentut.com/wp-content/uploads/downloads/2013/06/Northwind-Sample-Database-Diagram.pdf). Связи между таблицами в диаграмме обозначены с помощью графической нотации "воронья лапка", объяснение которой дано в статье ["Data Modelling using ERD with Crow Foot Notation"](https://www.codeproject.com/Articles/878359/Data-Modelling-using-ERD-with-Crow-Foot-Notation).


#### Выполнение

1. Используя диаграмму, выясните, какие данные хранят в себе сущности модели.
2. Выясните кардинальность для связей между таблицами PK Table (таблица, которая содержит первичный ключ сущности) и FK Table (таблица, содержащая внешний ключ сущности). Заполните колонки Cardinality в таблице:

| PK Table      | Cardinality PK Table | FK Table             | Cardinality FK Table | Relationship |
| ------------- | -------------------- | -------------------- | -------------------- | ------------ |
| shippers      | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| employees     | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| employees     | Zero-or-One          | employees            |  One-or-Many         | One-to-Many  |
| employees     | -                    | territories          | -                    | Many-to-Many |
| customers     | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| customers     | -                    | customerdemographics | -                    | Many-to-Many |
| orders        | One and only one     | orderdetails         |  One-or-Many         | One-to-Many  |
| products      | One and only one     | orderdetails         |  One-or-Many         | One-to-Many  |
| suppliers     | One and only one     | products             |  One-or-Many         | One-to-Many  |
| categories    | One and only one     | products             |  One-or-Many         | One-to-Many  |
| region        | One and only one     | territories          |  One-or-Many         | One-to-Many  |

3. Выясните тип отношений между таблицами базы данных. Заполните колонку Relationship в таблице выше. Используйте статью [Many-to-Many Relationship in the Northwind database](http://blog.codeontime.com/2012/04/many-to-many-relationship-in-northwind.html), чтобы найти связи типа "многие-ко-многим".


#### Дополнительные материалы

* Гудов, Завозкин, Пфайф. Учебное пособие ["Базы данных"](http://unesco.kemsu.ru/study_work/method/DB/book/index.html)
* Сабитов, Пирогов. Учебный курс ["Введение в СУБД"](https://cmd.inp.nsk.su/~pirogov/DB/slides)


### Northwind OData Service

[OData](https://ru.wikipedia.org/wiki/Open_Data_Protocol) - это открытый стандартизированный протокол для запроса и обновления данных, который позволяет выполнять операции с ресурсами, используя команды ("методы") протокола HTTP.


#### Выполнение

1. Изучите основы протокола HTTP - структуру, основные методы (GET, POST, PUT, DELETE), коды состояния, заголовки. Используйте статьи ["Обзор протокола HTTP"](https://developer.mozilla.org/ru/docs/Web/HTTP/Overview) и ["Простым языком об HTTP"](https://habr.com/ru/post/215117/) в качестве обучающего материала, и [статью в википедии](https://ru.wikipedia.org/wiki/HTTP) в качестве справочика.
2. Научитесь использовать Postman, инструмент для работы с HTTP-сервисами и REST API.
    * Установите [Postman](https://www.getpostman.com/downloads/).
    * Освойте базовые сценарии работы c Postman. Можно использовать примеры из видео "[Postman: от простого API-теста до конечного сценария"](https://www.youtube.com/watch?v=hGmJMeE_ok0).
    * Используйте руководство ["Learning OData on Postman"](https://www.odata.org/getting-started/learning-odata-on-postman/), чтобы добавить в Postman коллекцию с готовыми URL.
    * Пройдите ["Basic Tutorial](https://www.odata.org/getting-started/basic-tutorial).
3. Создайте в Postman новую коллекцию с именем Northwind, в этой коллекции создайте такие запросы к [Northwind OData Service](https://services.odata.org/V2/Northwind/Northwind.svc/), которые будут удовлетворять описанию из таблицы ниже. После проверки запроса, занесите необходимые параметры в таблицу:

| Query Description                                                 | HTTP Verb | Url                                                 |
| ----------------------------------------------------------------- | --------- | ----------------------------------------------------|
| Get service metadata.                                             | GET       | /$metadata                                          |
| Get all customers.                                                | GET       | /Customers                                          |
| Get a customer with "ALFKI" id.                                   | GET       | /Customers('ALFKI')                                 |
| Get all orders.                                                   | GET       | /Orders                                             |
| Get an order with "10248" id.                                     | GET       | /Orders(10248)                                      |
| Get all orders for a customer with "ANATR" id.                    | GET       | /Customers('ANATR')/Orders                          |
| Get a customer for an order with "10248" id.                      | GET       | /Orders(10248)/Customer             		      |
| Get all shippers with "Federal Shipping" company name.            | GET       | /Shippers?$filter=CompanyName eq 'Federal Shipping' |
| Get a country for an employee with "5" id.                        | GET       | /Employees(5)/Country                               |
| Get a employee id for an order with "10251" id.                   | GET       | /Orders(10251)/EmployeeID                           |
| Get all customers with "London" city.                             | GET       | /Customers?$filter=City eq 'London'                 |
| Get all order details where quantity more than 100.               | GET       | /Order_Details?$filter=Quantity gt 100              |


Создайте самостоятельно еще минимум 5 сложных запросов и запишите их в таблицу.


#### Дополнительные материалы

* OData REST API — мелкие хитрости ([часть 1](https://habr.com/ru/company/databoom/blog/262937/), [часть 2](https://habr.com/ru/company/databoom/blog/263167/), [часть 3](https://habr.com/ru/company/databoom/blog/263435/))
* [What’s New with OData 4: OData 2 vs OData 4](https://www.progress.com/blogs/whats-new-with-odata-4-odata-2-vs-odata-4)


### Создание клиента для Northwind OData Service

Протокол OData имеет несколько версий (на текущий момент - четыре). Версии 1, 2 и 3 являются обратно совместимыми, но не версия 4 - она не является обратно совместимой. Работа с сервисами разных версий отличается с точки зрения реализации клиента. Также реализация клиента зависит от целевой платформы - .NET Framework или .NET Core. Поэтому подход к работе с сервисами OData на стороне клиента может значительно разниться в зависимости от версии сервиса и платформы.

Документация протокола OData
* [OData Version 3.0](https://www.odata.org/documentation/odata-version-3-0/)
* [OData Version 4.0](https://www.odata.org/documentation/odata-version-4-0/)


#### Сервисы WCF

[WCF](https://docs.microsoft.com/en-us/dotnet/framework/wcf/) - это фреймворк, который в прошлом широко использовался для [построения распределенных приложений](http://sergeyteplyakov.blogspot.com/2011/02/wcf.html) на базе платформы .NET. Существует много рабочих приложений, которые построены на его основе и успешно работают. На текущий момент [популярность фреймворка сильно упала](https://github.com/dotnet/wcf/issues/1784), однако поддержка работающих сервисов и работа с ними по-прежнему является актуальной задачей для крупных промышленных программных систем.

Url сервиса выглядит следующим образом:

```
https://services.odata.org/V3/Northwind/Northwind.svc/
```

Наличие [".svc" в пути сервиса](https://stackoverflow.com/questions/17363429/does-a-wcf-service-always-use-an-svc-file) намекает на реализацию сервиса с помощью WCF.


#### Создание клиента сервиса OData v3 для приложения .NET Framework

Работать с сервисом OData можно с помощью вызовов HTTP-методов, однако часто для связи с сервисом используются прокси-классы, которые генерируются автоматически с помощью [дополнительного инструментария](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/generating-the-data-service-client-library-wcf-data-services). Кодогенерация значительно упрощает написание клиента, снижает количество кода, которое требуется для полноценной работы с сервисом, а также снижает количество ошибок, которые может допустить разработчик.

Visual Studio имеет функциональность [Add Service Reference](https://docs.microsoft.com/en-us/visualstudio/data-tools/how-to-add-update-or-remove-a-wcf-data-service-reference?view=vs-2019), с помощью которой можно сгенерировать [прокси-классы для клиента](https://docs.microsoft.com/ru-ru/dotnet/framework/wcf/how-to-create-a-wcf-client). Однако в версиях 2017 и выше эта функциональность может работать некорректно.

1. Используя Postman, получите метаданные [сервиса v3](https://services.odata.org/V3/Northwind/Northwind.svc/) и сохраните их в файл _northwind-data-service.edmx_.
2. Замените в файле версию сервиса с 1.0 на 3.0, чтобы код метаданных выглядел следующим образом:

```
edmx:DataServices m:DataServiceVersion="3.0" m:MaxDataServiceVersion="3.0"
```

3. Используйте утилиту [DataSvcUtil](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/wcf-data-service-client-utility-datasvcutil-exe) из пакета .NET Framework, чтобы [сгенерировать прокси-классы](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-manually-generate-client-data-service-classes-wcf-data-services) для сущностей сервиса:

```
"%windir%\Microsoft.NET\Framework\v3.5\DataSvcUtil.exe" /dataservicecollection /in:northwind-data-service.edmx /out:NorthwindDataService3.cs
```

Утилита вернет ошибку:
```
error 7025: Option 'DataServiceCollection' can not be specified when 'Version' is set to the default value of '1.0'.
```

Ошибка возникла из-за того, что при вызове утилиты не была указана версия протокола.

4. Узнайте, какие версии сервиса поддерживает DataSvcUtil:

```
"%windir%\Microsoft.NET\Framework\v3.5\DataSvcUtil.exe" /?
```

Используйте утилиту DataSvcUtil снова, но на этот раз укажите номер версии сервиса 2.0. Утилита вернет ошибку:

```
error 7001: The element 'DataService' has an attribute 'DataServiceVersion' with an unrecognized version '3.0'.
```

Ошибка возникла из-за того, что эта версия утилиты поддерживает только версии протоколов 1.0 и 2.0.

(Несмотря на то, что сервис Northwind OData реализует версию протокола 3.0, он отдает метаданные в формате версии 1.0. Поэтому, чтобы продемонстрировать ограничения утилиты DataSvcUtil из набора утилит .NET Framework 3.5, потребовалась замена версии сервиса в пункте 2 на версию 3.)

5. Установите [WCF Data Services 5.6.3](https://www.microsoft.com/en-us/download/details.aspx?id=45308), найдите DataSvcUtil на диске (C:\Program Files (x86)\Microsoft WCF Data Services). Узнайте, какие версии формата метаданных поддерживает эта версия утилиты.
6. Используйте DataSvcUtil версии 5.6.3 с указанием версии метаданных 3.0. После этого на диске должен появиться файл _NorthwindDataService.cs_.
7. Создайте .NET Framework приложение, которое будет работать клиентом сервиса Northwind OData.
    * Создайте консольное приложение .NET Framework.
    * Скопируйте файл _NorthwindDataService.cs_ в папку проекта и добавьте скопированный файл в проект.
    * Добавьте сборки версии 5.6.3 из папки пакета WCF Data Services 5.6.3 (папка .NETFramework):
        * Microsoft.Data.Edm
        * Microsoft.Data.OData
        * Microsoft.Data.Services
        * Microsoft.Data.Services.Client
    * Соберите проект. В случае ошибок, проверьте версию сборок.
    * Добавьте код из примера ниже, соберите проект и запустите его:

```cs
NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
var customers = entities.Customers.ToArray();
Console.WriteLine("{0} customers in the service found.", customers.Length);
```

Программа должна вернуть:

```
20 customers in the service found.
```

8. Найдите базовый класс, от которого унаследован _NorthwindModel.NorthwindEntities_. 
      DataServiceContext
    * В какой сборке находится базовый класс?
      Data.Services.Client
    * По какому пути лежит эта сборка?
      C:\Program Files (x86)\Microsoft WCF Data Services\5.0\bin\.NETFramework\
    * Какая версия у сборки, в которой находится базовый класс?
      5.0.0.0
    * Найдите документацию для этого класса на портале [docs.microsoft.com](https://docs.microsoft.com/).
      https://docs.microsoft.com/en-us/dotnet/api/system.data.services.client.dataservicecontext?view=netframework-4.8

Базовый клиент готов.


#### Создание клиента сервиса OData v3 для приложения .NET Core

Прокси-классы, которые генерирует утилита DataSvcUtil, годятся и для приложений .NET Core.

1. Создайте консольное приложение .NET Core.
2. Скопируйте файл _NorthwindDataService.cs_ в папку проекта, файл в Solution Explorer в Visual Studio появится автоматически.
3. Добавьте сборки версии 5.6.3 из папки ".NETPortable" (не .NETFramework):
    * Microsoft.Data.Edm.dll
    * Microsoft.Data.OData.dll
    * Microsoft.Data.Services.Client.dll
4. Добавьте код предыдущего приложения, соберите проект, запустите приложение. Какая возникла ошибка и почему?
5. Примените [подход APM](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/asynchronous-programming-model-apm), чтобы сделать вызов к сервису асинхронным:

```cs
NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
IAsyncResult asyncResult;
asyncResult = entities.Customers.BeginExecute((ar) =>
{
	var customers = entities.Customers.EndExecute(ar).ToArray();
	Console.WriteLine("{0} customers in the service found.", customers.Length);
}, null);
WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
Console.ReadLine();
```

Запустите приложение.

6. Удалите из проекта сборки версии 5.6.3, которые были добавлены в шаге 3.
7. Установите nuget-пакет [Microsoft.Data.Services.Client](https://www.nuget.org/packages/Microsoft.Data.Services.Client/). Пакет _Microsoft.Data.Services.Client_ является основным для создания клиентов к сервисам OData в .NET Core. Можно не устанавливать пакет _WCF Data Services_, вместо него в приложениях .NET Core следует использовать nuget-пакет _Microsoft.Data.Services.Client_. Соберите проект и запустите приложение.
8. Использование APM подхода при написании асинхронного кода усложняет структуру кода, делает код запутанным. Вместо подхода APM можно использовать подход [TAP](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap), который [уменьшает количество кода и упрощает его структуру](http://jqyblogger.blogspot.com/2013/11/linq-query-error-message-on-windows.html). :

```cs
NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
var taskFactory = new TaskFactory<IEnumerable<NorthwindModel.Customer>>();
var customers = (await taskFactory.FromAsync(entities.Customers.BeginExecute(null, null), iar => entities.Customers.EndExecute(iar))).ToArray();
Console.WriteLine("{0} customers in the service found.", customers.Length);
Console.ReadLine();
```

9. Для того, чтобы вызывать асинхронные методы, которые помечены модификатором async, метод _Main_ тоже должен быть помечен модификатором async. Второе требование - метод _Main_ должен возвращать _Task_. Перепишите код как показано ниже, соберите проект.

```cs
static async Task Main(string[] args)
```

10. [Возможность помечать метод _Main_ модификатором async](https://blogs.msdn.microsoft.com/mazhou/2017/05/30/c-7-series-part-2-async-main/) появилась в C# только в версии 7.1. В случае ошибки компиляции ("Program does not contain a static 'Main' method suitable for an entry point"), [измените номер версии языка на 7.1 или выше](https://dailydotnettips.com/choosing-the-c-language-latest-version-minor-release-in-visual-studio-2017/). Соберите проект, запустите приложение.

Базовый клиент готов.


#### Утилизация потоков в асинхронной модели (уровень сложности - повышенный)

Перед выполнением прочтите и выполните примеры:

  * [Debug multithreaded applications in Visual Studio](https://docs.microsoft.com/en-us/visualstudio/debugger/debug-multithreaded-applications-in-visual-studio)
  * [Get started debugging multithreaded applications](https://docs.microsoft.com/en-us/visualstudio/debugger/get-started-debugging-multithreaded-apps)
  * [Tools to debug threads and processes in Visual Studio](https://docs.microsoft.com/en-us/visualstudio/debugger/debug-threads-and-processes)
  * [View threads in the Visual Studio debugger by using the Threads window](https://docs.microsoft.com/en-us/visualstudio/debugger/walkthrough-debugging-a-multithreaded-application)
  * [Walkthrough: Debug a multithreaded app using the Threads window](https://docs.microsoft.com/en-us/visualstudio/debugger/how-to-use-the-threads-window)

Выполнение:

1. Примените подход APM к коду клиента и расставьте брейкпоинты, как указано в коде:

```cs
NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
IAsyncResult asyncResult;
asyncResult = entities.Customers.BeginExecute((ar) => // breakpoint #1.1
{
	var customers = entities.Customers.EndExecute(ar).ToArray(); // breakpoint #1.2
	Console.WriteLine("{0} customers in the service found.", customers.Length);
}, null);
WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
Console.ReadLine(); // breakpoint #1.3
```

Запустите приложение и, используя окно _Threads_, запишите параметры ID, Managed ID и Name для текущего потока в каждой точке останова.

2. Примените подход TAP к коду клиента и расставьте брейкпоинты, как указано в коде:

```cs
NorthwindModel.NorthwindEntities entities = new NorthwindModel.NorthwindEntities(new Uri("https://services.odata.org/V3/Northwind/Northwind.svc"));
var taskFactory = new TaskFactory<IEnumerable<NorthwindModel.Customer>>();
var customers = (await taskFactory.FromAsync(entities.Customers.BeginExecute(null, null), (iar) => // breakpoint #2.1
{
	return entities.Customers.EndExecute(iar); // breakpoint #2.2
})).ToArray();
Console.WriteLine("{0} customers in the service found.", customers.Length);
Console.ReadLine(); // breakpoint #2.3
```

Запустите приложение и, используя окно _Threads_, запишите параметры ID, Managed ID и Name для текущего потока в каждой точке останова.

3. Заполните таблицу:

| Breakpoint | Thread ID   | Thread Managed ID | Thread Name |
| ---------- | ----------- | ----------------- | ----------- |
| #1.1       |             |                   |             |
| #1.2       |             |                   |             |
| #1.3       |             |                   |             |
| #2.1       |             |                   |             |
| #2.2       |             |                   |             |
| #2.3       |             |                   |             |

В чем разница между двумя подходами для брейкпоинтов #1.3 и #2.3?
