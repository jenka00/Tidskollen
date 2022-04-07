# Tidskollen
Welcome to Tidskollen, a school project in Advanced .NET

##
The project is a fictive solution for a cultivation company to keep track of its employees, their ongoing projects and the employees working time. 
The project is buildt in C# in VisualStudio 2019. It consists of two systems in the same solution - one is a class library called "Tidskollen.Models"
and the other one is a Rest-API called "Tidskollen.API" which depends on "Tidskollen.Models". All items were stored in a database using Entity Framework.

Tidskollen.Models contains four models: Employees, TimeReport, Project, and EmployeeProject (which is a junction table between Employees and Projects). 
It's created as a class library to make it easy to be used it in other applications later on. 

Tidskollen.API is constructed as a Rest-API and uses the principals of repository pattern with dependency injection. By using the repository pattern, it makes it easy to (possibly in the future) swap out the data source without changing the whole API, and dependency injection makes it easy to add new models if this later on would be required. Tidskollen.API has two interfaces - ITidskollen and ITimeReport.
ITidskollen is generic interface, which all the repositories for Employee, Project, EmployeeProject and TimeReport implements. Since the 
requirements for the project where that the user should be able to Get All, Get single, Add, Update and Delete items from all the classes in Models, ITidskollen contains methods for this. 
To meet the more specific requirement for the TimeReports, to be able to see timereports within a certain period of time, I created a new interface, ITimeReport, with a method for this. I also added another method in this interface, to get all timereports from one employee by taking the employee id as an in-parameter. TimeReport repository therefore implements both of these interfaces. 

The controllers classes - EmployeeController, TimeReportController, ProjectController and EmployeeProjectController - inherits from Controllerbase, and they implement 
Action methods. When a user writes a request to a URL, the request will be mapped to an appropriate controller which matches the route which have been defined in 
the controller and the Http verb. The request invokes an action method in the appropriate controller, which executes a method in the controller. This results in an Http response to the user. 

Since the Employee class contains some sensitive information, and since I also wanted to simplify request and responses to endpoints, I created DTOs. By adding different DTOs with different property exposure, I can control what information that was beeing revealed at the different endpoints for example in "Get a single Employee" or "add a new employee". I created profiles for each model and used automapper to automatically map the models with the DTOs. 

When I started to build the project, my idea was to use the TimeReport class as a punch clock, so that when a employee meets up for work, the employee writes 
its employee number as a POST request and a new Timereport is added with the users Id, the CheckIn time as DateTime.Now, and the CheckStatus as true. When the
Employee has finished for the day, the employee again writes its employee id and a PUT request is created which searches for the employees id and a true ChecStatus. 
The CheckOut time is then sat to DateTime.Now and the CheckStatus to false. This idea is yet to come and hasn't been implemented.

## Links
- [Anrop till Postman](https://github.com/jenka00/Tidskollen/files/8426921/Projekt.Tidskollen.Anrop.till.Postman.pdf)
- [Beskrivning av uppgiften](https://qlok.notion.site/Projekt-Avancerad-NET-eb0527e709864272a6602afef06597b8)
