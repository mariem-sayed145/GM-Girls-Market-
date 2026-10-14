# GM — Girls Market

**Girls Market (GM)** is a full-stack marketplace web application that connects customers with talented women offering fashion, accessories, bags, gifts, and handmade services.

---

## 1. Project Overview

GM allows customers to browse handcrafted products and services, submit and track custom service requests, manage their account profile, and securely upload personal documents. Admin and Employee users manage the product catalogue, services, and customer requests through a role-protected administrative panel.

---

## 2. Main Features

### Public / Customer
- **Showcase & Discovery**: Responsive landing page with hero banner, product catalogue, service showcase, and interactive contact form.
- **Search & Filtering**: Search products by keyword, category, and minimum/maximum price bounds.
- **Service Requests**: Logged-in customers submit service requests with optional preferred dates and track status updates (Pending, InProgress, Completed, Cancelled).
- **Customer Dashboard**: View and update profile details (Full Name, Email).
- **Document Management**: Secure upload, listing, and download of documents with MIME-type and size validation (up to 10 MB).
- **Inquiry Handling**: Database-persisted contact inquiries.
- **Protected Routing**: Automatic redirect of unauthenticated visitors to login.

### Admin & Staff Management
- **Product Management**: Create, edit, and delete products with image upload and validation.
- **Service Management**: Create, edit, and delete service offerings with image upload.
- **Customer Request Workflow**: Inspect all incoming customer requests and transition request statuses.
- **Role-Based Access Control**:
  - `Customer` (0): Browse, request services, manage profile & own documents.
  - `Employee` (1): Manage products, services, and update request progress.
  - `Admin` (2): Full administrative authority including deletion of products and services.
- **Interactive Documentation**: Swagger/OpenAPI documentation in Development mode.

---

## 3. Technologies Used

| Layer | Technology |
|---|---|
| **Frontend** | React 19, Vite 8, React Router DOM 7, Vanilla CSS |
| **Backend** | ASP.NET Core Web API (.NET 10) |
| **Architecture** | Clean Architecture (Domain / Application / Infrastructure / API / Tests) |
| **Design Patterns & Tools** | CQRS with MediatR, FluentValidation pipeline behaviors |
| **Database** | Microsoft SQL Server |
| **ORM** | Entity Framework Core 10 |
| **Authentication** | JWT Bearer Tokens, ASP.NET Core Identity PasswordHasher (PBKDF2-HMAC-SHA256) |
| **Testing** | xUnit, .NET Test SDK |
| **Static Assets** | Local physical file storage via ASP.NET Core `wwwroot` |

---

## 4. Architecture Overview

The solution adheres strictly to Clean Architecture principles:

```
GM.Domain
  ├── Entities: Product, Service, User, CustomerRequest, Document, Client, Project, ProjectMember
  ├── Enums: UserRole, RequestStatus, ProjectStatus
  └── Common: BaseEntity (Id, CreatedAt, UpdatedAt)

GM.Application
  ├── Features: Commands, Queries, DTOs, and Validators per feature
  ├── Abstractions: Repository and Service interfaces
  └── Behaviors: ValidationBehavior pipeline for MediatR

GM.Infrastructure
  ├── Persistence: GMDbContext, EF Core Configurations, Repositories
  ├── Migrations: EF Core Schema Migrations
  └── Services: JwtService, PasswordHasher, FileStorageService, DocumentStorageService

GM.API
  ├── Controllers: AuthController, ProductsController, ServicesController, CustomerRequestsController, DocumentsController, ContactController
  ├── Middleware: ExceptionHandlingMiddleware (structured 400/401/404/409/500 responses)
  └── Configuration: Program.cs, configurable CORS, JWT authentication, Swagger

GM.Tests
  └── xUnit test suite for Domain entities, FluentValidation validators, and Security components
```

---

## 5. Project Structure

