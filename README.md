﻿# ASP.NET Core MVC WebApp -- Project: Secrets Keeper

This is a full-stack MVS web application in C#HTML (Razor) in the Microsoft Visual Studio 2020 IDE Community Edition and .NET 6.


## What is ASP.NET Core?

* Microsoft C# tool to build web applications
* Comparable to Java Spring, PHP Lavarel, Python Flask, and Node.js Express
* .NET -> Microsoft's Software Development Platform similar to Java Virtual Machine consists of runtime engine and libraries for executing programs written in a compliant language.  .NET is language neutral that supports more than 20 languages that include: C#, C++, VB, Java/J++, Ruby, Python,...

* ASP -> Active Server Page: Dynamic web pages, usually connected to a database.  It replaced the Classic ASP technology.
* Core -> open source, cross-platform version of ASP.

Together, ASP.NET Core is Full Stack having (database, business logic, HTML)

An ASP.NET page is web page that contains a mix of HTML markup and dynamic ASP markup.  An ASP.NET is run on the server, combining the static HTML code, and updating the dynamic ASP elements to produce a final HTML page.


![](./Static/ASPNETCORE.png)

### Create a new project

![wireframe](./Static/ASP_NET_Creation_Settings.PNG)

Initial project MVC scaffolding should be compilable and runable.

* Select `IIS` and `run`
![](./Static/ToolsSetAction.PNG)

* Initial looks and feel

![Initial project creation](./Static/WebAppEntryPoint.PNG)

<br />
<hr>

## The _MVC_ Design Pattern

The __M__ odel __V__ iew __C__ ontroller helps to enforce separation of concerns; avoiding mixing presentation logic, business logic, and data access logic.

### *`Model`* manages the behaviour and data
* Data related
* Consists of classes / objects with properties
* Uses SQL statements
* Supplies the controller with lists of objects

### *`View`* manages the display of data

* HTML CSS code (or similar)
* Usually gets a list of data from the controller.
* Dynamically combines data with HTML in a template
* Razor (ASP.net) - a combination of HTML and script

##### Step to create a view

1. Right click on `Views` folder and select `Add` -> `Class..`
1. Inside the ```public class Secret { }```, type `prop` and tab twice.
1. Type `ctor` and tab twice to generate code for empty constructor.

```javascript
    public class Secret {
        public int Id { get; set; }
        public string Name { get; set; }
        public string eMail { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Question1 { get; set; }
        public string Answer1 { get; set; }
        public string Question2 { get; set; }
        public string Answer2 { get; set; }
        public string Question3 { get; set; }
        public string Answer3 { get; set; }
        public string PIN { get; set; }
        public string Note { get; set; }
        public DateOnly CreationDate { get; set; }

        // constructor
        public Secret()
        {

        }
    }
```


### *`Controller`* handles page events and navigation between pages

##### Steps to automate dynamic pages using the IDE tools:
1. Right click on the `Controllers` folder and select `Add` -> `Controller`. 
1. Select `MVC controller with views, using Entity Framework`.  Click `Add`.
1. Select Model class: `Secret (SecretsKeeper.Models)`  and add new data context type: `SecretsKeeper.Data.ApplicationDbContext`
1. Finally, click `Add`.  A lot of autogenerated code will appear under the `Controller` and `View` folders, and a new folder `Views` -> `Secrets`.
1. The folder `View` -> `Secrets` contains files { Create.cshtml, Delete.cshtml, Details.cshtml, Edit.cshtml, Index.cshtml  }.

__Console output:__

```javascript
Finding the generator 'controller'...
Running the generator 'controller'...
Minimal hosting scenario!
Attempting to compile the application in memory with the modified DbContext.
Attempting to figure out the EntityFramework metadata for the model and DbContext: 'Secret'
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 6.0.15 initialized 'ApplicationDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.15' with options: None
Added Controller : '\Controllers\SecretsController.cs'.
Added View : \Views\Secrets\Create.cshtml
Added View : \Views\Secrets\Edit.cshtml
Added View : \Views\Secrets\Details.cshtml
Added View : \Views\Secrets\Delete.cshtml
Added View : \Views\Secrets\Index.cshtml

```


 
##### Create & configure the database

Traditionally, there are at least two ways to create database, DAO and ORM.

