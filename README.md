# Тестирование калькулятора бюджета

Это самостоятельный проект: для передачи достаточно этой папки целиком. Решение `BudgetCalculator.sln` содержит API и
тесты; файлы других заданий не требуются.

Установите .NET SDK 9. Откройте PowerShell в папке проекта.

Восстановление зависимостей и сборка:

```powershell
dotnet restore BudgetCalculator.sln
dotnet build BudgetCalculator.sln
```

Тесты и отчёт покрытия:

```powershell
dotnet test BudgetCalculator.sln --settings coverage.runsettings --collect:"XPlat Code Coverage" --results-directory TestResults
powershell.exe -NoProfile -ExecutionPolicy Bypass -File .\Show-Coverage.ps1
```

XML-отчёт создаётся в `TestResults`; готовый отчёт последней проверки — `coverage-report.html`.

Запуск API:

```powershell
dotnet run --project .\BudgetApi
```

API доступен на `http://localhost:5080`. Остановка — `Ctrl+C`.

