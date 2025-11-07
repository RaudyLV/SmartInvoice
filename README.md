# 🧾 SmartInvoice

**SmartInvoice** es una API REST para gestión de facturación, productos, clientes y pagos. Desarrollada con .NET y siguiendo patrones modernos como **CQRS + MediatR** **Redis + paginación** y **Clean/Onion Architecture**.

---

## 📋 Tabla de Contenido

1. [Descripción](#-descripción)
2. [Características Actuales](#-características-actuales)
3. [Stack Tecnológico](#-stack-tecnológico)
4. [Requisitos Previos](#-requisitos-previos)
5. [Instalación y Configuración](#-instalación-y-configuración)
6. [Estructura del Proyecto](#-estructura-del-proyecto)
7. [Endpoints Principales](#-endpoints-principales)
8. [Reglas de Negocio](#-reglas-de-negocio)
9. [Roadmap y Mejoras Futuras](#-roadmap-y-mejoras-futuras)
10. [Contribuciones](#-contribuciones)

---

## 🎯 Descripción

SmartInvoice es un sistema de facturación backend que permite:
- ✅ Crear facturas con múltiples productos
- ✅ Gestionar inventario de productos
- ✅ Registrar pagos parciales o totales
- ✅ Consultar clientes y su historial de facturas
- ✅ Validaciones de negocio robustas

Pensado como proyecto educativo y base para sistemas de facturación reales.

---

## ✨ Características Actuales

- **CQRS + MediatR**: Separación clara entre Commands y Queries
- **AutoMapper**: Mapeo automático entre entidades y DTOs
- **FluentValidation**: Validación de entrada de datos
- **JWT Authentication**: Protección de endpoints con tokens
- **Generación automática de números de factura**: Formato `F000001`
- **Cálculo de impuestos**: Soporte para múltiples tasas de impuestos (ITBIS 18%)
- **Control de stock**: Actualización automática al crear facturas
- **Estados de pago**: Unpaid, PartiallyPaid, Paid
- **Manejo de errores personalizado**: Excepciones de negocio específicas
- **Cacheo con redis**: Cacheo de queries optimizado 

---

## 🛠 Stack Tecnológico

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET | 8.0 | Framework principal |
| ASP.NET Core | 8.0 | Web API |
| Entity Framework Core | 8.0 | ORM |
| SQL Server | 2022 | Base de datos |
| AutoMapper | 13.0 | Mapeo objeto-objeto |
| MediatR | 12.0 | CQRS pattern |
| FluentValidation | 11.0 | Validaciones |
| Swagger/OpenAPI | 3.0 | Documentación API |
| Redis Cache | 3.0.504 | Caching |

---

## 📦 Requisitos Previos

- **.NET SDK 8.0+** → [Descargar](https://dotnet.microsoft.com/download)
- **SQL Server** (LocalDB, Express o Developer)
- **Visual Studio 2022** o **VS Code** con extensión C#
- **Git**
- **Redis 3.0.504 64 bit**

---

### 2. Configurar la base de datos

Edita `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=SmartInvoiceDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "tu-clave-super-secreta-de-al-menos-32-caracteres",
    "Issuer": "SmartInvoice",
    "Audience": "SmartInvoiceUsers",
    "ExpireMinutes": 60 
  },
  "RedisSettings": {
    "Host": "localhost",
    "Port": 6379,
    "InstanceName": "SmartInvoice_API"
  }
}


```
### 3. Aplicar migraciones
```bash
dotnet ef migrations add InitialCreate --project backend/SmartInvoice.Infrastructure --startup-project backend/SmartInvoice.Api
dotnet ef database update --project backend/SmartInvoice.Infrastructure --startup-project backend/SmartInvoice.Api

```

### 4. Ejecutar la aplicación
```bash
dotnet watch run
```

La API estará disponible en: `https://localhost:3000` (o el puerto configurado)

Swagger UI: `https://localhost:3000/swagger`

Redis: `localhost:6379`


## 📁 Estructura del Proyecto
```
SmartInvoice/
├── backend/
│   ├── SmartInvoice.API/              # Capa de presentación (**Controllers**)
│   ├── SmartInvoice.Application/      # **Lógica de aplicación (CQRS)**
│   │   ├── Behaviors/                  # Pipelines (Caching behavior, etc)
│   │   ├── Commands/                  # Commands (**escritura**)
│   │   ├── Queries/                   # Queries (**lectura**)
│   │   ├── DTOs/                      # Data Transfer Objects
│   │   ├── Mappings/                  # Perfiles de AutoMapper
│   │   ├── **Common/**                # **Clases de ayuda/Utilidades (Helpers)**
│   │   └── **Specifications/**        # **Especificaciones de Ardalis (Queries y Paginación)**
│   ├── SmartInvoice.Domain/           # Entidades y lógica de negocio
│   │   └── Entities/                  # Modelos de dominio
│   │   
│   └── SmartInvoice.Infrastructure/   # Acceso a datos y servicios externos
│       ├── Data/                      # DbContext
│       ├── Repositories/              # (Opcional) Repositorios específicos
│       └── **Services/**              # **Implementación de lógica reutilizable (p. ej., GetById, GetAll)**
```

---

## 🌐 Endpoints Principales

### Autenticación
```http
POST /api/auth/register
POST /api/auth/login
```

### Productos
```http
GET    /api/products/search
GET    /api/products/{id}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
```

### Clientes
```http
GET    /api/clients
GET    /api/clients/search?q=raudy
POST   /api/clients
PUT    /api/clients/{id}
DELETE /api/clients/{id}
GET    /api/clients/{id}/invoices
```

### Facturas (invoices)
```http
POST   /api/invoices
GET    /api/invoices/search
UPDATE /api/invoices/{id}

```
### Payments (pagos)
```http
POST   /api/payments
GET    /api/payments/search
GET /api/payments/{id}
```

**Ejemplo: Crear factura**
```json
POST /api/invoices
{
  "clientId": 1,
  "items": [
    {
      "productId": 10,
      "quantity": 2,
    },
    {
      "productId": 15,
      "quantity": 1,
    }
  ]
}
```

**Respuesta**
```json
{
  "data": {
    "id": 1,
    "invoiceNumber": "F000001",
    "clientId": 1,
    "clientName": "Juan Pérez",
    "Status": "Issued",
    "createdAt": "2024-11-01T10:30:00Z",
    "dueDate": "2024-12-01T10:30:00Z",
    "InvoiceItems": [
      {
        "productId": 10,
        "productName": "Laptop Dell",
        "quantity": 2,
        "unitPrice": 1000.00,
        "taxRate": 0.18,
        "subtotal": 2000.00,
        "taxAmount": 360.00,
        "total": 2360.00
      }
    ],
    "subTotal": 2000.00,
    "taxTotal": 360.00,
    "discount": 0.00,
    "total": 2360.00
  },
  "message": "Invoice created successfully"
}
```

---

## 💼 Reglas de Negocio

### Productos
- ✅ El nombre del producto debe ser único
- ✅ TaxRate debe estar entre 0 y 1 (ej: 0.18 = 18%)
- ✅ Stock no puede ser negativo
- ✅ No se puede eliminar un producto usado en facturas

### Facturas
- ✅ Los productos deben existir y estar activos
- ✅ Debe haber stock suficiente
- ✅ El stock se reduce automáticamente al crear la factura
- ✅ Número de factura formato: `F000001`, `F000002`, etc.
- ✅ Descuento automático del 10% si subtotal > $1000
- ✅ No se puede eliminar una factura con pagos registrados

### Pagos
- ✅ El monto no puede exceder el balance pendiente
- ✅ Estado se actualiza automáticamente: Issued → PartiallyPaid → Paid
- ✅ Métodos de pago soportados: Cash, Card, Transfer


## 🗺️ Roadmap y Mejoras Futuras

### 🔜 Próximamente

- [ ] **Serilog para logging estructurado**
  - Logs en archivos y consola
  - Integración con Seq/Elasticsearch
  - Correlación de requests

---

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Por favor:

1. Fork el proyecto
2. Crea una branch para tu feature: `git checkout -b feature/nueva-funcionalidad`
3. Commit tus cambios: `git commit -m 'Add: nueva funcionalidad'`
4. Push a la branch: `git push origin feature/nueva-funcionalidad`
5. Abre un Pull Request

### Convenciones de commits
```
Add: nueva funcionalidad
Fix: corrección de bug
Update: cambio en funcionalidad existente
Refactor: mejora de código sin cambios funcionales
Docs: cambios en documentación
Test: añadir o modificar tests
```
---

## 👨‍💻 Autor

**Raudy Lara V**
- GitHub: [@RaudyLV](https://github.com/RaudyLV)
- LinkedIn: [Raudy Lara Valenzuela](https://www.linkedin.com/in/raudylara/)

---
