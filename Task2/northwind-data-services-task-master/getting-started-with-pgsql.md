# Использование PostgreSQL


## Локальная установка БД

Установку приложения БД PostgreSQL можно произвести:
* Используя установщик [EnterpriseDB](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads).
* Вручную из [zip-архива](https://www.enterprisedb.com/download-postgresql-binaries).


### Установка из zip-архива

1. Скачать zip-архив с текущей версией (на момент создания руководства - Version 11.4).
2. Распаковать архив
3. Переместить папку pgsql из архива в подходящее место (пример - d:\northwind\pgsql):

```
$ dir d:\northwind\pgsql /b
bin
doc
include
lib
pgAdmin 4
share
StackBuilder
symbols
```

4. Добавьте путь pgsql\bin в переменную окружения Path - [руководство](https://www.computerhope.com/issues/ch000549.htm).
5. Следуйте инструкциям по созданию новой СУБД - [руководство](https://stackoverflow.com/questions/26441873/starting-postgresql-and-pgadmin-in-windows-without-installation).

```
$ initdb -D d:\northwind\pgdata -U postgres -W -E UTF8 -A scram-sha-256
```

Запуск и остановка СУБД:

```
$ pg_ctl -D d:\northwind\pgdata start
$ pg_ctl -D d:\northwind\pgdata stop
```


## Создание экземпляра СУБД в облаке Heroku

См. руководство в [Использование платформы Heroku](task-02-heroku.md).