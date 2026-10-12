# GM — Girls Market

**Girls Market (GM)** is a full-stack marketplace web application that connects customers with talented women offering fashion, accessories, bags, gifts, and handmade services. It was developed as the final project of a Full-Stack development track.

---

## 1. Project Overview

GM allows customers to browse products and services, submit service requests, manage their account profile, and upload personal documents. Admin and Employee users can manage the product catalogue, services, and customer requests through a protected admin panel.

---

## 2. Main Features

### Public / Customer
- Responsive landing page with hero, product showcase, service listing, and contact form
- Product search with filters (keyword, category, min/max price)
- Browse services
- Contact/inquiry form backed by the database
- User registration and login
- Customer dashboard: view and update profile
- Customer service requests: submit, view own requests
- Document upload and management (authenticated)
- Protected routes redirect unauthenticated users to login

### Admin / Employee
- Products: create, update, delete (with image upload)
- Services: create, update, delete (with image upload)
- Customer requests: view all, update status
- Role-based access control (Customer = 0, Employee = 1, Admin = 2)
- Swagger/OpenAPI UI available in Development mode

---

## 3. Technologies Used

| Layer | Technology |
|---|---|
| Frontend | React 19, Vite 8, React Router DOM 7, Vanilla CSS |
| Backend | ASP.NET Core Web API (.NET 10) |
| Architecture | Clean Architecture (Domain / Application / Infrastructure / API) |
| Messaging | MediatR, FluentValidation |
| Database | Microsoft SQL Server |
| ORM | Entity Framework Core 10 |
| Auth | JWT Bearer tokens |
| File Storage | Local file system (wwwroot) |

---

## 4. Architecture Overview

```
GM.Domain          — Entities (Product, Service, User, CustomerRequest, Document, Client, Project)
                     Enums (UserRole, RequestStatus, ProjectStatus)
                     BaseEntity (Id, CreatedAt, UpdatedAt)

GM.Application     — MediatR Commands & Queries per feature
                     FluentValidation validators
                     Repository interfaces (IProductRepository, IUserRepository, …)
                     Service interfaces (IJwtService, IPasswordHasher, IFileStorageService, IDocumentStorageService)
                     ValidationBehavior pipeline

GM.Infrastructure  — EF Core GMDbContext with all entity configurations
                     Repository implementations
                     JwtService, PasswordHasher, FileStorageService, DocumentStorageService
                     EF Core migrations

GM.API             — ASP.NET Core Web API controllers
                     JWT Bearer authentication middleware
                     ExceptionHandlingMiddleware (400/401/404/409/500)
                     CORS policy for the React frontend (http://localhost:5173)
                     Swagger/OpenAPI (Development only)
                     Static file serving (product & service images)
```

---

## 5. Project Structure

```
GM-Girls-Market-/
├── Backend/
│   ├── GM.slnx
│   ├── GM.API/
│   │   ├── Controllers/        AuthController, ProductsController, ServicesController,
│   │   │                       CustomerRequestsController, DocumentsController, ContactController
│   │   ├── Middleware/         ExceptionHandlingMiddleware
│   │   ├── Models/             Request DTOs per feature
│   │   ├── appsettings.json
│   │   └── Program.cs
│   ├── GM.Application/
│   │   ├── Features/           Authentication, Products, Services, CustomerRequests,
│   │   │                       Documents, Clients, Projects, ContactInquiries
│   │   ├── Abstractions/       IProductRepository, IUserRepository, IJwtService, …
│   │   └── Behaviors/          ValidationBehavior
│   ├── GM.Domain/
│   │   ├── Entities/           Product, Service, User, CustomerRequest, Document, Client, Project, ProjectMember
│   │   ├── Enums/              UserRole, RequestStatus, ProjectStatus
│   │   └── Common/             BaseEntity
│   └── GM.Infrastructure/
│       ├── Persistence/        GMDbContext, entity configurations, repositories
│       ├── Migrations/         EF Core migration files
│       ├── Authentication/     JwtService
│       └── Services/           FileStorageService, DocumentStorageService, PasswordHasher
└── frontend/
    └── gm-client/
        ├── src/
        │   ├── api/            authApi.js, productApi.js, serviceApi.js,
        │   │                   requestApi.js, contactApi.js, documentApi.js, config.js
        │   ├── components/     ProtectedRoute.jsx
        │   └── pages/          Home, Login, Register, CustomerDashboard, CustomerRequests,
        │                       AdminProducts, AddProduct, EditProduct,
        │                       AdminServices, AddService, EditService,
        │                       AdminRequests, EditRequest
        ├── public/images/      logo.jpg and sample product images
        └── package.json
```

