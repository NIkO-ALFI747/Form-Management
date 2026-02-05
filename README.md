# Form Management System

A production-grade full-stack form management application demonstrating enterprise-level software architecture, security best practices, and modern development patterns. Built with .NET 9 and React 19, featuring cookie-based JWT authentication, comprehensive validation, and Docker deployment.

## Architecture Overview

### Backend Architecture (.NET 9)

The backend follows **Clean Architecture** with strict separation of concerns across four layers:

```
┌─────────────────────────────────────────────────────────────┐
│                      Presentation Layer                      │
│                    (Form-Management.Api)                     │
│  Controllers, Middlewares, DTOs, Error Mapping, Filters     │
└──────────────────────┬──────────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────────┐
│                     Application Layer                        │
│                (Form-Management.Application)                 │
│  Services, Interfaces, Request/Response Models, Validation  │
└──────────────────────┬──────────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────────┐
│                       Domain Layer                           │
│                  (Form-Management.Domain)                    │
│    Entities, Value Objects, Domain Errors, Business Logic   │
└──────────────────────┬──────────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────────┐
│                   Infrastructure Layers                      │
│         (Persistence & Infrastructure Projects)              │
│   Data Access, External Services, JWT, Password Hashing     │
└─────────────────────────────────────────────────────────────┘
```

**Key Architectural Decisions:**

- **Domain-Driven Design (DDD)**: Core business logic encapsulated in the Domain layer using Value Objects
- **Repository Pattern**: Abstract data access through interfaces
- **CQRS-lite approach**: Separation of command and query concerns at the service level
- **Dependency Inversion**: All dependencies flow inward toward the Domain

### Frontend Architecture (React 19 + TypeScript)

Modern React architecture with functional components and custom hooks:

```
src/
├── components/         # Reusable UI components
│   ├── Auth/          # Authentication forms with complex state management
│   ├── NavBar/        # Navigation with auth-aware menu
│   ├── UsersSection/  # Data table with selection management
│   └── Routers/       # Route configuration with auth guards
├── pages/             # Page-level components
├── services/          # API client layer (Axios)
└── contracts/         # TypeScript interfaces for API communication
```

## Technical Deep Dive

### 1. Security Architecture

#### Password Security Implementation

**Multi-Layer Password Validation:**

1. **Domain-Level Validation** (`Password` Value Object):
   ```csharp
   - Minimum length: 8 characters
   - Maximum length: 100 characters
   - Regex complexity: (?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])
   - Compiled regex pattern for performance
   ```

2. **Pwned Passwords API Integration**:
   - Implements k-anonymity model via SHA-1 hash prefix
   - Async validation against HaveIBeenPwned database
   - Graceful degradation with fallback on API failure
   - Exception handling for network timeouts

3. **BCrypt Password Hashing**:
   - Industry-standard adaptive hashing function
   - Automatic salt generation per password
   - Work factor tuning for computational cost

**Code Reference:**
```csharp
// Domain/Models/ValueObjects/Password.cs - Enforces complexity
// Application/Services/ValueObjects/Password/Validation/IsPasswordNotPwnedService.cs
// Infrastructure/Auth/PasswordHasher.cs - BCrypt implementation
```

#### Authentication & Authorization

**JWT-Based Authentication:**

- **Token Generation**: HS256 algorithm with configurable expiration
- **Cookie Storage**: HttpOnly, Secure, SameSite=Strict cookies
- **Token Claims**: Minimal payload (userId only) to reduce token size
- **Validation**: ASP.NET Core JWT Bearer middleware with symmetric key

**Cookie Security Configuration:**
```csharp
- HttpOnly: true     // Prevents XSS access to tokens
- Secure: true       // HTTPS-only transmission
- SameSite: Strict   // CSRF protection
- Essential: true    // GDPR compliance flag
```

#### Data Protection

**ASP.NET Core Data Protection with X.509 Certificates:**

