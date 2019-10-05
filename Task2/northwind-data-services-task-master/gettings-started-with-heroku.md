# Northwind Data Services

## Использование платформы Heroku

[Heroku](https://ru.wikipedia.org/wiki/Heroku) - это облачная платформа, которая предоставляет сервис по созданию и обслуживанию веб-приложений, а также позволяет подключать необходимые для работы приложения сервисы. Существует минимальный пакет услуг, который предоставляется бесплатно - [Free Plan](https://ru.wikipedia.org/wiki/Heroku).

Используйте [руководство](http://student.webcamp.com.ua/rest_student/heroku101-first), чтобы:
1. Создать аккаунт на Heroku.
2. Установить Heroku CLI.
3. Зайти в систему через командную строку.

### Создание приложения

1. Создайте приложение:

```
$ heroku apps:create
Creating app... done, ⬢ warm-beyond-81154
https://warm-beyond-81154.herokuapp.com/ | https://git.heroku.com/warm-beyond-81154.git
```

Каждому приложению в heroku по-умолчанию присваивается случайное имя. Для данного приложения платформа сгенерировала имя _warm-beyond-81154_.

2. Получите список приложений:

```
$ heroku apps
=== youraccount@mail.ru Apps
warm-beyond-81154
```

3. Получите подробную информацию о приложении, используя параметр -a:

```
$ heroku apps:info -a warm-beyond-81154
=== warm-beyond-81154
Auto Cert Mgmt: false
Dynos:
Git URL:        https://git.heroku.com/warm-beyond-81154.git
Owner:          youraccount@mail.ru
Region:         us
Repo Size:      0 B
Slug Size:      0 B
Stack:          heroku-18
Web URL:        https://warm-beyond-81154.herokuapp.com/
```

Приложению было присвоено доменное имя _warm-beyond-81154.herokuapp.com_, и создан репозиторий _git.heroku.com/warm-beyond-81154.git_, который нужно использовать для размещения исходного кода приложения.

4. Откройте список приложений на портале [dashboard.heroku.com](https://dashboard.heroku.com/apps).

5. Откройте панеь упавления приложением (замените имя приложения) - [dashboard.heroku.com/apps/warm-beyond-81154](https://dashboard.heroku.com/apps/warm-beyond-81154).


### Создание СУБД

1. Добавьте СУБД - сервис добавляется как addon к приложению.

```
$ heroku addons:create heroku-postgresql:hobby-dev -a warm-beyond-81154
Creating heroku-postgresql:hobby-dev on ⬢ warm-beyond-81154... free
Database has been created and is available
 ! This database is empty. If upgrading, you can transfer
 ! data from another database with pg:copy
Created postgresql-fluffy-44621 as DATABASE_URL
Use heroku addons:docs heroku-postgresql to view documentation
```

СУБД было присвоено уникальное имя _postgresql-fluffy-44621_.

2. Посмотрите список addons для приложения:

```
$ heroku addons -a warm-beyond-81154

Add-on                                       Plan       Price  State
───────────────────────────────────────────  ─────────  ─────  ───────
heroku-postgresql (postgresql-fluffy-44621)  hobby-dev  free   created
 └─ as DATABASE

The table above shows add-ons and the attachments to the current app (warm-beyond-81154) or other apps.
```

3. Посмотрите детальную информацию о СУБД:

```
$ heroku pg:info -a warm-beyond-81154
=== DATABASE_URL
Plan:                  Hobby-dev
Status:                Available
Connections:           0/20
PG Version:            11.4
Created:               2019-07-10 11:02 UTC
Data Size:             7.7 MB
Tables:                0
Rows:                  0/10000 (In compliance)
Fork/Follow:           Unsupported
Rollback:              Unsupported
Continuous Protection: Off
Add-on:                postgresql-fluffy-44621
```

Подробную информацию см. в статье [Heroku Postgres](https://devcenter.heroku.com/articles/heroku-postgresql).


### Развертывание БД Northwind

Для PostgreSQL существует адаптированная версия БД Northwind - [pthom/northwind_psql](https://github.com/pthom/northwind_psql).

1. Скачайте файл [northwind.sql](https://raw.githubusercontent.com/pthom/northwind_psql/master/northwind.sql).
2. Выполните команды из файла northwind.sql, используя команду psql:

```
$ heroku pg:psql -a warm-beyond-81154 -f northwind.sql
--> Connecting to postgresql-fluffy-44621
...
```

Время исполнения скрипта ~ 5 мин.

3. Посмотрите информацию о СУБД:

$ heroku pg:info -a warm-beyond-81154
=== DATABASE_URL
Plan:                  Hobby-dev
Status:                Available
Connections:           0/20
PG Version:            11.4
Created:               2019-07-10 11:02 UTC
Data Size:             8.9 MB
Tables:                14
Rows:                  3362/10000 (In compliance)
Fork/Follow:           Unsupported
Rollback:              Unsupported
Continuous Protection: Off
Add-on:                postgresql-fluffy-44621
```

"Rows" показывает общее количество строк во всех таблицах базы данных - 3362. Бесплатный тарифный план Hobby-dev разрешает иметь в БД 10000 строк. Таким образом, в базу на этом тарифном плане можно добавить еще 6638 строк (10000-3362).