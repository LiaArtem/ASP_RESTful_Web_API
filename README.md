# ASP_RESTful_Web_API
ASP.NET Core 7 RESTful Web API (Entity Framework Core, Azure SQL, MS SQL, Docker)

- База данных для MSSQL взята из проекта Github (# Microsoft_SQL_Server_2022 или #Docker-Win11)
- База данных для AzureSQL взята из проекта Github (# Microsoft Azure SQL Databases)

Памятка при первичном создании проекта:

1) Выбираем в VS проект ASP.NET Core Web API
2) Ставим NuGet пакеты:
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.EntityFrameworkCore.Tools
   - Microsoft.VisualStudio.Web.CodeGeneration.Design

3) Запускаем в package manager colsole -> Консоль диспетчера пакетов (через поиск) для разворачивание структур таблицы BUFF_IMPORT_DATA
   PM> Scaffold-DbContext "Data Source=localhost;Initial Catalog=DatabaseTestDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context "ASP_NET_Code_RESTful_Context" -DataAnnotations -Tables "BUFF_IMPORT_DATA"

4) Добавляем контролеер
   Папка Controllers -> Добавить -> Создать шаблонный элемент -> Контроллер API с действиями, использующий Entity Framework

5) Настройка DB:
   - подключение AzureSQL (.\appsettings\appsettings_azuresql.json заменить файл appsettings.json)
   - подключение MS SQL 2022 без пароля (.\appsettings\appsettings_mssql.json заменить файл appsettings.json)
   - подключение MS SQL 2022 с паролем (.\appsettings\appsettings_mssql_SA.json заменить файл appsettings.json)
   - подключение MS SQL 2022 Docker (.\appsettings\appsettings_mssql_docker.json заменить файл appsettings.json)

----------------------------------------------------------------------------
-- Создание сети в Docker с подключением
----------------------------------------------------------------------------
6) В Windows PowerShell запускаем:
-> docker network create docker-network
-> docker network connect docker-network MSSQLContainer
-> docker network connect docker-network ASP_RESTful_Web_API

7) Проверяем настройки сети Docker
-> docker network inspect docker-network
-> В результате MSSQLContainer - 172.18.0.2, ASP_RESTful_Web_API - 172.18.0.3

   Перечень контейнеров
   -> docker ps