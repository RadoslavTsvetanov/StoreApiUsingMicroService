# StoreApiUsingMicroService



This is a REST webapplication made in a microservice style using C#, .NET (.NET > SpringBoot) and MongoDB (best fb)


----------------

Commands to run the project:
dotnet run -> runs and builds the project
dotnet build -> Buils the projects
http://localhost:{port}/swagger -> gives acces to the api like postman
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo -> spins up the mongo container

---------------

Design Patterns used:
- Repository Database
- Dependency Injection pattern
