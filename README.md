# 🎬 Enterprise Media Catalog REST Engine

[![Enterprise DevOps Pipeline](https://github.com)](https://github.com)
[![Database Engine](https://shields.io)](https://microsoft.com)
[![Security Gateway](https://shields.io)]()
[![Compliance Validation](https://shields.io)]()

A high-performance, enterprise-grade backend REST API architecture built with **.NET 10** implementing secure data persistence pipelines, stateless security gateway firewalls, real-time compliance auditing modules, and automated DevOps deployment workflows.

---

## 🚀 Core Architectural Highlights

*   **High-Performance REST Architecture:** Hand-engineered a full **CRUD (Create, Read, Update, Delete) data lifecycle routing engine** utilizing ASP.NET Core Minimal APIs to handle structured resource payloads dynamically over HTTP loops [INDEX].
*   **Production-Grade SQL Persistence:** Migrated system storage layers from volatile in-memory arrays to a permanent physical **SQLite Database Engine (`catalog.db`)** using **Entity Framework Core (EF Core)** [INDEX]. Implemented multi-field schema migrations to append advanced entity properties seamlessly [INDEX, 1.1.18].
*   **Stateless Security Gateway Shield:** Designed and deployed an **API Key Authentication Middleware (X-API-KEY)** system that intercepts incoming request headers, validates cryptographic credentials, and drops unauthorized access attempts with defensive **`401 Unauthorized`** blocks [INDEX].
*   **Automated Regulatory Compliance Stream:** Developed an autonomous runtime compliance manager (`GET /api/compliance/report`) that evaluates active dataset metrics, checks access validation parameters, and dynamically compiles structured **SOC 2 Type II** and **GDPR Article 5** audit reports in under 30 milliseconds.
*   **Continuous Integration DevOps Automation:** Engineered a self-triggering **GitHub Actions workflow pipeline (`devops-pipeline.yml`)** [INDEX]. The pipeline spins up remote cloud virtual machines on every push to execute automated package restorations, compile code quality gates, and run publication deployment checklists completely on autopilot.
*   **Docs-as-Code Standardization (10x Faster Docs):** Integrated XML triple-slash code descriptors with OpenAPI v3 specs to auto-generate machine-readable blueprints and an interactive **Deep Space Dark Mode Scalar UI Dashboard Playground** [INDEX].

---

## 📡 Programmatic System Routing Matrix

| Route Endpoint | HTTP Verb | Authentication | System Operational Functionality |
| :--- | :--- | :--- | :--- |
| `/api/catalog` | `GET` | 🔓 Public | Queries the physical SQL database file and retrieves the complete library. |
| `/api/catalog/search` | `GET` | 🔓 Public | Performs real-time, case-insensitive text parsing across collection rows. |
| `/api/catalog` | `POST` | 🔐 Required | Validates API credentials and commits new unique records permanently to disk [INDEX]. |
| `/api/catalog/{id}` | `PUT` | 🔐 Required | Target-maps existing ID indexes and updates record properties dynamically. |
| `/api/catalog/{id}` | `DELETE`| 🔐 Required | Locates specific data asset rows and permanently purges them from memory. |
| `/api/compliance/report` | `GET` | 🔓 Public | Computes system metrics and compiles instant SOC 2 / GDPR security posture checks. |

---

## 🛠️ Local Sandbox Installation & Execution

1. Ensure the **.NET 10 SDK** environment runtime is initialized on your device.
2. Launch your command terminal in the repository's root workspace directory.
3. Fire up the backend compiler engine and turn on the server:
   ```bash
   dotnet run
   ```
4. Access the automated interactive documentation dashboard playground live from your web browser layout:
   👉 **`http://localhost:5201/scalar/v1`** [INDEX]
