# Requirements

- Create API using .Net 6:
  - Authentication and authorization
  - Relational Database
  - Swagger
  - Unit test
- Create Angular project
  - At lest 5 fields of different types
  - Connects to the API
  - CRUD

# Requirements analysis

- Authentication and authorization:
  - The decision here is to use JWT because is one of the most used methods and also is the method that I use more.
- Relational Database:
  - The decision here is to use Mysql is the relational database that I use most and I'm more confident.
- Unit test:
  - I never wrote test on C# and .Net so use TDD for the development can take longer to me, so I will use xunit and Moq because I saw some examples on internet and in the documentation of microsoft.
- At lest 5 fields of different types:
  - That point made me think about what entity can have 5 different fields so I think an entity of type product can had:
    - name : string
    - price : decimal
    - dateOfCreation: DateTime
    - available: boolean
    - quantity: int

Those are the points that were important to analyze in my development process.

# Tools & resources used:

- Visual studio code:
  - C# Dev Kit extension
- TablePlus to connect to mysql database.
- Chat-gpt to sintaxis help and also errors validations. Just for speed the development process.
- [Official Microsoft documentation](https://learn.microsoft.com/es-es/aspnet/core/?view=aspnetcore-8.0&WT.mc_id=dotnet-35129-website)
- [Official Angular documentation](https://angular.dev/overview)

# Steps of development:

- Installation of .Net sdk using the [documentation](https://learn.microsoft.com/es-mx/dotnet/core/install/linux-snap-sdk).
- Reading the documentation of .Net I fiound that exist tow types of api's in .NET core the minimal and controller based. I choose to use controlled based api'es, because they have less incompatibility problems and also have more documentation.
- For the creation of the .net project I used the terminal because I was using vscode.
- Testing the Api creation I cannot saw the swagger page so checking the Program.cs configuration I found that I have to configurate the 'app.Environment' in development.
- I had some problems to connect to mysql database around the connection configuration I read the [documentation](https://learn.microsoft.com/es-es/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) to find the solution, the problem was that in the addition process of db context I wrote wrong the ProductsContext because I wrote ProductContext with the missing of the 's', so when scaffolding and create the controller I got some errors. In addition of that point I have to say that the errors messages on .Net are to long so took me some minutes to get comfortable with this.
- In the process of implementing the auth I used the next [article.](https://medium.com/@vndpal/how-to-implement-jwt-token-authentication-in-net-core-6-ab7f48470f5c)
- To enable the auth in swagger I used the next [article.](https://medium.com/@rahman3593/implementing-jwt-authentication-with-swagger-ca991b7aca08)
- I decide to not encode the Passwords because I had not much time to advance and the encoding is a easy process but need some configurations, so in way to advance in other parts I prefered to leave this in the todo list.
- I configured the program to use environment variables and create the .env example where are the definitions needed to run the program.
- The api is using localhosted mysql database.

# Final considerations:

- I focused on the backend and cannot made the frontend for time reasons.
- I had to learn and remember some concepts around C# and .NET are more funny that I remembered.
- I love the process is more focus on the quality of the development that the finished of all test.
- I had around 2 hours to work in the project and I tried to advance with a good quality way the most that I can. Testing in the middle of the way with Swagger.
- I dedicated some time to analyze before start coding and also making that Readme that try to explain all the process that I made to got this result.