1. __Data Access Object (DAO)__
* Manually create tables
* Traditional method of database access.
* Write your own SQL statements.
* Database managers (DBA's) usually prefer DAOs which allows greater visibility on finding problems.

2. __Object Relational Mapper (ORM)__
* Allow the computer to generate database tables based on classes defined in the application.
* Programmers prefer this method of writting SQL statements without writing SQL statements.
* Database is updated using migrations.
* e.g. Entity Framework is Microsoft's ORM.  It is simple for basic applications but not recommended for complex multi-tier application.  For that, we recommend using DAO. 

We will use the ORM method `migrate` to map views logic (a class) to the database table(s).

1. Select `Tools` -> `NuGet Package Manager` -> `Package Manager Console`
 
![](./Static/PackageManagerConsole.png)

1. In the console with prompt `PM>`, type __add-migration "initialsetup"__ and enter.
1. type __update-database__ then enter.

__Console output:__

```javascript
...

Applying migration '20230512011820_initialsetup'.
Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20230512011820_initialsetup'.
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Secret] (
          [Id] int NOT NULL IDENTITY,
          [Name] nvarchar(max) NOT NULL,
          [eMail] nvarchar(max) NOT NULL,
          [UserId] nvarchar(max) NOT NULL,
          [Password] nvarchar(max) NOT NULL,
          [Question1] nvarchar(max) NOT NULL,
          [Answer1] nvarchar(max) NOT NULL,
          [Question2] nvarchar(max) NOT NULL,
          [Answer2] nvarchar(max) NOT NULL,
          [Question3] nvarchar(max) NOT NULL,
          [Answer3] nvarchar(max) NOT NULL,
          [PIN] nvarchar(max) NOT NULL,
          [Note] nvarchar(max) NOT NULL,
          [CreationDate] datetime2 NOT NULL,
          CONSTRAINT [PK_Secret] PRIMARY KEY ([Id])
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20230512011820_initialsetup', N'6.0.15');
Done.
PM>
```

#### Verify database creation

* From the IDE nav panel, select `View` -> `SQL Server Object Explorer`
 
![](./Static/DbSecretTable.PNG) 

#### Add routes to the NavBar

1. Open file `View` -> `Shared` - `_Layout.cshtml`

1. Insert new <li> element in the between the <ul class="navbar-nav flex-grow-1"> element as follow

```javascript
<ul class="navbar-nav flex-grow-1">
    ...
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Secrets" asp-action="Index">Secrets</a>
        </li>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Secrets" asp-action="ShowSearchForm">Search</a>
        </li>
</ul>
```

1. Code the action `ShowSearchForm` in file `Controllers` -> 'SecretController.cs'

```javascript
        // GET: Secrets/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Secret != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Secret'  is null.");
        }
```

1. Create a search form:  From the code above, right-click on 'ShowSearchForm()' and select `Add View...` with the following settings:

![](./Static/AddViewForSearchForm.PNG)

* File `Views` -> `Secrets` - 'ShowSearchForm.cshtml' should be generated.

Replace its content with the following:

```javascript
<h4>Search for Secret</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="ShowSearchResults">
            <div class="form-group">
                <label for="SearchPhrase" class="control-label"></label>
                <input for="SearchPhrase" class="form-control" />
            </div>
            
            <div class="form-group">
                <input type="submit" value="Search" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

```


1. Code logic for action `ShowSearchResults` in file `Controllers` -> 'SecretsController.cs`:

Insert this code snippet:

```javascript
        // POST: Secrets/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Secret.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());
        }
```

 
* With the database connected, the Register feature should be functional at this time.

![](./Static/RegisterFunction.PNG)

* Login should work, and as long as it is, the user profile should too.

![](./Static/Login.PNG)

![](./Static/Profile.PNG)

At this point, we have a fully functional web site.

#### Display limited fields for "Index"

1. Limit the display of "Index" to relevent <li> elements and dislpay full table only when clicked on "Detail".
Edit file `Views` -> `Secrets` - "Index.cshtml" and make sure the table tag contains the following:

```javascript
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.eMail)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.UserId)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Note)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.eMail)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.UserId)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Note)
            </td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                <a asp-action="Details" asp-route-id="@item.Id">Details</a> |
                <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>
            </td>
        </tr>
}
    </tbody>
</table>
```

##### Enable 'Authorization' on SecretsController.

This enable register/login action before CRUD operations are allowed.

1.  Insert [Authorize] decorration on top of each method in file `Controlers` -> "SecretsController.cs".


### Push codebase to GIT Revision Control

1. Select `Git` -> `Create Git Repository..`
![](./Static/GitRepoPush.PNG)

### Deployment

1.  Select `Build` -> `Publish SecretsKeeper`


## Conclusion

Creating a functional ASP.NET MVS web application only takes from 20 - 30 minutes using the Microsoft Visual Studio.
