# ASP_RESTful_Web_API
ASP.NET Core 8 RESTful Web API (Entity Framework Core, Azure SQL, MS SQL, Docker, Docker Compose)

-------------------------------------------------------------------------------------
- Первинне створення проекту (з автоматичним розгортанням структури бази даних):
--------------------------------------------------------------------------------------
1) Вибираємо у VS проект ASP.NET Core Web API
2) Вибираємо NuGet пакети:
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.EntityFrameworkCore.Tools
3) Запускаємо в package manager colsole -> Консоль диспетчера пакетів (через пошук) для розгортання структур таблиці BUFF_IMPORT_DATA
   PM> Scaffold-DbContext "Data Source = localhost; Initial Catalog = DatabaseTestDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultiActiveResultSets = False; Connect Timeout = 60; EntityFrameworkCore.SqlServer -OutputDir Models -Context "ASP_NET_Code_RESTful_Context" -DataAnnotations -Tables "BUFF_IMPORT_DATA"
4) Додаємо контролер
   Папка Controllers -> Додати -> Створити шаблонний елемент -> Контролер API з діями, що використовує Entity Framework

------------------------------------------------------------------------------------------------
-- Створення контейнера в Docker із підключенням до бази даних MSSQL сервер (Docker Compose)
------------------------------------------------------------------------------------------------
1) У проекті VS додаємо Docker -> Додати -> Підтримка Docker -> Target OS (Linux)
   Генерується Dockerfile
2) У проекті VS -> Складання -> Зібрати рішення
3) У проекті VS додаємо Docker Compose -> Додати -> Підтримку оркестратора контейнерів -> Docker Compose -> Target OS (Linux)
   Генерується docker-compose.yml
4) Створюємо пов'язані контейнери за допомогою Docker Compose у Windows Powershell:
   -> Шлях до проекту -> cd "d:\Інші\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker-compose -f docker-compose.yml up
5) Перевіряємо
   -> http://localhost:49900/swagger/index.html

------------------------------------------------------------------------------------------------
-- Створення контейнера в Docker із підключенням до бази даних MSSQL сервер (Docker)
------------------------------------------------------------------------------------------------
1) У проекті VS додаємо Docker -> Додати -> Підтримка Docker -> Target OS (Linux)
   Генерується Dockerfile
2) У проекті VS -> Складання -> Зібрати рішення
3) Створення мережі в Docker із підключенням MSSQLContainer
   - У Windows PowerShell запускаємо:
     -> docker network create docker-network
     -> docker network connect docker-network MSSQLContainer
     Перевіряємо налаштування мережі Docker
     -> docker network inspect docker-network
     (Приклад: MSSQLContainer - 172.18.0.2)
4) Налаштування DB:
   - підключення MS SQL 2022 Docker (.\appsettings\appsettings_mssql_docker.json замінити файл appsettings.json)
   - прописуємо IP 172.18.0.2 у appsettings.json
5) Створюємо образ у Windows Powershell:
   -> Шлях до проекту -> cd "d:\Інші\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker build -t asprestfulwebapi:work .
6) Створюємо та запускаємо контейнер у Windows Powershell:
   -> docker run --name ASP_RESTful_Web_API_Work --restart=always -p 49900:80 -d asprestfulwebapi:work
7) Мережа в Docker із підключенням ASP_RESTful_Web_API_Work
   - У Windows PowerShell запускаємо:
     -> docker network connect docker-network ASP_RESTful_Web_API_Work
     Перевіряємо налаштування мережі Docker
     -> docker network inspect docker-network
     (Приклад: ASP_RESTful_Web_API_Work - (172.18.0.3) 172.19.0.3)
8) Перевіряємо
   -> http://localhost:49900/swagger/index.html

- Структуру бази даних для MS SQL 2022 Docker можна взяти із проекту Github (#Docker-Win11)

------------------------------------------------------------------------------------------------
-- Створення контейнера в Docker з підключенням до бази даних AzureSQL або окремого MSSQL сервера
------------------------------------------------------------------------------------------------
1) У проекті VS додаємо Docker -> Додати -> Підтримка Docker -> Target OS (Linux)
   Генерується Dockerfile
2) У проекті VS -> Складання -> Зібрати рішення
3) Налаштування DB:
   - підключення AzureSQL (.\appsettings\appsettings_azuresql.json замінити файл appsettings.json)
   - підключення MS SQL 2022 без пароля (.\appsettings\appsettings_mssql.json замінити файл appsettings.json)
   - Підключення MS SQL 2022 з паролем (.\appsettings\appsettings_mssql_SA.json замінити файл appsettings.json)
4) Створюємо образ у Windows Powershell:
   -> Шлях до проекту -> cd "..\Project\Project C#\ASP_RESTful_Web_API\"
   -> docker build -t asprestfulwebapi:work .
5) Створюємо та запускаємо контейнер у Windows Powershell:
   -> docker run --name ASP_RESTful_Web_API_Work --restart=always -p 49900:80 -d asprestfulwebapi:work
6) Перевіряємо
   -> http://localhost:49900/swagger/index.html

- Структуру бази даних для MSSQL можна взяти із проекту Github (# Microsoft_SQL_Server_2022)
- Структуру бази даних для AzureSQL можна взяти із проекту Github (# Microsoft Azure SQL Databases)