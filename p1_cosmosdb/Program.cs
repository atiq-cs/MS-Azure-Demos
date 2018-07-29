using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace p1_cosmosdb {
  class CosmosDBDemo {
    private const string EndpointUri = "https://myapp.azure.com:myport";
    private const string PrimaryKey = "MY_PRIMARY_KEY==";
    private DocumentClient client;

    public CosmosDBDemo() {
      client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
    }

    public async Task CreateDatabaseDemo(string dbName)
    {
      await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName });
    }

    public async Task DeleteDatabaseDemo(string dbName)
    {
      // this.ExecuteSimpleQuery(dbName, "FamilyCollection");
      // await this.DeleteFamilyDocument(dbName, "FamilyCollection", "Andersen.1");
      await this.client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(dbName));
    }

    public async Task CreateCollection(string dbName, string collectionName)
    {
      await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.
        CreateDatabaseUri(dbName), new DocumentCollection { Id = collectionName });
    }

    private async Task CreateFamilyDocumentIfNotExists(string databaseName, string collectionName, Family family) {
      try {
        await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, family.Id));
        this.WriteToConsoleAndPromptToContinue("Found {0}", family.Id);
      }
      catch (DocumentClientException de) {
        if (de.StatusCode == HttpStatusCode.NotFound)
        {
            await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), family);
            this.WriteToConsoleAndPromptToContinue("Created Family {0}", family.Id);
        }
        else
        {
            throw;
        }
      }
    }

    private void ExecuteSimpleQuery(string databaseName, string collectionName)
    {
      // Set some common query options
      FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

      // Here we find the Andersen family via its LastName
      IQueryable<Family> familyQuery = this.client.CreateDocumentQuery<Family>(
        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
        .Where(f => f.LastName == "Andersen");

      // The query is executed synchronously here, but can also be executed asynchronously via the IDocumentQuery<T> interface
      Console.WriteLine("Running LINQ query...");
      foreach (Family family in familyQuery) {
          Console.WriteLine("\tRead {0}", family);
      }

      // Now execute the same query via direct SQL
      IQueryable<Family> familyQueryInSql = this.client.CreateDocumentQuery<Family>(
        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        "SELECT * FROM Family WHERE Family.LastName = 'Andersen'",
        queryOptions);

      Console.WriteLine("Running direct SQL query...");
      foreach (Family family in familyQueryInSql) {
        Console.WriteLine("\tRead {0}", family);
      }

      Console.WriteLine("Press any key to continue ...");
      Console.ReadKey();
    }

    public async Task AddDocument(string dbName, string collectionName)
    {
      Family andersenFamily = new Family {
        Id = "Andersen.1",
        LastName = "Andersen",

        Parents = new Parent[] {
          new Parent { FamilyName = "Jeffersen", FirstName = "Thomas" },
          new Parent { FirstName = "Mary Kay" }
        },
        Children = new Child[] {
          new Child {
            FirstName = "Henriette Thaulow",
            Gender = "female",
            Grade = 5,
            Pets = new Pet[] {
              new Pet { GivenName = "Fluffy" }
            }
          }
        },
        Address = new Address { State = "WA", County = "King", City = "Seattle" },
        IsRegistered = true
      };

      await this.CreateFamilyDocumentIfNotExists(dbName, collectionName, andersenFamily);

      Family wakefieldFamily = new Family {
        Id = "Wakefield.7",
        LastName = "Wakefield",
        Parents = new Parent[] {
          new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
          new Parent { FamilyName = "Miller", FirstName = "Ben" }
        },
        Children = new Child[] {
          new Child {
            FamilyName = "Merriam",
            FirstName = "Jesse",
            Gender = "female",
            Grade = 8,
            Pets = new Pet[] {
              new Pet { GivenName = "Goofy" },
              new Pet { GivenName = "Shadow" }
            }
          },
          new Child {
            FamilyName = "Miller",
            FirstName = "Lisa",
            Gender = "female",
            Grade = 1
          }
        },
        Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
        IsRegistered = false
      };

      await this.CreateFamilyDocumentIfNotExists(dbName, collectionName, wakefieldFamily);
      this.ExecuteSimpleQuery("FamilyDB", "FamilyCollection");
    }

    private void WriteToConsoleAndPromptToContinue(string format, params object[] args) {
        Console.WriteLine(format, args);
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    // internal family classes
    public class Family
    {
      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }
      public string LastName { get; set; }
      public Parent[] Parents { get; set; }
      public Child[] Children { get; set; }
      public Address Address { get; set; }
      public bool IsRegistered { get; set; }
      public override string ToString() {
        return JsonConvert.SerializeObject(this);
      }
    }

    public class Parent
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
    }

    public class Child
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int Grade { get; set; }
        public Pet[] Pets { get; set; }
    }

    public class Pet
    {
        public string GivenName { get; set; }
    }

    public class Address
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
  }


  class Program {
    static async Task Main(string[] args)
    {
      try {
        CosmosDBDemo p = new CosmosDBDemo();
        await p.CreateDatabaseDemo("FamilyDB");
        //await p.DeleteDatabaseDemo("FamilyDB");
        await p.CreateCollection("FamilyDB", "FamilyCollection");
        await p.AddDocument("FamilyDB", "FamilyCollection");
      }
      catch (DocumentClientException de) {
        Exception baseException = de.GetBaseException();
        Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
      }
      catch (Exception e) {
        Exception baseException = e.GetBaseException();
        Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
      }
      finally {
        Console.WriteLine("End of demo, press any key to exit.");
        Console.ReadKey();
      }
    }
  }
}
