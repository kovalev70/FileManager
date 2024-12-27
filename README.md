# Задание
Создать ASP .Net Core Angular приложение по загрузке файлов для пользователей. Пользователи заходят через аутентификацию Windows и идентифицируются в приложении по логину. При входе в приложение отображается список загруженных пользователем файлов и предоставляется возможность выгрузки, загрузки, удаления файлов. 
При загрузке рассчитывается хэш-сумма файла и сравнивается с хэшами уже загруженных файлов. При наличии файла с той же хэш-суммой файл не загружается и выдается соответствующее предупреждение. 
## Технологии
Angular, C#, Net. Core, Web API, EF Core (Database First), MS SQL

# Развертывание
## API
В файле FileManager/src/FileManager.Api/appsettings.json можно поменять DefaultConnection на вашу строку подключения к БД.

В AllowedOrigins можно поменять URL на ваш UI

Команда для миграции БД ```Update-Database```

Команда для публикации
```dotnet publish -c Release -o ./publish```

## UI
В файле src/FileManager.UI/src/environments/environment.ts можно поменять apiUrl на ваш URL API

UI можно развернуть в докере

```
docker build -t filemanager.ui .
docker run --name filemanager.ui -p 4200:4200 filemanager.ui
```
