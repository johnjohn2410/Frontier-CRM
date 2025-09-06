# Frontier CRM

A **developer-first, admin-friendly, high-performance CRM** written in C#/.NET that rivals Salesforce in breadth while beating it in **speed, openness, pricing clarity, and customization**.

## Core Differentiators

1. **Blazing performance**: sub-100 ms P95 for common reads; reactive UI with optimistic concurrency; realtime presence/updates
2. **Open data & clean exits**: one-click full export (JSONL + Parquet + SQL), reverse-ETL connectors; no proprietary lock-in
3. **Developer platform in C#**: safe, sandboxed C# functions (Roslyn-based) for server-side business logic, webhooks, and scheduled jobs
4. **Admin-grade customization**: metadata-driven custom objects/fields, formula fields, page layouts, validation rules, conditional UI
5. **Automation that scales**: visual workflow builder + event bus; idempotent actions; versioned flows; built-in test/simulate mode
6. **Predictable pricing**: per-seat + fair usage for compute/storage; unlimited API calls within reasonable throttles
7. **First-class mobile & offline**: .NET MAUI app with delta sync; conflict-free replicated data types (CRDT) for notes/activities

## Architecture

Built using **Clean Architecture** principles with a modular monolith approach:

```
FrontierCRM/
├── src/
│   ├── FrontierCRM.Domain/          # Entities, ValueObjects, Domain Events
│   ├── FrontierCRM.Application/     # UseCases, DTOs, Validators, Handlers (MediatR)
│   ├── FrontierCRM.Infrastructure/  # EF Core, Repositories, Message Bus, Search, File Storage
│   ├── FrontierCRM.Api/             # ASP.NET Core API (Minimal APIs), Auth, DI
│   ├── FrontierCRM.Web/             # Blazor Server (admin) or React app
│   ├── FrontierCRM.Workers/         # Background services (indexer, workflows)
│   └── FrontierCRM.Shared/          # Contracts, Abstractions
└── tests/
    ├── FrontierCRM.UnitTests/
    ├── FrontierCRM.IntegrationTests/
    └── FrontierCRM.EndToEndTests/
```

## Tech Stack

- **Runtime**: .NET 9, ASP.NET Core Minimal APIs
- **Database**: PostgreSQL with JSONB for custom fields
- **Cache**: Redis
- **Message Bus**: RabbitMQ or Azure Service Bus
- **Search**: OpenSearch/Elasticsearch
- **Frontend**: Blazor Server/WebAssembly or React
- **Auth**: OIDC (Auth0/Entra ID) + SAML for legacy SSO
- **Observability**: OpenTelemetry, Prometheus, Grafana
- **Deploy**: Containers + Kubernetes

## Multi-Tenancy

- **Primary model**: Single database, shared schema with `TenantId` on every row + EF Core global query filters
- **Optional premium isolation**: Dedicated DB per large tenant
- **Security**: Row-level security enforced in app layer + DB RLS (optional)
- **Limits/Quotas**: Seats, storage, API bandwidth, automation runtime seconds

## Security & Permissions

- **AuthN**: OIDC/SAML SSO; sessions via cookies + rotating refresh tokens
- **AuthZ**: Hybrid Role-Based + Attribute-Based (RBAC+ABAC)
- **Record sharing**: Owner-centric; share tables with effective permissions computed via materialized view/cache
- **Audit & Compliance**: Append-only audit log; immutable hash chain; GDPR tooling

## Core CRM Features

### MVP (3–4 months)
- Auth/SSO, Tenants & Users, Roles/Permissions
- Accounts, Contacts, Leads, Opportunities, Activities
- Pipelines + Kanban, basic forecasting
- Global search, list views, inline edit
- Import wizard (CSV) + Salesforce/HubSpot import
- REST API + Webhooks, basic SDKs
- Admin: custom fields, validation rules, page layouts
- Observability, audit log, usage metering

### MLP + Enterprise (next 3–6 months)
- Workflow builder (events → conditions → actions)
- Server-side C# functions (sandboxed), schedulers
- Email & calendar sync (Gmail/Outlook) + side panel
- Reporting, dashboards, scheduled reports
- Territories, record sharing, field-level security
- App marketplace (private apps), change data capture stream
- Mobile app with offline (.NET MAUI)

## Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL 15+
- Redis 7+
- Docker (optional, for containerized development)

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/FrontierCRM.git
   cd FrontierCRM
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Set up the database**
   ```bash
   # Update connection strings in appsettings.json
   dotnet ef database update --project src/FrontierCRM.Infrastructure --startup-project src/FrontierCRM.Api
   ```

4. **Run the application**
   ```bash
   dotnet run --project src/FrontierCRM.Api
   ```

5. **Access the application**
   - API: `https://localhost:7001`
   - Web UI: `https://localhost:7002`

## Development Roadmap

### Days 0–30
- ✅ Bootstrap repo & CI/CD
- ✅ Tenant middleware
- ✅ User/role models
- ✅ Accounts/Contacts CRUD
- ✅ EF Core + filters
- ✅ OpenTelemetry
- 🔄 Import wizard
- 🔄 List views
- 🔄 Search indexer skeleton

### Days 31–60
- Leads/Opportunities/Activities
- Pipelines & forecasting
- Audit log
- Webhooks & REST SDK
- Admin: custom fields + validation rules
- Basic dashboards
- SSO

### Days 61–90
- Workflow engine v1 (events/conditions/actions)
- Scheduling
- Sandboxed C# functions (pilot)
- Hardening, load tests
- Billing/metering
- Closed beta

## Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- Documentation: [docs.frontiercrm.com](https://docs.frontiercrm.com)
- Issues: [GitHub Issues](https://github.com/yourusername/FrontierCRM/issues)
- Discussions: [GitHub Discussions](https://github.com/yourusername/FrontierCRM/discussions)

---

**Built with .NET 9 and Clean Architecture principles**