```
GM-Girls-Market-/
├── Backend/
│   ├── GM.slnx
│   ├── GM.API/
│   │   ├── Controllers/             API Controllers
│   │   ├── Middleware/              ExceptionHandlingMiddleware
│   │   ├── Models/                  Request/Response DTO models
│   │   ├── wwwroot/                 Static assets and image storage
│   │   ├── appsettings.json         Default configuration
│   │   ├── appsettings.Production.json.example
│   │   └── Program.cs               Application entry point
│   ├── GM.Application/
│   │   ├── Features/                Feature slices (CQRS + FluentValidation)
│   │   ├── Abstractions/            Interfaces for persistence and services
│   │   └── Behaviors/               MediatR Pipeline Behaviors
│   ├── GM.Domain/
│   │   ├── Entities/                Core domain models
│   │   ├── Enums/                   Domain enumerations
│   │   └── Common/                  BaseEntity
│   ├── GM.Infrastructure/
│   │   ├── Persistence/             GMDbContext, Configurations, Repositories
│   │   ├── Migrations/              EF Core database migrations
│   │   ├── Authentication/          JwtService
│   │   └── Services/                File & Document storage, PasswordHasher
│   └── GM.Tests/
│       └── UnitTest1.cs             Automated unit tests
└── frontend/
    └── gm-client/
        ├── public/                  Static assets, images, and _redirects
        ├── src/
        │   ├── api/                 API clients with dynamic base URL support
        │   ├── components/          ProtectedRoute and reusable components
        │   ├── pages/               Page components (Home, Admin, Customer, Auth)
        │   ├── App.jsx              Router configuration
        │   └── App.css              Design system and styles
        ├── .env.example             Frontend environment template
        ├── package.json
        └── vite.config.js
```

---

## 6. Prerequisites

| Tool | Recommended Version |
|---|---|
| **.NET SDK** | 10.0 or later |
| **Node.js** | 18+ or 20+ LTS |
| **npm** | 9+ |
| **Microsoft SQL Server** | 2019, 2022, or SQL Server Express / LocalDB |
| **dotnet-ef CLI** | 10.0.x (`dotnet tool install -g dotnet-ef`) |

---

## 7. Installation Instructions

### 1. Clone the repository
```bash
git clone https://github.com/mariem-sayed145/GM-Girls-Market-.git
cd GM-Girls-Market-
```

### 2. Restore backend dependencies
```bash
dotnet restore Backend/GM.slnx
```

### 3. Install frontend dependencies
```bash
cd frontend/gm-client
npm install
cd ../..
```

---

## 8. Environment Variables

### Backend Configuration

Configuration is loaded from [`Backend/GM.API/appsettings.json`](Backend/GM.API/appsettings.json) or environment variables:

| Setting Key | Environment Variable Equivalent | Description |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | `ConnectionStrings__DefaultConnection` | SQL Server connection string |
| `Jwt:Key` | `Jwt__Key` | Secret key used for signing JWT tokens (min 256 bits) |
| `Jwt:Issuer` | `Jwt__Issuer` | Token issuer (default: `GM.API`) |
| `Jwt:Audience` | `Jwt__Audience` | Token audience (default: `GM.Client`) |
| `Jwt:ExpirationMinutes` | `Jwt__ExpirationMinutes` | Token validity period in minutes (default: `60`) |
| `Cors:AllowedOrigins` | `Cors__AllowedOrigins__0`, etc. | Allowed frontend origins (array) |

A production template is available in [`Backend/GM.API/appsettings.Production.json.example`](Backend/GM.API/appsettings.Production.json.example).

### Frontend Configuration

Copy the example file to `.env`:
```bash
cd frontend/gm-client
cp .env.example .env
```

| Variable | Description | Default |
|---|---|---|
| `VITE_API_BASE_URL` | Base URL of the ASP.NET Core API without trailing slash | `http://localhost:5262` |

---

## 9. SQL Server Setup