- Certificate-based key encryption for cookie encryption
- Base64-encoded certificate storage in environment variables
- Separate database context (`DataProtectionKeyDbContext`) for key persistence
- PostgreSQL-backed key ring storage

**Implementation:**
- Automatic key rotation support
- Keys persisted to database for multi-instance deployments
- X509Certificate2 validation with cryptographic error handling

### 2. Error Handling & Validation

#### Functional Error Handling with Result Pattern

Using **CSharpFunctionalExtensions** library for railway-oriented programming:

```csharp
public static Result<User, ValueObjectValidationError> Create(
    string? name, 
    string? email, 
    string? password
) =>
    FilledString.Create(name)
    .Bind(nameValue => Email.Create(email)
    .Bind(emailValue => Password.Create(password)
    .Map(passwordValue => new User(nameValue, emailValue, passwordValue))));
```

**Benefits:**
- Eliminates null reference exceptions
- Explicit error types at compile time
- Composable validation chains with `Bind` and `Map`
- Domain errors bubble up to API layer for proper HTTP status mapping

#### Problem Details (RFC 7807) Error Responses

Standardized error response format with custom extensions:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "errors": {
    "Password": ["Password must contain at least one uppercase letter..."]
  }
}
```

**Custom Result Objects:**
- `ValidationErrorsProblemDetailsResult`: Aggregated validation errors
- `BadRequestErrorsProblemDetailsResult`: Domain-specific errors
- `ErrorResponseMappingConfig`: Centralized error code to HTTP status mapping

#### Global Exception Middleware

```csharp
// Middlewares/ErrorHandlingMiddleware.cs
- Catches unhandled exceptions
- Logs with structured logging (ILogger)
- Returns consistent Problem Details responses
- Prevents information leakage in production
```

### 3. Frontend State Management & Validation

#### Form State Management with Formik + Yup

**Complex Validation Logic:**

```typescript
// SignUpForm validation schema
const validationSchema = yup.object({
  name: yup.string()
    .required('Name is required')
    .min(3, 'Name must be at least 3 characters'),
  email: yup.string()
    .required('Email is required')
    .email('Invalid email format'),
  password: yup.string()
    .required('Password is required')
    .min(8, 'Password must be at least 8 characters')
    .matches(/[A-Z]/, 'Must contain uppercase')
    .matches(/[a-z]/, 'Must contain lowercase')
    .matches(/\d/, 'Must contain number')
    .matches(/[\W_]/, 'Must contain special character')
});
```

#### Custom Hooks Architecture

**`useFieldInteractionErrorsHandling`** - Sophisticated error display logic:

1. **Error Visibility State**: Tracks when to show validation errors
2. **Field Error Observers**: Monitors Formik field states
3. **Server Error Logic**: Handles API validation failures
4. **Interaction Tracking**: Shows errors only after user interaction

**Benefits:**
- Prevents premature error display (UX optimization)
- Syncs client/server validation errors
- Reusable across multiple form fields

**`useSubmitAuthForm`** - Form submission orchestration:
```typescript
- Formik integration with server-side error handling
- Loading state management
- Authentication state updates
- Error alert display coordination
```

### 4. Data Access Layer

#### Entity Framework Core with PostgreSQL

**DbContext Configuration:**

- **Separate Contexts**: 
  - `FormManagementDbContext`: Application data
  - `DataProtectionKeyDbContext`: ASP.NET Core key ring

- **Value Object Conversions**:
  ```csharp
  // UserConfiguration.cs
  builder.Property(u => u.Name)
      .HasConversion(
          name => name.Value,
          value => FilledString.Create(value).Value
      );
  ```

- **Concurrency**: Configured for PostgreSQL with Npgsql provider
- **Migrations**: Code-first with versioned migrations

#### Repository Pattern Implementation

```csharp
public interface IUsersRepository
{
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
}
```

**Async/Await Throughout**: All database operations use async patterns for scalability

### 5. API Design

#### RESTful Endpoints

```
POST   /api/signup    - User registration with validation
POST   /api/login     - Authentication with JWT issuance
POST   /api/signout   - Session termination
GET    /api/users     - Retrieve all users (authenticated)
```

#### Request/Response Pipeline

1. **Request Validation**: FluentValidation rules on DTOs
2. **Controller Actions**: Thin controllers delegating to services
3. **Service Layer**: Business logic orchestration
4. **Error Mapping**: Domain errors → HTTP responses
5. **Response Serialization**: Mapster for DTO mapping

**Action Filters:**
- `UserExistenceCheckFilter`: Validates user operations before execution

### 6. Frontend-Backend Integration

#### Axios Configuration

```typescript
// axiosConfig.ts
axios.defaults.withCredentials = true;  // Include cookies in requests
```

**Cookie-Based Authentication Flow:**

1. Client sends credentials to `/api/login`
2. Server validates and issues JWT in HttpOnly cookie
3. Browser automatically includes cookie in subsequent requests
4. Server validates JWT from cookie via middleware
5. On logout, cookie is cleared server-side

**CORS Configuration:**
```csharp
AllowedOrigins: ["http://localhost:5000"]  // Frontend origin
AllowCredentials: true                     // Enable cookie sharing
```

### 7. Docker Deployment

#### Multi-Container Setup

**Backend Container (`form_management_api`):**
- Base image: .NET 9 SDK for build, runtime for production
- Multi-stage build for optimized image size
- Port: 10000
- Environment-based configuration injection

**Frontend Container (`form_management_client`):**
- Base image: Node 18 for build, Nginx for serving
- Build-time API URL injection via Vite environment variables
- Port: 5000
- Production-optimized build with minification

**Docker Compose Configuration:**
```yaml
services:
  form_management_api:
    environment:
      - ASPNETCORE_ENVIRONMENT=Release
      - DATASOURCE_URL=DB_STRING
      - CORS_ALLOWED_ORIGINS=http://localhost:5000
      - JWT_SECRET_KEY=...
      - CERTIFICATE_BASE64=...
  
  form_management_client:
    depends_on:
      - form_management_api
    build:
      args:
        - VITE_USERS_URL=http://localhost:10000/api/users
        - VITE_LOGIN_URL=http://localhost:10000/api/login
