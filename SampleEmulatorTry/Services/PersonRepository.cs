using Microsoft.Azure.Cosmos.Table;
using SampleEmulatorTry.Interfaces;
using SampleEmulatorTry.Model;

namespace SampleEmulatorTry.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly CloudTable _table;

        public PersonRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorageConnection");
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference("People"); // Replace with your table name
            _table.CreateIfNotExists();
        }

        public async Task InsertAsync(PersonEntity entity)
        {
            var insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