---

## 6. Prerequisites

| Tool | Version |
|---|---|
| .NET SDK | 10.0 or later |
| Node.js | 18+ |
| npm | 9+ |
| SQL Server | 2019+ (or SQL Server Express / LocalDB) |
| dotnet-ef CLI | 10.0.x (`dotnet tool install -g dotnet-ef`) |

---

## 7. Installation Instructions

### Clone the repository

```bash
git clone https://github.com/mariem-sayed145/GM-Girls-Market-.git
cd GM-Girls-Market-
```

### Restore backend dependencies

```bash
dotnet restore Backend/GM.slnx
```

### Install frontend dependencies

```bash
cd frontend/gm-client
npm install
cd ../..
```

---

## 8. Environment Variables

### Backend

All configuration is in [`Backend/GM.API/appsettings.json`](Backend/GM.API/appsettings.json).

For local development, override values in `appsettings.Development.json` (this file is in `.gitignore`).

| Key | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `Jwt:Key` | Secret key used to sign JWT tokens (change in production) |
| `Jwt:Issuer` | Token issuer — default `GM.API` |
| `Jwt:Audience` | Token audience — default `GM.Client` |
| `Jwt:ExpirationMinutes` | Token lifetime in minutes — default `60` |

**Default connection string (Windows Auth / trusted connection):**
```json
"Server=localhost;Database=GMDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

### Frontend

The frontend API base URL is configured in [`frontend/gm-client/src/api/config.js`](frontend/gm-client/src/api/config.js):

```js
export const API_BASE_URL = 'http://localhost:5262'
```

A `.env.example` is provided at `frontend/gm-client/.env.example` for reference.

> **Never commit real credentials.** The `appsettings.Development.json` file is `.gitignore`d.

---

## 9. SQL Server Database Setup

1. Ensure SQL Server is running locally.
2. The connection string in `appsettings.json` uses Windows Authentication (`Trusted_Connection=True`). No username/password is required for a local default instance.
3. The database `GMDb` will be created automatically when you apply migrations.

To use SQL Server Express named instance, update the connection string:
```
Server=localhost\SQLEXPRESS;Database=GMDb;Trusted_Connection=True;TrustServerCertificate=True;
```

---

## 10. EF Core Migration Instructions

All migrations are already committed. Apply them to create (or update) the database:

```bash
dotnet ef database update \
  --project Backend/GM.Infrastructure \
  --startup-project Backend/GM.API
```

**Current migrations:**
| Migration | Description |
|---|---|
| `20260910121745_InitialCreate` | ContactInquiries table |
| `20260913190844_AddProductsTable` | Products table |
| `20260918183203_AddUsersTable` | Users table with unique email index |
| `20261012184547_AddClientsProjectsDocumentsCustomerRequests` | Clients, Projects, ProjectMembers, Services, Documents, CustomerRequests tables; Role column on Users |

> **Do not run `dotnet ef database drop`** — it will destroy existing data.

---

## 11. How to Run the ASP.NET Core Backend

```bash
cd Backend
dotnet run --project GM.API
```

Or from the repository root:

```bash
dotnet run --project Backend/GM.API
```

The API starts on:
- `http://localhost:5262` (HTTP profile)
- `https://localhost:7184` (HTTPS profile)

To use the HTTP profile explicitly:

```bash
dotnet run --project Backend/GM.API --launch-profile http
```

---

## 12. How to Run the React Frontend

```bash
cd frontend/gm-client
npm run dev
```

The frontend runs on **http://localhost:5173** by default.

The CORS policy on the backend allows requests from `http://localhost:5173`.

---

## 13. API and Swagger Information

Swagger UI is available when the backend is running in Development mode:

```
http://localhost:5262/swagger
```

### API Endpoints Summary

