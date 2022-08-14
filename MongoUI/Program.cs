using MongoDataAccess;
using MongoDataAccess.Schemas;
using MongoDB.Driver;
using MongoUI;

string connectionString = Utils.GetConnectionString();

DataAccess<PersonSchema> db = new DataAccess<PersonSchema>(connectionString, "MyDatabase", "Contacts");

// Create Index

//var indexBuilder = Builders<PersonSchema>.IndexKeys;
//var indexModel = new CreateIndexModel<PersonSchema>(indexBuilder.Ascending(x => x.Email));
//db.db.GetCollection<PersonSchema>("Contacts").Indexes.CreateOne(indexModel);

//

bool endProgram = false;

while (!endProgram)
{
    Console.Clear();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("A) Add a person");
    Console.WriteLine("B) Remove a person");
    Console.WriteLine("C) Update a person");
    Console.WriteLine("D) Print all people");
    Console.WriteLine("X) Exit");

    string? answer = Console.ReadLine();

    Console.Clear();

    if(string.IsNullOrEmpty(answer))
    {
        Console.WriteLine("Invalid option, try again!");
        Console.ReadLine();
        continue;
    }

    bool isGuid = false;

    switch (answer.ToLower())
    {
        case "a" or "n":

            PersonSchema personToAdd = new();

            Console.WriteLine("First name:");
            personToAdd.FirstName = Console.ReadLine();

            Console.WriteLine("Last name:");
            personToAdd.LastName = Console.ReadLine();

            Console.WriteLine("Phone number:");
            personToAdd.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Email address:");
            personToAdd.Email = Console.ReadLine();

            db.Insert(personToAdd);

            Console.WriteLine("Person added successfully!");
            Console.ReadLine();

            break;

        case "b" or "r":

            while(!isGuid)
            {
                Console.Clear();
                Console.WriteLine("Give the person Id to delete or X to cancel:");

                string? personToRemoveId = Console.ReadLine();

                if(string.IsNullOrEmpty(personToRemoveId))
                {
                    Console.WriteLine("Invalid guid!");
                    Console.ReadLine();
                    continue;
                }

                if(personToRemoveId.ToLower() == "x")
                {
                    break;
                }

                isGuid = Guid.TryParse(personToRemoveId, out Guid id);

                if(!isGuid)
                {
                    Console.WriteLine("Invalid guid!");
                    Console.ReadLine();
                }
                else
                {
                    db.Delete(id);
                    Console.WriteLine("Successfully deleted person");
                    Console.ReadLine();
                }
            }

            break;

        case "c" or "u":

            while (!isGuid)
            {
                Console.Clear();
                Console.WriteLine("Give the person Id to update or X to cancel:");

                string? personToRemoveId = Console.ReadLine();

                if (string.IsNullOrEmpty(personToRemoveId))
                {
                    Console.WriteLine("Invalid guid!");
                    Console.ReadLine();
                    continue;
                }

                if (personToRemoveId.ToLower() == "x")
                {
                    break;
                }

                isGuid = Guid.TryParse(personToRemoveId, out Guid id);

                if (!isGuid)
                {
                    Console.WriteLine("Invalid guid!");
                    Console.ReadLine();
                }
                else
                {
                    PersonSchema personToUpdate = new PersonSchema
                    {
                        Id = id
                    };

                    Console.WriteLine("First name:");
                    personToUpdate.FirstName = Console.ReadLine();

                    Console.WriteLine("Last name:");
                    personToUpdate.LastName = Console.ReadLine();

                    Console.WriteLine("Phone number:");
                    personToUpdate.PhoneNumber = Console.ReadLine();

                    Console.WriteLine("Email address:");
                    personToUpdate.Email = Console.ReadLine();

                    db.Upsert(personToUpdate);

                    Console.WriteLine("Successfully updated person");
                    Console.ReadLine();
                }
            }

            break;

        case "d" or "p":

            Console.WriteLine("List of all people");
            Console.WriteLine("*****************");
            Console.WriteLine("Id : First name : Last name : Phone Number : Email");

            db.FindAll().ForEach(p =>
            {
                Console.WriteLine($"{ p.Id } : { p.FirstName } : { p.LastName } : { p.PhoneNumber } : { p.Email }");
            });

            Console.ReadLine();

            break;

        case "x" or "q":

            endProgram = true;

            break;

        default:

            Console.WriteLine("Invalid option, try again!");
            Console.ReadLine();

            break;
    }
}