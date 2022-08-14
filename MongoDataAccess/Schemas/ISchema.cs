namespace MongoDataAccess.Schemas
{
    public interface ISchema
    {
        public string Table { get; }

        public Guid Id { get; set; }
    }
}
