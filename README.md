# Overview of the Code architecture for the competex-backend.
```
/competex-backend
- /API
    - Controllers
        - MembersController.cs
    - DTOs
        - MemberDTO.cs
- /BLL
    - Interfaces
        - IMemberService.cs
    - Services
        - MemberService.cs
- /DAL
    - Repositories
        - MemberRepository.cs
- /Models
    - Member.cs
```

## API
In this directory we store all that is relevant to communication to and from the api.

### Controllers
We store the api controllers in this folder. Each api controller has CRUD functionality.
### DTOs
When we communicate via the api, the api controllers use contract based-models in form of Data Transfer Objects (DTOs)

---

## BLL (Business Logic Layer)
The Business Logic Layer is responsible for all the domain specific logic.

### Interfaces
### Services
Here we can perform various calculations and other stuff specific to the domain.
Controllers also communicate with the DAL layer through the services in the BLL.
The service layer utilizes a nuget package called "AutoMapper".
It requires a MappingProfile (Inherits from 'Profile'), but when configured, the mapping to and from DTOs and models, is as done in the following way.

```
_mapper.Map<MemberDTO>(member)
```
Where MemberDTO is the destination type and member is the source object (consisting of a type)

---

## DAL (Data Access Layer)
The data access layer handles communication with external data providers such as SQL databases.
We have chosen to implement Postgres as the database.

### Interfaces
We define interfaces such that the database can be changed without much hassle.
A database specific implementation is required, to add a database. This must inherit the I<Entity>Repository, i.e. IMemberRepository in postgres would look like

`PostgresMemberRepository : IMemberRepository`

### Repositories
The repositories is specific queries for entities, such as MemberRepository - which contains the queries related to the Member entity.

---

## Models
Use your models(e.g., Member) to represent the domain entities of your application.

---

# Github

You must push to develop.
When doing so, the following action will run.
`.github/workflows/dotnet-build-and-run-tests.yml`

It build the .net solution and runs the tests located in: 
´./competex-backend-tests´

the `main` branch is branc-protected. Only pull requests from the `develop` branch is allowed.
When performing a pull request to main, the following action, which check the upstream branch runs:
`.github/workflows/main-pull-request-protection.yml`

---



# Patterns used and their contributions
- Layered Architecture: Separates the application into distinct layers (API, BLL, DAL, Models).
- Repository Pattern: Encapsulates data access logic within repositories.
- Dependency Injection: Promotes loose coupling and facilitates testing.
- Data Transfer Object (DTO) Pattern: Simplifies data transfer between layers.
- Interface Segregation Principle: Defines clear contracts for services.
- MVC Pattern: Handles the separation of concerns within the API.
