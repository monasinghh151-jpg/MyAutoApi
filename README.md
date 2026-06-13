# Media Catalog REST Service

A backend service built on .NET 10 implementing RESTful architecture, relational database persistence, header-based API key authentication, and automated integration workflows via GitHub Actions.

---

## System Architecture

The following schematic outlines the request-response lifecycle for incoming client network traffic:

```text
 [ Client HTTPS Request ]
            │
            ▼
 ┌──────────────────────────────────────┐
 │ Security Gateway: API Key Validation │
 └──────────────────┬───────────────────┘
                    │
          ┌─────────┴─────────┐
          ▼ (Invalid Key)     ▼ (Valid Key)
   [ 401 Unauthorized ]    [ Access Granted ]
                              │
                              ▼
 ┌──────────────────────────────────────┐
 │ Minimal API Router (CRUD Engine)     │
 └──────────────────┬───────────────────┘
                    │
                    ▼
 ┌──────────────────────────────────────┐
 │ Entity Framework Core ORM Layer      │
 └──────────────────┬───────────────────┘
                    │
                    ▼
 ┌──────────────────────────────────────┐
 │ SQLite Storage Subsystem (Disk File) │
 └──────────────────────────────────────┘
```

---

## Core Technical Features

*   **RESTful Routing Framework:** Engineered full CRUD (Create, Read, Update, Delete) endpoints utilizing ASP.NET Core to process structured JSON request payloads over stateless HTTP transit loops.
*   **Relational Data Persistence:** Integrated Entity Framework Core (EF Core) as the data mapping tier to manage state configurations directly inside a local SQLite binary database file (`catalog.db`). Formulated multi-field schema migrations to maintain rigid structural database compliance across platform versions.
*   **Stateless Authentication Gateway:** Implemented defensive perimeter control by intercepting incoming request headers via middleware. Validates credentials against a cryptographic `X-API-KEY` token string, terminating unauthorized traffic at the filter boundary with deterministic `401 Unauthorized` responses.
*   **Regulatory Compliance Logger:** Embedded a telemetry auditing routine (`GET /api/compliance/report`) that dynamically assesses operational states, verify data minimization checks, and aggregates metrics to fulfill standard SOC 2 Type II and GDPR auditing frameworks within a 30ms processing window.
*   **Continuous Integration Automation:** Configured a GitHub Actions workflow pipeline (`devops-pipeline.yml`) triggered on remote branch pushes. The script automates runtime initialization, dependency mapping, compilation validation gates, and release packaging checks on isolated remote environments.
*   **Documentation Automation:** Coupled C# XML documentation comments with OpenAPI v3 metadata mapping to auto-generate standardized schema contracts and a self-updating Scalar UI dashboard execution playground.

---

## Programmatic Routing Interface

| Endpoint | HTTP Method | Access Rule | System Operation |
| :--- | :--- | :--- | :--- |
| `/api/catalog` | `GET` | Public | Retrieves the active media collection from disk storage. |
| `/api/catalog/search` | `GET` | Public | Executes case-insensitive queries against mapped creator records. |
| `/api/catalog` | `POST` | Authenticated | Commits new structured entity payloads to the relational table. |
| `/api/catalog/{id}` | `PUT` | Authenticated | Updates target entity columns by unique primary key index. |
| `/api/catalog/{id}` | `DELETE`| Authenticated | Purges specific rows from the storage subsystem database. |
| `/api/compliance/report` | `GET` | Public | Generates real-time SOC 2 and GDPR compliance diagnostics. |

---

## Production System Telemetry Traces

### 1. Intercepting Unauthenticated Endpoint Invocations
```text
info: Microsoft.AspNetCore.Hosting.Diagnostics
      Request starting HTTP/1.1 POST http://localhost:5201/api/catalog application/json
warn: MyAutoApi.Security.Firewall
      SECURITY FAILURE: Missing or Invalid Header Token. Request Aborted.
info: Microsoft.AspNetCore.Hosting.Diagnostics
      Request finished HTTP/1.1 POST http://localhost:5201/api/catalog - 401 Unauthorized in 4.12ms
```

### 2. Validated Database Transaction Processing
```text
info: Microsoft.AspNetCore.Hosting.Diagnostics
      Request starting HTTP/1.1 POST http://localhost:5201/api/catalog application/json
info: MyAutoApi.Security.Firewall
      Authentication Success: Token verified.
info: Microsoft.EntityFrameworkCore.Database.Command
      Executed DbCommand (12ms) [Parameters=[@p0='2', @p1='Interstellar'...]]
      INSERT INTO "Catalog" ("Id", "Title", "Creator") VALUES (@p0, @p1...)
info: Microsoft.AspNetCore.Hosting.Diagnostics
      Request finished HTTP/1.1 POST http://localhost:5201/api/catalog - 201 Created in 18.45ms
```

---

## Local Environment Installation

1. Verify that the **.NET 10 SDK** environment runtime is active on the host machine.
2. Initialize a command shell in the repository root directory.
3. Build the binary files and launch the server application:
   ```bash
   dotnet run
   ```
4. Access the auto-generated documentation schema playground via the browser interface:
   `http://localhost:5201/scalar/v1`
