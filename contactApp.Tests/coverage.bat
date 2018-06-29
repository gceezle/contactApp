@echo off

dotnet clean
dotnet build /p:DebugType=Full
dotnet minicover instrument --workdir ../ --assemblies contactApp.Tests/**/bin/**/*.dll --sources contactApp/**/*.cs --exclude-sources contactApp/Migrations/**/*.cs --exclude-sources contactApp/*.cs --exclude-sources contactApp\Data\ContactAppDbContext.cs --exclude-sources contactApp\Controllers\HomeController.cs --exclude-sources contactApp\Data\DbInitializer.cs

dotnet minicover reset --workdir ../

dotnet test --no-build
dotnet minicover uninstrument --workdir ../
dotnet minicover htmlreport --workdir ../ --threshold 60
dotnet minicover report --workdir ../ --threshold 60