# KavenegarCodePractice

This code practice has been submitted as the initial stage of eligibility evaluation for the job application I submitted to Kavenegar.

Here are some descriptions about what I did in this practice:

1. The project employs a monolithic design with DDD
2. All primitive types are modeled as value objects with validation logics encapsulated inside
3. CQRS pattern is employed to achieve separation of concerns in command and query sides
4. Repository and Unit of Work patterns are implemented in command side
5. In Query side we directly access DbContext and Redis cache for more performant queries and returned results are modeled as View Models (instead of Domain Models in command side) due to possible mismatches with Domain Models for join operations
6. OCP principle applied in Application layer design where each command and query implemented as separate module
7. We have a tiny Framework layer that is shared among all projects and have utility functions corresponding to each layer
8. Optimistic concurrency control is implemented by shadow properties in each entity configured in DbContext
9. Unit tests are written for domain layer and application layer (Using Moq to mock repository)
10. MediatR and Mapster libraries are used for mapping controllers to corresponding handlers
11. Validation in application layer is implemented using FluentValidation library
12. Global exception handling mechanism is implemented as middleware
13. CQRS pattern that I implemented can facilitate horizontal scaling, but due to stateful nature of the service, implemnting it in practice may be beyond the scope of this code

I hope you find the results compelling :)