1. Verify Microsoft SQL Server is running:
   ```powershell
   Get-Service -Name "*MSSQL*"
   ```
2. The default connection string uses Windows Authentication (`Trusted_Connection=True`):
   ```json
   "Server=localhost;Database=GMDb;Trusted_Connection=True;TrustServerCertificate=True;"
   ```
3. If using a named SQL Server Express instance:
   ```json
   "Server=localhost\\SQLEXPRESS;Database=GMDb;Trusted_Connection=True;TrustServerCertificate=True;"
   ```

---

## 10. Entity Framework Core Migrations

All database schema migrations are committed in the repository. To create or update the database schema:

```bash
dotnet ef database update --project Backend/GM.Infrastructure --startup-project Backend/GM.API
```

### Committed Migrations
1. `20260910121745_InitialCreate` — `ContactInquiries` table.
2. `20260913190844_AddProductsTable` — `Products` table.
3. `20260918183203_AddUsersTable` — `Users` table with unique email index.
4. `20261012184547_AddClientsProjectsDocumentsCustomerRequests` — `Clients`, `Projects`, `ProjectMembers`, `Services`, `Documents`, and `CustomerRequests` tables; added `Role` column to `Users`.

> **Important**: Never run `database drop` in shared or production environments.

---

## 11. Running the Backend

From the repository root:
```bash
dotnet run --project Backend/GM.API
```

By default, the backend listens on:
- HTTP: `http://localhost:5262`
- HTTPS: `https://localhost:7184`

---

## 12. Running the Frontend

From the repository root:
```bash
cd frontend/gm-client
npm run dev
```

The frontend launches at **http://localhost:5173**.

---

## 13. API Endpoints

Interactive Swagger documentation is available in Development mode at `http://localhost:5262/swagger`.

### Key Endpoints:

| Method | Endpoint | Authorization | Description |
|---|---|---|---|
| `POST` | `/api/Auth/register` | Anonymous | Register customer account |
| `POST` | `/api/Auth/login` | Anonymous | Authenticate & retrieve JWT |
| `GET` | `/api/Auth/me` | Authenticated | Fetch current profile |
| `PUT` | `/api/Auth/me` | Authenticated | Update current profile |
| `GET` | `/api/Products` | Anonymous | List/filter products |
| `GET` | `/api/Products/{id}` | Anonymous | Get product by ID |
| `POST` | `/api/Products` | Employee, Admin | Add new product (multipart/form-data) |
| `PUT` | `/api/Products/{id}` | Employee, Admin | Update product (multipart/form-data) |
| `DELETE` | `/api/Products/{id}` | Admin | Delete product |
| `GET` | `/api/Services` | Anonymous | List services |
| `GET` | `/api/Services/{id}` | Anonymous | Get service by ID |
| `POST` | `/api/Services` | Employee, Admin | Create service (multipart/form-data) |
| `PUT` | `/api/Services/{id}` | Employee, Admin | Update service (multipart/form-data) |
| `DELETE` | `/api/Services/{id}` | Admin | Delete service |
| `POST` | `/api/CustomerRequests` | Authenticated | Submit service request |
| `GET` | `/api/CustomerRequests/my` | Authenticated | List own service requests |
| `GET` | `/api/CustomerRequests` | Employee, Admin | List all requests |
| `GET` | `/api/CustomerRequests/{id}` | Authenticated (Owner) | Get own request details |
| `GET` | `/api/CustomerRequests/admin/{id}` | Employee, Admin | Get any request details |
| `PUT` | `/api/CustomerRequests/{id}/status` | Employee, Admin | Update request status (0-3) |
| `POST` | `/api/Documents` | Authenticated | Upload document (PDF, DOCX, JPG, etc.) |
| `GET` | `/api/Documents/my` | Authenticated | List own documents |
| `GET` | `/api/Documents/{id}/download` | Authenticated (Owner) | Secure document stream |
| `DELETE` | `/api/Documents/{id}` | Authenticated (Owner) | Delete document |
| `POST` | `/api/Contact` | Anonymous | Submit contact inquiry |

