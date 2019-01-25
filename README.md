# EnumSeeder
An example of how to use Enum classes to create lookup 
tables in the database using entity framework code first.

Code first may be the best thing since sliced bread, but 
I don't care much for how it handles enums. 
Enums work just fine but if you're only looking at the 
database they don't make much sense.

So an enum like the following:

```
public enum Status {
    New,
    InProgress,
    OnHold
}
```

just get's translated to 0, 1, and 2 in the database. 
Functionally this is fine, but you'll find yourself 
constantly refrencing the enum to figure out what 
2 means in the "jobs" table.

In a more traditonal (old style) database we would have 
had a lookup table called Department with Id and Name columns. 
This table could easily be joined to the jobs table 
so we could displaye the human readable names of 
"New", "InProgress", and "OnHold" instead of 0, 1 and 2.

You could manually create and populate the table using a 
custom migration and some insert statements, but now if the 
enum needs to change you will likely have more than one place 
to change the code.

Fortunately with just a little bit of code you can create 
something that is a lot more code-first-like where all you
need to do is change the enum and everything else will
just fall in line.

So let's walk through it.
I have created 3 projects, a Web API project with swagger 
(for testing), a models project and a service project.







#### Create the new projects:

1. Create an empty Solution - [ProjectName].sln (Listed under "other project types")
2. Add a Web Api - [ProjectName].Api (.Net Core)
3. Add a models project - [ProjectName].Models (.Net Core)
4. Create The Service Layer- [ProjectName].Service (.Net Core)

Next we'll add the nuget packages:

#### Add Nuget Packages:

.Api
```
Install-Package Microsoft.Rest.ClientRunTime
Install-Package NLog.Web.AspNetCore
Install-Package Swashbuckle.AspNetCore
Install-Package Swashbuckle.AspNetCore.SwaggerUi
Install-Package Swashbuckle.AspNetCore.SwaggerGen
Install-Package Swashbuckle.AspNetCore.Annotations
Install-Package Microsoft.AspNetCore.Owin
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore 
Install-Package Microsoft.AspNetCore.WebUtilities
Install-Package NLog.Extensions.Logging
Install-Package Microsoft.IdentityModel.Tokens
```

.Models
```
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

.Service
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.AspNetCore.Owin
Install-Package Newtonsoft.json
Nlog
Install-Package System.Linq.Dynamic.Core
Install-Package Automapper
Install-Package Microsoft.AspNetCore.Mvc
```


