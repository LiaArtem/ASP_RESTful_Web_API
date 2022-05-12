# ASP_RESTful_Web_API
ASP.NET Core 6 RESTful Web API (Entity Framework Core, база данных MS SQL)

База данных взята из проекта Github - # Microsoft_SQL_Server_2019

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