# NoteApp

Данное приложение (backend) было разработано для работы с заметками, напоминаниями и тэгами. Оно реализует такие функции: создание, редактирование, удаление, просмотр заметок, напоминаний и тэгов. Для тэгов была добавлена привязка к заметкам и напоминаниям. Статус разработки: в разработке, процент покрытия тестами: реализованы только 2 теста для заметок.

## Содержание

- [Технологии](#технологии)
- Начало работы
- Тестирование
- Deploy и CI/CD
- Contributing
- To do
- Команда проекта

## Технологии

- [C# и .NET7](https://dotnet.microsoft.com/ru-ru/download/dotnet/7.0)
- ORM EntityFramework Core
- FluentValidation
- Паттерн Repository
- Парадигма CQRS
- MediatR

## Использование

Скачайте Pre_release NoteApp-0.9 разархивируйте архив и запустите NoteApp.exe. Далее необоходимо данный адрес

<img width="594" height="217" alt="изображение" src="https://github.com/user-attachments/assets/67d010bd-12c1-4161-b780-6b5a887c2fba" /> 

ввести в строку Postman, добавить необходимый маршрут и ввести JSON:

<img width="933" height="515" alt="изображение" src="https://github.com/user-attachments/assets/7133369c-1811-4713-81ab-a16c1f0ae3d5" />:

Примеры JSON для API контроллеров:

- POST /api/v1/function/note/create - Создание заметки
```json
{  
    "title" : "Название для заметки",
    "text" : "Текст для заметки"
}
```
- POST /api/v1/function/note/delete/<название заметки> - Удаление заметки
- POST /api/v1/function/note/update/<название заметки> - Редактирование заметки
```json
{  
    "title" : "Старое название для заметки",
    "text" : "Измененный текст"
}
```
- POST /api/v1/function/note/get/<название заметки> - Получение заметки
- POST /api/v1/function/note/get-all - Получение всех заметок
    
- POST /api/v1/function/reminder/create - Создание напоминания
```json
{  
    "title" : "Название напоминания",
    "text" : "Текст напоминания",
    "remindertime" : "Время для напоминания: 2025-07-24T06:14:00Z или null"
} 
```
- POST /api/v1/function/reminder/delete/<название напоминания> - Удаление напоминания
- POST /api/v1/function/reminder/update/<название напоминания> - Редактирование напоминания
```json
{  
    "title" : "Старое название напоминания",
    "text" : "Новый текст напоминания",
    "remindertime" : "Новое время"
}
```
- POST /api/v1/function/reminder/get/<название напоминания> - Получение напоминания
- POST /api/v1/function/reminder/get-all - Получение всех напоминаний

- POST /api/v1/function/tag/create - Создание тэга
```json
{  
    "name" : "Название тэга",
    "notetitle" : "Название существующей заметки(привязка к заметке)",
    "remindertitle" : "Название существующего напоминания(привязка к напоминанию)"
}
```
- POST /api/v1/function/tag/delete/<название тэга> - Удаление тэга
- POST /api/v1/function/tag/update/<название тэга> - Редактирование тэга(не работает)
```json
{  
    "name" : "Старое название тэга",
    "notetitle" : "Новое название заметки",
    "remindertitle" : "Новое название напоминания"
}
```
- POST /api/v1/function/tag/get/<название тэга> - Получение тэга
- POST /api/v1/function/tag/get-all - Получение всех тэгов

## Разработка

### Требования

Для установки и запуска проекта, необходимо: 
- [.NET SDK 7.0.410](https://dotnet.microsoft.com/ru-ru/download/dotnet/7.0)
- [PostgreSQL 17.5](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
- [Postman](https://www.postman.com/downloads/)

Также при установке PostgreSQL нужно указать 5432 порт и пароль "gjcnuhtc5165". При необходимсти можно поменять конфигурацию в файле DB/Context/ApplicationContext.cs

### Запуск Development сервера

Чтобы запустить сервер для разработки, перейдите в корневую папку NoteApp

<img width="627" height="326" alt="изображение" src="https://github.com/user-attachments/assets/180945bf-25ba-4da3-b045-8dd89f87d6e0" />

и выполните команду:
```cmd
dotnet run
```

### Создание билда

Чтобы выполнить production сборку, выполните команду:
```cmd
dotnet build
```

### Тестирование

Проект был покрыт двумя юнит-тестами Xunit. Для их запуска перейдите в папку NoteApp.Tests выполните команду:
```cmd
dotnet test
```
