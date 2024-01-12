# MeminToDo
ASP.Net Core Microservices

- Containerization: Docker
- Container Management: Portainer
- API Gateway: Ocelot
- Logging: ElasticSearch, SeriLog, Kibana
- Distributed Caching: Redis Sentinel
- Log Monitoring: Seq
- Monitoring: AppMetrics/Prometheus/Grafana
- Tracing: OpenTracing/JaegerUI
- Service Discovery: Consul
- Resiliency: Polly
- Messaging: RabbitMQ/MassTransit
- Real Time Communication: SignalR
- Storage: Amazon S3 Storage, Azure Blob Storage

Meal Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- MSSQL database connection
- Customized Entity Framework Core DBContext and ChangeTracker behaviors
- Generic Repository Pattern, Unit of Work Pattern
- Custom AutoMapper class
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Messaging done by RabbitMQ/MassTransit

Skill Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- MongoDB database connection
- Generic Repository Pattern, CQRS Pattern, MediatR implementation
- Autofac, AutoMapper implementation
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Amazon S3 Storage and Azure Blob Storage implementation
- Messaging done by RabbitMQ/MassTransit

Entertainment Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- PostgreSQL database connection
- Using Dapper for micro-orm implementation to simplify data access and ensure high performance
- Autofac, AutoMapper implementation
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Messaging done by RabbitMQ/MassTransit

Workout Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- CosmosDB database connection
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Messaging done by RabbitMQ/MassTransit

Budget Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- MSSQL database connection
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Real Time Communicatio done by SignalR
- Messaging done by RabbitMQ/MassTransit
  
Log Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- DynamoDB database connection
- Caching was done by Redis Sentinel
- Cron jobs for backup mechanism via Hangfire
- Messaging done by RabbitMQ/MassTransit

Dashboard Service
- ASP.NET Core Web API application
- Aggregator Pattern
- Logging was done by SeriLog
- Caching was done by Redis Sentinel
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Resiliency implementations were made with Polly

IdentityServer
- ASP.NET Core Web API application
- IdentityServer4 implementations
- MSSQL database connection
  
HealthCheck
- ASP.NET Core MVC application
- Implemented HealthChecks and WatchDogUI
- Logging was done by SeriLog
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Consul Service Registrations have been completed
  
Microservices Cross-Cutting Implementations
- HealthChecks/WatchDogUI integrations for health check
- AppMetrics/Prometheus/Grafana integrations for monitoring
- JaegerUI/OpenTracing integrations for tracing
- Consul integrations for service discovery
- Ocelot integrations for API Gateway
- Polly integrations for resiliency
