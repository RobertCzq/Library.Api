# Library.Api
This is an implementation of the Library api for the web api exercise.
It contains 2 projects:
 - Library.Api contains the main api which is split into 3 controllers.
 - Library.Api.UnitTests contains the unit tests, mostly for the services.
In order to run the tests clone the github project to your IDE of choice and run the tests from the Library.Api.UnitTests project.

For ease of development I have decided to use a Sqlite database, in the database there are 3 tables that have the same structure that is described in the task.
The main trade off with my approach would be using the local db for storing data, in a real word scenario this will be configured as a separate resource and not something local.

Future improvements:
- Improve error handling and loging.
- Extend api to add GetAll endpoints
- Add more unit tests and integration tests, right now there is only unit testing which is focused mostly on the services.

The api is available on azure at https://libraryapiroc.azurewebsites.net/swagger/index.html
I've added some dummy data for books (id 2,3,4) and members(id 2, 3) that can be used

