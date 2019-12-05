# aspnet-core-web-app
An Example ASP.Net Core 3 Web API Application, with some of the latest third-party tools like MediatR, AutoMapper, FluentValidation, Flurl, OData, EntityFramework Core, Swashbuckle and more. 

The intent of this project is to create a Event Driven Micro-service Web Application and we will start from the beginning of a basic ASP.Net Core 3 Web API App and add the additional event management as we go along.

We will try to keep this as simple and easy to use and extend as possible and thus we decided to mostly use common and trending third-part tools to do so.

## Packages Used:

### ![EFCore](Docs/Images/EF-Core-icon.png) EntityFramework Core
https://docs.microsoft.com/en-us/ef/#pivot=efcore
https://docs.microsoft.com/en-us/ef/core
> Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.
EF Core can serve as an object-relational mapper (O/RM), enabling .NET developers to work with a database using .NET objects, and eliminating the need for most of the data-access code they usually need to write.

### ![MediatR](Docs/Images/MediatR-icon.png) Mediatr
https://github.com/jbogard/MediatR/wiki
> MediatR is essentially a library that allows in process messaging – which in turn allows you to follow the Mediator Pattern!

### ![AutoMapper](Docs/Images/AutoMapper-icon.png) AutoMapper
https://automapper.org/
> AutoMapper is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another. This type of code is rather dreary and boring to write, so why not invent a tool to do it for us?

### ![FluentValidation](Docs/Images/FluentValidation-icon.png) FluentValidation
https://fluentvalidation.net
> A popular .NET library for building strongly-typed validation rules.

### ![Flurl](Docs/Images/Flurl-icon.png) Flurl
https://flurl.dev
> Flurl is a modern, fluent, asynchronous, testable, portable, buzzword-laden URL builder and HTTP client library for .NET.

### ![OData](Docs/Images/OData-icon.png) OData
https://docs.microsoft.com/en-us/odata
https://www.odata.org
https://www.odata.org/getting-started/basic-tutorial
> The Open Data Protocol (OData) is a data access protocol built on core protocols like HTTP and commonly accepted methodologies like REST for the web.

### ![Swashbuckle](Docs/Images/Swashbuckle-icon.png) Swashbuckle 
https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.0
> When consuming a Web API, understanding its various methods can be challenging for a developer. Swagger, also known as OpenAPI, solves the problem of generating useful documentation and help pages for Web APIs. It provides benefits such as interactive documentation, client SDK generation, and API discoverability.

### ![NUnit](Docs/Images/NUnit-icon.png) NUnit
https://nunit.org
> NUnit is a unit-testing framework for all .Net languages. Initially ported from JUnit, the current production release, version 3, has been completely rewritten with many new features and support for a wide range of .NET platforms.

### ![Moq](Docs/Images/Moq-icon.png) Moq
https://github.com/Moq/moq4/wiki/Quickstart
> The most popular and friendly mocking library for .NET


## Patterns we are trying to implement:

### The Mediator design pattern
> The Mediator design pattern defines an object which encapsulates how a set of objects interact with each other. 
> You can think of a Mediator object as a kind of traffic-coordinator; it directs traffic to appropriate parties based on its own state or outside values. Further, Mediator promotes loose coupling (a good thing!) by keeping objects from referring to each other explicitly.

### Command and Query Responsibility Segregation (CQRS) pattern
> Segregate operations that read data from operations that update data by using separate interfaces. This can maximize performance, scalability, and security. Supports the evolution of the system over time through higher flexibility, and prevents update commands from causing merge conflicts at the domain level.

### No need for repositories and unit of work with Entity Framework Core
Please read https://gunnarpeipman.com/ef-core-repository-unit-of-work

## Automated Testing 

## Unit Tests
We aim to have high code coverage by Unit testing all out Commands, Queries, Validations, Mappings and other parts of the code base. 

## Integration Tests
Integration Tests to test all exposed enpoints and Event messages, By hosting the web service in memory and calling the endpoints via http

## To Do

 - Add RabbitMQ for Event Driven Design
 - Add Basic Angular UI Application to showcase API Usage
 - Add SignalR for near real time updates to UI
 - Create NuGet Package of core and shared projects for package reuse
 - Project and NuGet Package Semantic Versioning
 - Add Polly for resilience and transient-fault-handling 