| Controller | Endpoint | Auth Required | Description |
|---|---|---|---|
| **Auth** | `POST /api/Auth/register` | No | Register a new user |
| | `POST /api/Auth/login` | No | Log in, receive JWT |
| | `GET /api/Auth/me` | Yes | Get current user profile |
| | `PUT /api/Auth/me` | Yes | Update current user profile |
| **Products** | `GET /api/Products` | No | List/search products |
| | `GET /api/Products/{id}` | No | Get product by ID |
| | `POST /api/Products` | Employee/Admin | Create product (multipart/form-data) |
| | `PUT /api/Products/{id}` | Employee/Admin | Update product |
| | `DELETE /api/Products/{id}` | Admin only | Delete product |
| **Services** | `GET /api/Services` | No | List all services |
| | `GET /api/Services/{id}` | No | Get service by ID |
| | `POST /api/Services` | Employee/Admin | Create service (multipart/form-data) |
| | `PUT /api/Services/{id}` | Employee/Admin | Update service |
| | `DELETE /api/Services/{id}` | Admin only | Delete service |
| **CustomerRequests** | `POST /api/CustomerRequests` | Yes | Submit a service request |
| | `GET /api/CustomerRequests/my` | Yes | Get own requests |
| | `GET /api/CustomerRequests` | Employee/Admin | Get all requests |
| | `GET /api/CustomerRequests/{id}` | Yes (owner) | Get own request by ID |
| | `GET /api/CustomerRequests/admin/{id}` | Employee/Admin | Get any request by ID |
| | `PUT /api/CustomerRequests/{id}/status` | Employee/Admin | Update request status |
| **Documents** | `POST /api/Documents` | Yes | Upload a document |
| | `GET /api/Documents/my` | Yes | List own documents |
| | `GET /api/Documents/{id}/download` | Yes | Download own document |
| | `DELETE /api/Documents/{id}` | Yes (owner) | Delete own document |
| **Contact** | `POST /api/Contact` | No | Submit contact inquiry |

---

## 14. Authentication Overview

- **Mechanism**: JWT Bearer tokens stored in `localStorage` under the key `gm_token`.
- **Registration**: `POST /api/Auth/register` — hashes password with BCrypt (PBKDF2), creates user with `Customer` role.
- **Login**: `POST /api/Auth/login` — verifies credentials, returns JWT + user info.
- **Token**: Contains `NameIdentifier` (user ID), `Email`, `Name`, `Role` claims.
- **Protected routes (frontend)**: `ProtectedRoute` component checks `localStorage` for the token and user role before rendering.
- **Protected endpoints (backend)**: `[Authorize]` and `[Authorize(Roles = "Employee,Admin")]` attributes enforced by ASP.NET Core JWT middleware.
- **Logout**: Client clears `gm_token` and `gm_user` from `localStorage`.
- **Role values**: `Customer = 0`, `Employee = 1`, `Admin = 2`.

---

## 15. How to Run Tests

There are currently no automated test projects in the repository. Manual testing can be performed via:

- **Swagger UI**: `http://localhost:5262/swagger` (run backend in Development mode)
- **Browser**: Navigate to `http://localhost:5173` with both servers running
- **API client**: Any HTTP client (e.g., Postman) using the endpoints listed in Section 13

---

## 16. Common Issues and Troubleshooting

### Backend does not start
- Ensure SQL Server is running.
- Verify the connection string in `appsettings.json` matches your instance name.
- Run `dotnet ef database update` before starting the API.
- Check the `Jwt:Key` value is present in configuration.

### "Cannot connect to database"
- Check SQL Server service is running (`services.msc` → SQL Server).
- For named instances, use `Server=localhost\SQLEXPRESS` in the connection string.
- Ensure `TrustServerCertificate=True` is set in the connection string.

### Frontend shows "Failed to load products" (network error)
- Confirm the backend is running on `http://localhost:5262`.
- Check browser console for CORS errors — ensure the frontend is accessed at `http://localhost:5173`.
- The CORS policy in `Program.cs` allows only `http://localhost:5173`.

### JWT token rejected (401 Unauthorized)
- Log out and log in again to get a fresh token.
- The default token lifetime is 60 minutes (`Jwt:ExpirationMinutes`).

### Product/Service images not loading
- The backend serves images from `Backend/GM.API/wwwroot/images/products/`.
- Static files are only served when the `wwwroot` folder exists. Create it if missing.
- Image URLs from the API follow the pattern `/images/products/{guid}.ext`.

### Migration already exists error
- Do not run `dotnet ef migrations add` again without first checking `dotnet ef migrations list`.
- All required migrations are already committed.

### "Role column missing" on Users table
- This was added in the `AddClientsProjectsDocumentsCustomerRequests` migration.
- Run `dotnet ef database update` to apply it.

---

## Running Both Servers

Open two terminals:

**Terminal 1 — Backend:**
```bash
dotnet run --project Backend/GM.API
```

**Terminal 2 — Frontend:**
```bash
cd frontend/gm-client
npm run dev
```

Then open **http://localhost:5173** in your browser.

---

© 2026 Girls Market — Made with love.
