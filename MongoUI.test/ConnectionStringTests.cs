using MongoUI;

namespace MongoUI.test
{
    public class ConnectionStringTests
    {
        [Fact]
        public void TestConnectionString()
        {
            string connectionString = Utils.GetConnectionString("Echo");
            Assert.Equal("Echo", connectionString);
        }
    }
}