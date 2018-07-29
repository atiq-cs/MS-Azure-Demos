# Microsoft Azure CosmosDB Demo
First demo of microsoft azure is this one.

To create a console cosmosdb project we do,

    $ dotnet new console -n p1_cosmosdb
    
We can run it with code,

    $ code .

Or Visual Studio,    

    $ start devenv .

We add a package named `Microsoft.Azure.DocumentDB.Core` which specifically for
.net core applications,
    
    $ dotnet add package Microsoft.Azure.DocumentDB.Core
      Writing C:\Users\atiqc\AppData\Local\Temp\tmp2D2F.tmp
    info : Adding PackageReference for package 'Microsoft.Azure.DocumentDB.Core' into project 'D:\p1_cosmosdb\p1_cosmosdb.csproj'.
    log  : Restoring packages for D:\p1_cosmosdb\p1_cosmosdb.csproj...
    info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.azure.documentdb.core/index.json
    info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.azure.documentdb.core/index.json 91ms
    info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.azure.documentdb.core/1.10.0/microsoft.azure.documentdb.core.1.10.0.nupkg
    info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.azure.documentdb.core/1.10.0/microsoft.azure.documentdb.core.1.10.0.nupkg 248ms
    log  : Installing Microsoft.Azure.DocumentDB.Core 1.10.0.
    info : Package 'Microsoft.Azure.DocumentDB.Core' is compatible with all the specified frameworks in project 'D:\p1_cosmosdb\p1_cosmosdb.csproj'.
    info : PackageReference for package 'Microsoft.Azure.DocumentDB.Core' version '1.10.0' added to file 'D:\p1_cosmosdb\p1_cosmosdb.csproj'.

We gradually implement methods to create database, collection, document and adding/modifying documents.

Demo code on the reference uses `Wait()` which I replace using await and I make main method async.

    static void Main(string[] args)
    {
      try
      {
        Program p = new Program();
        p.GetStartedDemo().Wait();
      }
      catch (DocumentClientException de)
      {
        ...

I change above to following,

    static async Task Main(string[] args)
    {
      try {
        CosmosDBDemo p = new CosmosDBDemo();
        await p.CreateDatabaseDemo("FamilyDB");
        ...
        
To be able to change main method to `async` we have to change lang version,

    <PropertyGroup>
      ...
      <LangVersion>Latest</LangVersion>
    </PropertyGroup>

Finally we build/run the project,
    
    $ dotnet run
    Microsoft (R) Build Engine version 15.7.179.6572 for .NET Core
    Copyright (C) Microsoft Corporation. All rights reserved.

      Restoring packages for D:\p1_cosmosdb\p1_cosmosdb.csproj...
      Restore completed in 546.67 ms for D:\p1_cosmosdb\p1_cosmosdb.csproj.
      p1_cosmosdb -> D:\p1_cosmosdb\bin\Debug\netcoreapp2.1\p1_cosmosdb.dll

    Build succeeded.
        0 Warning(s)
        0 Error(s)

    Time Elapsed 00:00:02.85

    End of demo, press any key to exit.

Operations such as,

 - Creating Database
 - Creating Collection

etc are idempotent. It means running them more than once does not produce an
error and might have no different effect.

## Summary for API
Here is a brief overview,

 - Creating Database is done using `CreateDatabaseIfNotExistsAsync`
 - Delete Database is performed using `DeleteDatabaseAsync`
 - Creating Collection is performed using `CreateDocumentCollectionIfNotExistsAsync`.
 - Document is created using `CreateDocumentAsync`.
 - Query is generated using `CreateDocumentQuery`.

Using exception status code we can check.. if URL was not found..

    de.StatusCode == HttpStatusCode.NotFound

## References
 - Primary reference - [Azure Cosmos DB: Getting started with the SQL API and .NET Core][1]
 - On cosmos db, [how it evolved from document db][2]


  [1]: https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-dotnetcore-get-started
  [2]: https://stackoverflow.com/questions/43932359/what-are-the-differences-between-cosmodb-and-documentdb
