# MiRenta API

API REST para la gestion de rentas, propietarios, propiedades e inquilinos.

El proyecto esta construido con .NET 8, Entity Framework Core, SQL Server, autenticacion JWT y Swagger para documentacion interactiva en ambiente de desarrollo.

## Estructura del proyecto

```text
MiRenta.API/
├── MiRenta.API/              # Web API, controladores, middleware y configuracion HTTP
├── MiRenta.Application/      # Casos de uso, servicios, DTOs e interfaces
├── MiRenta.Domain/           # Entidades de dominio
├── MiRenta.Infrastructure/   # Persistencia, EF Core, seguridad y migraciones
└── MiRenta.API.slnx          # Solucion
```

## Requisitos

- .NET SDK 8
- SQL Server o SQL Server Express
- Entity Framework Core CLI

Para instalar la CLI de EF Core:

```powershell
dotnet tool install --global dotnet-ef
```

Si ya la tienes instalada:

```powershell
dotnet tool update --global dotnet-ef
```

## Configuracion local

El archivo `appsettings.json` contiene valores locales como la cadena de conexion y la llave JWT. No debe usarse para secretos reales en repositorios compartidos.

Crea o ajusta `MiRenta.API/MiRenta.API/appsettings.json` con una estructura similar:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MiRentaDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "change-this-key-for-local-development",
    "Issuer": "MiRenta",
    "Audience": "MiRenta",
    "ExpiresInMinutes": 60
  }
}
```

Tambien puedes sobrescribir configuracion con variables de entorno usando el formato de ASP.NET Core:

```powershell
$env:ConnectionStrings__DefaultConnection="Server=localhost\SQLEXPRESS;Database=MiRentaDb;Trusted_Connection=True;TrustServerCertificate=True;"
$env:Jwt__Key="change-this-key-for-local-development"
$env:Jwt__Issuer="MiRenta"
$env:Jwt__Audience="MiRenta"
$env:Jwt__ExpiresInMinutes="60"
```

## Base de datos

Desde la raiz del repositorio:

```powershell
dotnet restore MiRenta.API/MiRenta.API.slnx
dotnet ef database update --project MiRenta.API/MiRenta.Infrastructure --startup-project MiRenta.API/MiRenta.API
```

Para crear una nueva migracion:

```powershell
dotnet ef migrations add NombreDeLaMigracion --project MiRenta.API/MiRenta.Infrastructure --startup-project MiRenta.API/MiRenta.API --output-dir Migrations
```

## Ejecutar la API

Desde la raiz del repositorio:

```powershell
dotnet run --project MiRenta.API/MiRenta.API
```

Perfiles disponibles:

- HTTP: `http://localhost:5162`
- HTTPS: `https://localhost:7095`
- Swagger: `/swagger`

Ejemplo:

```text
https://localhost:7095/swagger
```

## Endpoints principales

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/owners`
- `GET /api/properties`
- `GET /api/tenants`

Los endpoints de `owners`, `properties` y `tenants` requieren autenticacion con token Bearer.

## Desarrollo

Compilar la solucion:

```powershell
dotnet build MiRenta.API/MiRenta.API.slnx
```

Revisar el estado de migraciones:

```powershell
dotnet ef migrations list --project MiRenta.API/MiRenta.Infrastructure --startup-project MiRenta.API/MiRenta.API
```

## Notas de seguridad

- No subas llaves JWT reales, cadenas de conexion productivas ni archivos `.env`.
- Usa `appsettings.Development.json`, variables de entorno o user-secrets para configuracion local.
- Si `appsettings.json` ya fue versionado y quieres dejarlo solo local, ejecuta:

```powershell
git rm --cached MiRenta.API/MiRenta.API/appsettings.json
```
