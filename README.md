# Girls Market (GM)

**Girls Market** is a full-stack marketplace web application that connects customers with talented women offering fashion, accessories, bags, gifts, and handmade products.

The project was developed as part of the **VOLTIX Internship Task** and focuses on building a real frontend-to-backend flow using a clean, maintainable, and scalable backend architecture.

---

## Features

### Customer Experience

* Modern responsive landing page
* Product showcase
* Product categories
* Seller call-to-action section
* Contact & Inquiry form
* Frontend connected to a real Backend API

### Product Management

* Product listing
* Get product by ID
* Create products
* Update products
* Delete products
* Product categories
* Product image upload
* Product images stored on the backend
* Protected product management endpoints

### Authentication & Authorization

* User registration
* User login
* Secure password hashing
* JWT-based authentication
* Protected API endpoints
* Protected frontend routes
* Current authenticated user endpoint
* Logout functionality
* Authentication token handling on frontend

### Backend

* Server-side validation
* Invalid requests return proper error responses
* Valid inquiries are stored in SQL Server
* Products are stored in SQL Server
* Users are stored in SQL Server
* Clean Architecture
* MediatR request/handler pattern
* FluentValidation
* Entity Framework Core
* Repository Pattern
* Dependency Injection
* Global exception handling
* Swagger API documentation
* CORS configuration
* JWT authentication

---

# Tech Stack

## Frontend

* React
* Vite
* JavaScript
* HTML
* CSS
* React Router
* Fetch API
* FormData
* Local Storage for authentication state

## Backend

* .NET 10
* ASP.NET Core Web API
* Clean Architecture
* MediatR
* FluentValidation
* Entity Framework Core
* JWT Authentication
* Repository Pattern
* Dependency Injection

## Database

* SQL Server
* Entity Framework Core Migrations

## Development Tools

* Visual Studio / VS Code
* Swagger
* Git
* GitHub

---

# Project Structure

```text
GM (Girls Market)
│
├── frontend
│   └── gm-client
│       ├── public
│       ├── src
│       │   ├── api
│       │   │   ├── authApi.js
│       │   │   └── productApi.js
│       │   │
│       │   ├── assets
│       │   │
│       │   ├── components
│       │   │   └── ProtectedRoute.jsx
│       │   │
│       │   ├── pages
│       │   │   ├── Home.jsx
│       │   │   ├── Login.jsx
│       │   │   ├── Register.jsx
│       │   │   ├── AdminProducts.jsx
│       │   │   ├── AddProduct.jsx
│       │   │   └── EditProduct.jsx
│       │   │
│       │   ├── App.jsx
│       │   ├── App.css
│       │   ├── index.css
│       │   └── main.jsx
│       │
│       ├── package.json
│       └── vite.config.js
│
└── Backend
    ├── GM.API
    │   ├── Controllers
    │   ├── Middleware
    │   ├── Models
    │   ├── wwwroot
    │   │   └── images
    │   │       └── products
    │   └── Program.cs
    │
    ├── GM.Application
    │   ├── Abstractions
    │   ├── Behaviors
    │   ├── Features
    │   └── DependencyInjection.cs
    │
    ├── GM.Domain
    │   ├── Common
    │   └── Entities
    │
    └── GM.Infrastructure
        ├── Authentication
        ├── Persistence
        │   ├── Context
        │   ├── Configurations
        │   └── Repositories
        ├── Services
        └── DependencyInjection.cs
```

---

# Backend Architecture

The backend follows **Clean Architecture**, separating business logic, application logic, infrastructure concerns, and API responsibilities.

```text
                 GM.API
                   │
                   ▼
          GM.Infrastructure
                   │
                   ▼
           GM.Application
                   │
                   ▼
              GM.Domain
```

### GM.Domain

Contains the core business entities and domain logic.

Current entities include:

```text
ContactInquiry
Product
User
```

### GM.Application

Contains application use cases and business workflows.

It includes:

* MediatR Commands
* MediatR Queries
* Command Handlers
* Query Handlers
* FluentValidation
* Application abstractions
* Validation Pipeline Behavior

### GM.Infrastructure

Handles external concerns such as:

* Entity Framework Core
* SQL Server
* Database configuration
* Repository implementations
* Password hashing
* JWT token generation
* File storage
* Dependency Injection

### GM.API

Provides the HTTP API and handles:

* Controllers
* HTTP requests/responses
* Authentication
* Authorization
* Middleware
* CORS
* Swagger
* Static file serving

---

# Contact & Inquiry System

Visitors can submit inquiries through the Contact form.

The form accepts:

* Name
* Email
* Subject
* Message

The complete flow is:

```text
User
  │
  ▼
React Contact Form
  │
  ▼
POST /api/Contact
  │
  ▼
ASP.NET Core API
  │
  ▼
MediatR
  │
  ▼
FluentValidation
  │
  ▼
CreateContactInquiryCommand
  │
  ▼
ContactInquiry Entity
  │
  ▼
Repository
  │
  ▼
Entity Framework Core
  │
  ▼
SQL Server
```

---

# Product Management

The application includes a complete product management flow.

## Product Operations

```text
GET    /api/Products
GET    /api/Products/{id}
POST   /api/Products
PUT    /api/Products/{id}
DELETE /api/Products/{id}
```

Product information includes:

* Name
* Description
* Price
* Category
* Image URL
* Creation date
* Update date

Product management endpoints are protected using JWT authentication.

---

# Product Image Upload

Products support image uploads using `multipart/form-data`.

The backend:

1. Receives the uploaded image.
2. Validates the request.
3. Generates a unique file name.
4. Stores the image under:

```text
GM.API/wwwroot/images/products
```

5. Saves the image URL with the product.

The frontend uses `FormData` when creating or updating products.

---

# Authentication & Authorization

The application uses **JWT Bearer Authentication**.

## Registration

Users can create an account using:

```http
POST /api/Auth/register
```

### Request

```json
{
  "fullName": "Mariam Sayed",
  "email": "mariam@example.com",
  "password": "password123"
}
```

The password is never stored as plain text.

It is hashed using ASP.NET Core's password hashing functionality before being stored in SQL Server.

---

## Login

Users can log in using:

```http
POST /api/Auth/login
```

### Request

```json
{
  "email": "mariam@example.com",
```
