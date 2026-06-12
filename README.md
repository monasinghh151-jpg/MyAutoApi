# MyAutoApi

An automated ASP.NET Core Web API built with .NET 10 featuring autonomous OpenAPI spec compilation and a live interactive Scalar documentation workspace dashboard.

## 🛠️ Prerequisites
* [.NET SDK 10](https://microsoft.com)

## 🚀 Local Installation & Execution
1. Open your terminal in the repository root directory.
2. Run the application server:
   ```bash
   dotnet run
   ```

## 📊 Live Interactive Documentation API Portal
Once the application initializes locally, the codebase auto-compiles and exposes your developer dashboard workspace at:
* **Interactive Portal:** [http://localhost:5201/scalar/v1](http://localhost:5201/scalar/v1)
* **Raw OpenAPI Specification Spec:** [http://localhost:5201/openapi/v1.json](http://localhost:5201/openapi/v1.json)
