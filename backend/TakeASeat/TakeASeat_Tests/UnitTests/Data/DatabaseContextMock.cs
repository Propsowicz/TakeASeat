using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data.DatabaseContext;

namespace TakeASeat_Tests.UnitTests.Data
{
    public class DatabaseContextMock
    {
        public async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: $"MockDB-{Guid.NewGuid()}")
                .Options;

            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();
            
            return contextMock;
        }
        public async Task<DatabaseContext> GetSqliteDatabaseContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(connection)
                .Options;

            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();

            return contextMock;
        }

        public async void CleanDB()
        {
            var context = await GetDatabaseContext();
            await context.Database.EnsureDeletedAsync();
        }
    }
}
