using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using Xunit;

namespace TakeASeat_Tests.Data
{
    public class DatabaseContextMock
    {
        public async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDB")
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