---

## 14. Authentication and Authorization

- **Authentication**: JWT Bearer tokens passed via the `Authorization: Bearer <token>` HTTP header.
- **Password Security**: Managed using ASP.NET Core Identity's PBKDF2 implementation with unique salt hashing.
- **Client Storage**: Tokens are stored in browser `localStorage` under `gm_token`. User metadata is stored under `gm_user`.
- **RBAC Roles**:
  - `Customer` (`0`): default role on self-registration.
  - `Employee` (`1`): elevated rights to manage catalog and customer requests.
  - `Admin` (`2`): highest authority with deletion rights.
- **Backend Protection**: Endpoints enforce role checks independently (`[Authorize(Roles = "Employee,Admin")]`).

---

## 15. Testing

### Run Backend Automated Tests
```bash
dotnet test Backend/GM.slnx
```
Runs the xUnit test suite in `GM.Tests` verifying:
- User entity initialization and role assignment
- CustomerRequest lifecycle and status transitions
- Input validation (Registration, Products, Services, Requests)
- PasswordHasher hashing and verification algorithms

### Run Frontend Linting & Checks
```bash
cd frontend/gm-client
npm run lint
```
Runs `oxlint` with 0 warnings and 0 errors.

---

## 16. Production Build

### Frontend Production Bundle
```bash
cd frontend/gm-client
npm run build
```
Generates optimized static assets in `frontend/gm-client/dist/`.

### Backend Publication
```bash
dotnet publish Backend/GM.API/GM.API.csproj -c Release -o Backend/GM.API/bin/Release/net10.0/publish
```
Produces self-contained or framework-dependent production binaries in the output directory.

---

## 17. Deployment Considerations

### 1. Database Hosting (SQL Server)
- Local SQL Server (`localhost`) is only accessible on your development machine.
- For cloud deployment, provision a managed SQL Server instance (e.g. Azure SQL Database, AWS RDS for SQL Server, or a hosted SQL Server container/VM).
- Set `ConnectionStrings__DefaultConnection` in the hosting environment variables.

### 2. Secrets Management
- Set `Jwt__Key` in your hosting platform's Secret Manager with a high-entropy string (e.g. 64-character hex or base64).
- Do not commit production connection strings or JWT keys to source control.

### 3. CORS Configuration
- In production, set `Cors__AllowedOrigins__0` to your actual frontend domain (e.g. `https://girlsmarket.com`).

### 4. SPA Client Routing
- When deploying the React frontend on Netlify, Cloudflare Pages, or Vercel, client routes require rewrite fallback to `/index.html`.
- A [`public/_redirects`](frontend/gm-client/public/_redirects) file is included for automatic SPA routing compatibility.

---

## 18. Common Issues and Troubleshooting

### Backend database connection fails on startup
- Ensure SQL Server is running (`Get-Service MSSQLSERVER`).
- Confirm database migrations have been applied (`dotnet ef database update --project Backend/GM.Infrastructure --startup-project Backend/GM.API`).
- Ensure `TrustServerCertificate=True` is enabled when connecting with self-signed SSL certificates.

### CORS errors in the browser console
- Verify that `Cors:AllowedOrigins` in `appsettings.json` or the environment variable contains your frontend origin.
- For local development, `http://localhost:5173` is included by default.

### Uploaded images return 404
- Verify that `Backend/GM.API/wwwroot/` exists. ASP.NET Core requires the WebRoot directory to exist at application startup to register static file handlers.
- The repository includes `wwwroot/.gitkeep` to preserve the folder structure.

### 401 Unauthorized errors on API requests
- Check if the JWT has expired (default: 60 minutes).
- Sign in again to refresh the client token in `localStorage`.

---

© 2026 Girls Market — Empowering women through commerce.
