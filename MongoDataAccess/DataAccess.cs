using MongoDataAccess.Schemas;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDataAccess
{
    public class DataAccess<T>
        where T : ISchema
    {
        private MongoClient dbClient;
        public IMongoDatabase db { get; }
        private readonly string table;

        public DataAccess(string connectionString, string database, string table)
        {
            dbClient = new MongoClient(connectionString);
            db = dbClient.GetDatabase(database);
            this.table = table;
        }

        public List<T> FindAll() => db.GetCollection<T>(table).Find(new BsonDocument()).ToList();

        public void Insert(T schema) => db.GetCollection<T>(table).InsertOne(schema);

        public void Upsert(T schema) => 
            db.GetCollection<T>(table)
            .ReplaceOne(
                new BsonDocument("_id", new BsonBinaryData(schema.Id, GuidRepresentation.Standard)), 
                schema, 
                new ReplaceOptions { IsUpsert = true });

        public void InsertMany(List<T> schemaList) => db.GetCollection<T>(table).InsertMany(schemaList);

        public void Delete(T schema) => db.GetCollection<T>(table).DeleteOne(Builders<T>.Filter.Eq("Id", schema.Id));
        public void Delete(Guid id) => db.GetCollection<T>(table).DeleteOne(Builders<T>.Filter.Eq("Id", id));

        public void DeleteMany(List<T> schemaList) => schemaList.ForEach(x => Delete(x));
    }
}