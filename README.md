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
* Customer account dashboard
* View authenticated user information
* Update account information
* Persistent account data stored in SQL Server

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
* Update current authenticated user
* Logout functionality
* Authentication token handling on frontend
* User identity resolved from JWT
* Users can only update their own profile

### Customer Dashboard

The customer dashboard is connected directly to the backend and database.

Authenticated customers can:

* View their account information
* Retrieve their profile from the database
* Update their full name
* Update their email
* Receive validation/error responses from the backend
* Access the dashboard only when authenticated

The dashboard uses the authenticated user's ID from the JWT rather than accepting a user ID from the frontend.

The profile update flow is:

```text
Customer
   │
   ▼
Customer Dashboard
   │
   ▼
GET /api/Auth/me
   │
   ▼
JWT Authentication
   │
   ▼
User ID from JWT
   │
   ▼
GetCurrentUserQuery
   │
   ▼
User Repository
   │
   ▼
SQL Server
```

For profile updates:

```text
Customer
   │
   ▼
Edit Profile
   │
   ▼
PUT /api/Auth/me
   │
   ▼
JWT Authentication
   │
   ▼
User ID from JWT
   │
   ▼
UpdateProfileCommand
   │
   ▼
UpdateProfileCommandHandler
   │
   ▼
User Entity
   │
   ▼
User Repository
   │
   ▼
SQL Server
```

The update endpoint does not accept a `UserId` from the request body. This prevents a customer from attempting to update another user's profile by supplying a different user ID.

### Backend

* Server-side validation
* Invalid requests return proper error responses
* Valid inquiries are stored in SQL Server
* Products are stored in SQL Server
* Users are stored in SQL Server
* Customer profile updates are persisted in SQL Server
* Clean Architecture
* MediatR*
