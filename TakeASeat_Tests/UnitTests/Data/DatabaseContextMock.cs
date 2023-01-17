using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using Xunit;

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
