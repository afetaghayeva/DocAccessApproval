# DocAccessApproval

A document access approval system built with .NET 8, following Domain-Driven Design (DDD), Clean Architecture principles, and using SQLite as the database.

---

## Project Structure & Design Decisions

This project uses a layered architecture with Domain-Driven Design (DDD) principles:

- **Domain Layer**  
  Contains the core domain models and aggregates ( `Document`, `User`), domain events, and interfaces (like `IDomainEvent`).

- **Application Layer**  
  Includes business logic like commands, queries, handlers, and application services. Uses MediatR for decoupled event handling and CQRS.

- **Infrastructure Layer**   
  Contains database implementations, context, entity configurations (SQLite and EF Core).

- **Cross-Cutting Features**  
  - Event simulation via domain events (e.g., `AccessRequestApproved`)
  - MediatR for domain event dispatching and handler execution
  - Bitwise operations for access control

---

##  Features & Assumptions

###  Implemented Features

1. **Document Request Expiration**  
   Each access request has an expiration date.

2. **Bitwise Access Control**  
   - Access types include `Read`, `Edit`, `Delete`.
   - User access type is stored as an integer using bitwise OR.
   - Access validation uses bitwise AND to check permissions:
     ```csharp
     (userAccess.AccessType & (int)accessType) == (int)accessType
     ```

3. **Filtering & Pagination**  
   - All list endpoints support pagination.
   - Access requests can be filtered by status.

4. **Seed Data**  
   - Default roles: `admin`, `approver`, `user`
   - Default admin user:
     - Username: `admin`
     - Password: `admin`

---

##  How to Run the Project

1. **Clone the repo**  
   ```bash
   git clone https://github.com/your-repo/doc-access-approval.git
   cd doc-access-approval
   
2. **Run the project**
   ```bash
   dotnet run

3. **Database Initialization**
   No manual migration is needed.
   The app automatically applies EF Core migrations and creates a SQLite database on first run.

4. **Run the tests**
   ```bash
   dotnet test


## Future Improvements (If More Time Available)
  1. Add Refresh Token support for authentication.
  2. Add Localization for all exception and validation messages.
  3. Integrate FluentValidation for request validation in Commands and Queries.

## Database Design
![image](https://github.com/user-attachments/assets/a0fb5e8f-9c8b-49ff-acad-fafdf942d509)
![image](https://github.com/user-attachments/assets/9f255549-43a8-4f82-8385-8e20b1d1dc39)


