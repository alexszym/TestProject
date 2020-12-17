# Introduction
This is a test project for a simple products api. It is an ASP.NET Core api with Mongo integration.

# How to run
To run the project you need to have Docker running. Use `docker-compose up` to run the project. This will initiate all the dependencies including a seeded mongo database.

To use navigate to http://localhost:5000/swagger

# Some notes
Normally I'd include some unit tests with a project, but since the api is very simple integration tests (postman script) seemed more useful.

# Some quick feedback on the exercise
1. The README with the tasks implies that ids are preceded with a '00x' and called product codes, but that's not what the tests are looking for.