```

**Network Configuration:**
- Backend exposed on host port 10000
- Frontend exposed on host port 5000
- Containers communicate via Docker network

## Technology Stack

### Backend
- **.NET 9** - Latest LTS framework with C# 13
- **ASP.NET Core** - Web API framework
- **Entity Framework Core 9** - ORM with PostgreSQL provider
- **Mapster** - High-performance object mapping
- **FluentValidation** - Declarative validation rules
- **CSharpFunctionalExtensions** - Railway-oriented programming
- **BCrypt.Net-Next** - Password hashing
- **JWT Bearer Authentication** - Token-based auth

### Frontend
- **React 19** - Latest React with concurrent features
- **TypeScript 5.8** - Type-safe JavaScript
- **Vite 6** - Next-generation build tool
- **React Router v7** - Client-side routing
- **Formik 2.4** - Form state management
- **Yup 1.6** - Schema validation
- **Axios 1.9** - HTTP client
- **Bootstrap 5** - UI framework
- **React Bootstrap** - React components for Bootstrap

### Infrastructure
- **PostgreSQL** - Primary database
- **Docker & Docker Compose** - Containerization
- **Nginx** - Static file serving (frontend)

## Key Features & Patterns

### Design Patterns Implemented

1. **Repository Pattern**: Data access abstraction
2. **Factory Pattern**: Value object creation with validation
3. **Dependency Injection**: Built-in ASP.NET Core DI container
4. **Result Pattern**: Functional error handling
5. **Middleware Pattern**: Cross-cutting concerns (error handling, auth)
6. **Service Layer Pattern**: Business logic encapsulation
7. **Value Object Pattern**: Domain-driven design primitives

### Advanced Techniques

- **Async/Await Everywhere**: Non-blocking I/O throughout the stack
- **LINQ**: Fluent data querying
- **Expression Bodies**: Concise method syntax
- **Record Types**: Immutable DTOs
- **Nullable Reference Types**: Compile-time null safety
- **Generic Constraints**: Type-safe abstractions

### Code Quality Practices

- **Separation of Concerns**: Clear layer boundaries
- **Single Responsibility**: Focused classes and methods
- **Open/Closed Principle**: Extensible without modification
- **Interface Segregation**: Minimal interface contracts
- **Explicit Error Handling**: No silent failures
- **Immutability**: Value objects are immutable by design

## Running the Project

### Prerequisites

- Docker Desktop
- .NET 9 SDK (for local development)
- Node.js 18+ (for local frontend development)
- PostgreSQL (if running outside Docker)

### Quick Start with Docker

```bash
# Clone the repository
git clone <repository-url>
cd Form-Management-main

