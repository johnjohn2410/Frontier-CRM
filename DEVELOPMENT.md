# Development Setup

This guide will help you set up the Frontier CRM development environment.

## Prerequisites

- .NET 9 SDK
- Docker and Docker Compose
- Visual Studio Code or Visual Studio 2022
- Git

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/johnjohn2410/Frontier-CRM.git
cd Frontier-CRM
```

### 2. Start Dependencies

Start PostgreSQL and Redis using Docker Compose:

```bash
docker-compose up -d
```

This will start:
- PostgreSQL on port 5432
- Redis on port 6379

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run --project src/FrontierCRM.Api
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:7001`
- Swagger UI: `https://localhost:7001/swagger`

## Database Setup

The application will automatically:
1. Create the database if it doesn't exist
2. Run migrations
3. Seed initial data (default tenant, admin user, roles, permissions)

### Default Credentials

- **Admin User**: admin@frontiercrm.com
- **Default Tenant**: "default" subdomain
- **Default Roles**: Administrator, User, ReadOnly

## API Testing

### Create a Tenant

```bash
curl -X POST "https://localhost:7001/api/tenants" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Company",
    "subdomain": "testcompany",
    "website": "https://testcompany.com",
    "industry": "Technology"
  }'
```

### Create a User

```bash
curl -X POST "https://localhost:7001/api/users" \
  -H "Content-Type: application/json" \
  -H "X-Tenant-Id: <tenant-id>" \
  -d '{
    "email": "user@testcompany.com",
    "firstName": "John",
    "lastName": "Doe",
    "jobTitle": "Sales Manager"
  }'
```

### Get Users

```bash
curl -X GET "https://localhost:7001/api/users" \
  -H "X-Tenant-Id: <tenant-id>"
```

## Project Structure

```
FrontierCRM/
├── src/
│   ├── FrontierCRM.Domain/          # Domain entities and value objects
│   ├── FrontierCRM.Application/     # Use cases, DTOs, handlers
│   ├── FrontierCRM.Infrastructure/  # Data access, external services
│   ├── FrontierCRM.Api/             # Web API controllers
│   ├── FrontierCRM.Web/             # Blazor web application
│   └── FrontierCRM.Workers/         # Background services
└── tests/
    ├── FrontierCRM.UnitTests/
    ├── FrontierCRM.IntegrationTests/
    └── FrontierCRM.EndToEndTests/
```

## Development Workflow

1. **Make changes** to domain, application, or infrastructure layers
2. **Run tests** to ensure nothing is broken
3. **Test API endpoints** using Swagger UI or curl
4. **Commit changes** with descriptive messages

## Troubleshooting

### Database Connection Issues

1. Ensure PostgreSQL is running: `docker-compose ps`
2. Check connection string in `appsettings.Development.json`
3. Verify database exists: `docker exec -it frontiercrm-postgres psql -U postgres -l`

### Port Conflicts

If ports 5000, 7001, 5432, or 6379 are in use:
1. Stop conflicting services
2. Or modify ports in `docker-compose.yml` and `appsettings.json`

### Migration Issues

If you encounter migration errors:
1. Delete the database: `docker-compose down -v`
2. Restart services: `docker-compose up -d`
3. Run the application again

## Next Steps

- Implement authentication and authorization
- Add more CRM entities (Accounts, Contacts, Leads, Opportunities)
- Create the Blazor web interface
- Add comprehensive tests
- Set up CI/CD pipeline
