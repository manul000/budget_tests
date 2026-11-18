$ErrorActionPreference = 'Stop'
$file = Get-ChildItem -Path '.\TestResults' -Filter 'coverage.cobertura.xml' -Recurse | Sort-Object LastWriteTime -Descending | Select-Object -First 1
if (-not $file) { throw 'Сначала запустите dotnet test с параметром сбора покрытия из README.' }
[xml]$coverage = Get-Content -LiteralPath $file.FullName -Raw
$method = $coverage.SelectSingleNode("//class[@name='BudgetApi.Controllers.BudgetController']/methods/method[@name='Calculate']")
if (-not $method) { throw 'Покрытие метода Calculate не найдено.' }
Write-Output ('Calculate: lines={0:P0}, branches={1:P0}' -f [double]::Parse($method.GetAttribute('line-rate'), [Globalization.CultureInfo]::InvariantCulture), [double]::Parse($method.GetAttribute('branch-rate'), [Globalization.CultureInfo]::InvariantCulture))
Write-Output ('Отчёт: ' + $file.FullName)