# Start all services
docker-compose up --build

# Access the application
# Frontend: http://localhost:5000
# Backend API: http://localhost:10000
```

### Local Development Setup

#### Backend

```bash
cd Form-Management.Api

# Restore dependencies
dotnet restore

# Update database connection string in appsettings.Development.json
# Run migrations
dotnet ef database update --context FormManagementDbContext
dotnet ef database update --context DataProtectionKeyDbContext

# Run the API
dotnet run
```

#### Frontend

```bash
cd Form-Management.Client

# Install dependencies
npm install

# Create .env file with API URLs
echo "VITE_USERS_URL=http://localhost:10000/api/users" > .env
echo "VITE_LOGIN_URL=http://localhost:10000/api/login" >> .env
echo "VITE_SIGNUP_URL=http://localhost:10000/api/signup" >> .env
echo "VITE_SIGNOUT_URL=http://localhost:10000/api/signout" >> .env

# Start development server
npm run dev
```

## Project Structure

```
Form-Management-main/
├── Form-Management.Api/              # Presentation Layer
│   ├── Controllers/                  # API endpoints
│   ├── Middlewares/                  # Request pipeline middleware
│   ├── Extensions/                   # Service collection extensions
│   ├── Contracts/                    # DTOs and response models
│   ├── Filters/                      # Action filters
│   └── Program.cs                    # Application entry point
│
├── Form-Management.Application/      # Application Layer
│   ├── Services/                     # Business logic services
│   ├── Interfaces/                   # Service contracts
│   ├── Contracts/                    # Request/Response models
│   └── Extensions/                   # Validation extensions
│
├── Form-Management.Domain/           # Domain Layer
│   ├── Models/                       # Entities and Value Objects
│   │   ├── User/                     # User aggregate
│   │   └── ValueObjects/             # Email, Password, FilledString
│   ├── Errors/                       # Domain-specific errors
│   └── Interfaces/                   # Repository interfaces
│
├── Form-Management.Persistence/      # Data Access Layer
│   ├── FormManagement/               # Main database context
│   │   ├── Configurations/           # EF Core entity configs
│   │   ├── Repositories/             # Repository implementations
│   │   └── Migrations/               # Database migrations
│   └── DataProtection/               # Key ring database
│
├── Forms-Management.Infrastructure/  # Infrastructure Layer
│   └── Auth/                         # JWT & password services
│
├── Form-Management.Client/           # React Frontend
│   ├── src/
│   │   ├── components/               # React components
│   │   │   ├── Auth/                 # Authentication forms
│   │   │   ├── NavBar/               # Navigation
│   │   │   └── UsersSection/         # User management table
│   │   ├── pages/                    # Page components
│   │   ├── services/                 # API clients
│   │   └── contracts/                # TypeScript types
│   └── Dockerfile                    # Frontend container
│
├── docker-compose.yml                # Multi-container orchestration
└── Form-Management.sln               # Visual Studio solution
```

## Configuration

### Environment Variables (Backend)

```bash
ASPNETCORE_ENVIRONMENT=Development|Production
ASPNETCORE_HTTP_PORTS=10000
DATASOURCE_URL=Server=host;Port=5432;Database=form_management;User Id=user;Password=pass
CORS_ALLOWED_ORIGINS=http://localhost:5000
JWT_SECRET_KEY=<minimum-256-bit-secret>
JWT_EXPIRES_HOURS=12
CERTIFICATE_BASE64=<base64-encoded-pfx>
CERTIFICATE_PASSWORD=<certificate-password>
AUTH_COOKIES_KEY=form_management_auth_cookies
```

### Environment Variables (Frontend)

```bash
VITE_USERS_URL=http://localhost:10000/api/users
VITE_LOGIN_URL=http://localhost:10000/api/login
VITE_SIGNUP_URL=http://localhost:10000/api/signup
VITE_SIGNOUT_URL=http://localhost:10000/api/signout
```

### Certificate Generation

Use the included PowerShell script to generate a development certificate:

```powershell
./GenerateAndBase64EncodeCertificate.ps1
```

## Security Considerations

### Production Deployment Checklist

- [ ] Use strong, randomly generated JWT secret key (256+ bits)
- [ ] Generate production X.509 certificate for Data Protection
- [ ] Enable HTTPS only (disable HTTP)
- [ ] Configure CORS with specific allowed origins (no wildcards)
- [ ] Use environment-specific secrets (not hardcoded in appsettings)
- [ ] Enable rate limiting on authentication endpoints
- [ ] Configure database connection string via secrets manager
- [ ] Set up logging and monitoring
- [ ] Implement IP whitelisting for admin operations
- [ ] Enable HSTS (HTTP Strict Transport Security)
- [ ] Configure Content Security Policy headers
- [ ] Use PostgreSQL connection pooling

### Known Security Considerations

- Development certificates included in repository (replace for production)
- Default JWT secret in appsettings (use secrets manager)
- CORS configuration should be environment-specific
- Consider implementing refresh tokens for long-lived sessions
- Add rate limiting middleware to prevent brute force attacks

## Testing Considerations

While this project focuses on architecture and implementation, a comprehensive test suite would include:

**Unit Tests:**
- Value Object creation and validation logic
- Service layer business logic
- Password hashing verification
- JWT token generation and validation

**Integration Tests:**
- Repository database operations
- Authentication flow end-to-end
- API endpoint responses
- Cookie setting and validation

**Frontend Tests:**
- Component rendering
- Form validation logic
- Custom hooks behavior
- API integration

## Performance Optimizations

- **Compiled Regex**: Password validation regex compiled for repeated use
- **Async I/O**: Non-blocking database and HTTP operations
- **Connection Pooling**: EF Core connection pool management
- **Object Mapping**: Mapster for high-performance DTO conversion
- **Static File Caching**: Nginx cache headers for frontend assets
- **Docker Layer Caching**: Multi-stage builds optimize image size

## Extensibility Points

The architecture supports easy extension in several areas:

1. **New Value Objects**: Add to Domain layer with validation
2. **Additional Entities**: Extend Domain with new aggregates
3. **Service Methods**: Add to Application layer services
4. **API Endpoints**: Create new controllers following existing patterns
5. **Frontend Pages**: Add routes and components
6. **Authentication Providers**: Plug in OAuth2, OpenID Connect
7. **Database Providers**: Switch from PostgreSQL via EF Core abstractions

## License

This project is licensed under the MIT License – see the LICENSE file for details.

## Author

A production-grade project demonstrating enterprise-level software engineering practices in .NET and React ecosystems.
