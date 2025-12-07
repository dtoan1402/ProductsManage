1. Approach the Requirement: What are the steps in your development process?
   - Analyze requirements: Define functionality: create CRUD, filtering, searching, pagination, category management, data integrity.
   - API Design (RESTful) Apply REST standard: /api/products, /api/products/{id}, query params for filter.
   - Build Domain Model & DTO Separate: Product, Category → ProductCreateDto, ProductResponseDto.
   - Integrate ORM Use Entity Framework Core to map C# -> SQL
   - ValidationFluentValidation to check detailed input
   - Business processing Check category existence
   - Pagination & Filter Use LINQ + Skip/Take, index support performance.
2. Database Design: Which database? Why? How does it support new features?
Choose: Microsoft SQL Server. Reasons:
   - Data integrity: FOREIGN KEY, UNIQUE, CHECK, DEFAULT -> cannot add a product with a non-existent category_id.
   - Clear relationship: Product <-> Category -> easy to JOIN, report
   - Complex queries: filter by price + category + status -> WHERE,JOIN.. effectively with index.
   - Integrates well with .NET Core, LINQ, Migration, EF.
- How does your design support new product features or attributes:
  + Expand more data in the future if nessesary: product_images (product_id, url, is_primary), reviews (product_id, user_id, rating, comment),....
3. Technology Stack Components
   - NET 10.0: latest, high performance, cross-platform, RESTful native
   - Entity Framework Core: LINQ, Migration, Change Tracking, Lazy Loading
   - MS SQL Server: Integrity, transaction, .NET integration
   - FluentValidation: context-aware, reusable
   - AutoMapper: automatically map Entity <-> DTO
4. API and Data Handling: How do you process input/output?
   - Receive JSON → Deserialize to DTO
   - Validate using FluentValidation (async, check DB)
   - Check business rule: category exists
   - Map to Entity
   - Save to DB (EF Core)
5. Performance: Caching & Concurrency
   - Response Caching [ResponseCache(Duration = 60)] for GET public
