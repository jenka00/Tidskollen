# Tidskollen
Welcome to Tidskollen, a school project in Advanced .NET

##
The project is a fictive solution for a cultivation company to keep track of its employees, their ongoing projects and the employees working time. 
The project is buildt in C# in VisualStudio 2019. It consists of two systems in the same solution - one is a class library called "Tidskollen.Models"
and the other one is a Rest-API called "Tidskollen.API" which depends on "Tidskollen.Models".

Tidskollen.Models contains four models: Employees, TimeReport, Project, and EmployeeProject (which is a junction table between Employees and Projects). 
It's created as a class library to make it easy to be used it in other applications later on. 

Tidskollen.API is constructed as a Rest-API and uses the principals of dependency injection. It has two interfaces - ITidskollen and ITimeReport.
ITidskollen is generic interface, which all the repositories for Employee, Project, EmployeeProject and TimeReport implements, since the 
requirements for the project where that the user should be able to Get All, Get single, Add, Update and Delete items from all these classes. 
There were also a more specific requirement for the TimeReports, like show timereports within a certain period of time. Therefore I created an extra interface 
for this method and TimeReport repository implements both of the interfaces. 

All items were stored in a database using Entity Framework.



## Links
[Projekt Tidskollen Anrop till Postman](https://github.com/jenka00/Tidskollen/files/8426921/Projekt.Tidskollen.Anrop.till.Postman.pdf)
