# ASP_RESTful_Web_API
ASP.NET Core 7 RESTful Web API (Entity Framework Core, Azure SQL, MS SQL, Docker, Docker Compose)

-------------------------------------------------------------------------------------
-- Первичное создание проекта (с автоматическим разворачиванием структур базы данных):
--------------------------------------------------------------------------------------
1) Выбираем в VS проект ASP.NET Core Web API
2) Выбираем NuGet пакеты:
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.EntityFrameworkCore.Tools
3) Запускаем в package manager colsole -> Консоль диспетчера пакетов (через поиск) для разворачивание структур таблицы BUFF_IMPORT_DATA
   PM> Scaffold-DbContext "Data Source=localhost;Initial Catalog=DatabaseTestDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context "ASP_NET_Code_RESTful_Context" -DataAnnotations -Tables "BUFF_IMPORT_DATA"
4) Добавляем контролеер
   Папка Controllers -> Добавить -> Создать шаблонный элемент -> Контроллер API с действиями, использующий Entity Framework

------------------------------------------------------------------------------------------------
-- Создание контейнера в Docker с подключением к базе данных MSSQL сервер (Docker Compose)
------------------------------------------------------------------------------------------------
1) В проекте VS добавляем Docker -> Добавить -> Поддержка Docker -> Target OS (Linux)
   Генерируется Dockerfile
2) В проекте VS -> Сборка -> Собрать решение
3) В проекте VS добавляем Docker Compose -> Добавить -> Поддержку оркестратора контейнеров -> Docker Compose -> Target OS (Linux)
   Генерируется docker-compose.yml
4) Создаем связанные контейнеры с помощью Docker Compose в Windows Powershell:
   -> Путь к проекту -> cd "d:\Прочие\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker-compose -f docker-compose.yml up
5) Проверяем
   -> http://localhost:49900/swagger/index.html

------------------------------------------------------------------------------------------------
-- Создание контейнера в Docker с подключением к базе данных MSSQL сервер (Docker)
------------------------------------------------------------------------------------------------
1) В проекте VS добавляем Docker -> Добавить -> Поддержка Docker -> Target OS (Linux)
   Генерируется Dockerfile
2) В проекте VS -> Сборка -> Собрать решение
3) Создание сети в Docker с подключением MSSQLContainer
   - В Windows PowerShell запускаем:
     -> docker network create docker-network
     -> docker network connect docker-network MSSQLContainer
     Проверяем настройки сети Docker
     -> docker network inspect docker-network
     (Пример: MSSQLContainer - 172.18.0.2)
4) Настройка DB:
   - подключение MS SQL 2022 Docker (.\appsettings\appsettings_mssql_docker.json заменить файл appsettings.json)
   - прописываем IP 172.18.0.2 в appsettings.json
5) Создаем образ в Windows Powershell:
   -> Путь к проекту -> cd "d:\Прочие\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker build -t asprestfulwebapi:work .
6) Создаем и запускаем контейнер в Windows Powershell:
   -> docker run --name ASP_RESTful_Web_API_Work --restart=always -p 49900:80 -d asprestfulwebapi:work
7) Cеть в Docker с подключением ASP_RESTful_Web_API_Work
   - В Windows PowerShell запускаем:
     -> docker network connect docker-network ASP_RESTful_Web_API_Work
     Проверяем настройки сети Docker
     -> docker network inspect docker-network
     (Пример: ASP_RESTful_Web_API_Work - (172.18.0.3) 172.19.0.3)
8) Проверяем
   -> http://localhost:49900/swagger/index.html

- Структуру базы данных для MS SQL 2022 Docker можно взять из проекта Github (#Docker-Win11)

------------------------------------------------------------------------------------------------
-- Создание контейнера в Docker с подключением к базе данных AzureSQL или отдельный MSSQL сервер
------------------------------------------------------------------------------------------------
1) В проекте VS добавляем Docker -> Добавить -> Поддержка Docker -> Target OS (Linux)
   Генерируется Dockerfile
2) В проекте VS -> Сборка -> Собрать решение
3) Настройка DB:
   - подключение AzureSQL (.\appsettings\appsettings_azuresql.json заменить файл appsettings.json)
   - подключение MS SQL 2022 без пароля (.\appsettings\appsettings_mssql.json заменить файл appsettings.json)
   - подключение MS SQL 2022 с паролем (.\appsettings\appsettings_mssql_SA.json заменить файл appsettings.json)
4) Создаем образ в Windows Powershell:
   -> Путь к проекту -> cd "d:\Прочие\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker build -t asprestfulwebapi:work .
5) Создаем и запускаем контейнер в Windows Powershell:
   -> docker run --name ASP_RESTful_Web_API_Work --restart=always -p 49900:80 -d asprestfulwebapi:work
6) Проверяем
   -> http://localhost:49900/swagger/index.html

- Структуру базы данных для MSSQL можно взять из проекта Github (# Microsoft_SQL_Server_2022)
- Структуру базы данных для AzureSQL можно взять из проекта Github (# Microsoft Azure SQL Databases)
