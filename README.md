# MeminToDo
ASP.Net Core Microservices

- Containerization: Docker
- Container Management: Portainer
- API Gateway: Ocelot
- Logging: ElasticSearch, SeriLog, Kibana
- Log Monitoring: Seq
- Monitoring: AppMetrics/Prometheus/Grafana
- Tracing: OpenTracing/JaegerUI
- Service Discovery: Consul
- Resiliency: Polly

Skill Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- MongoDB database connection
- Generic Repository Pattern, CQRS Pattern, MediatR implementation
- Autofac, AutoMapper implementation
- Logging was done by SeriLog
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI

Entertainment Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- PostgreSQL database connection
- Using Dapper for micro-orm implementation to simplify data access and ensure high performance
- Autofac, AutoMapper implementation
- Logging was done by SeriLog
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI

Workout Service
- ASP.NET Core Web API application
- REST API principles, CRUD operations
- CosmosDB database connection
- Logging was done by SeriLog
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI

Dashboard Service
- ASP.NET Core Web API application
- Aggregator Pattern
- Logging was done by SeriLog
- Monitoring was done by AppMetrics/Prometheus/Grafana
- Tracing was done by OpenTracing/JaegerUI
- Resiliency implementations were made with Polly
  
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
