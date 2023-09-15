using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using SampleEmulatorTry.Model;
using System.Threading.Tasks;

namespace SampleEmulatorTry.Services
{
    public class MyEntityRepository
    {
        private readonly CloudTable _table;

        public MyEntityRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorageConnection");
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference("MyTable"); // Replace with your table name
            _table.CreateIfNotExists();
        }

        public async Task<MyEntity> CreateAsync(MyEntity entity)
        {
            var insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
            return entity;
        }

        public async Task<MyEntity> GetByIdAsync(string partitionKey, string rowKey)
        {
            var retrieveOperation = TableOperation.Retrieve<MyEntity>(partitionKey, rowKey);
            var result = await _table.ExecuteAsync(retrieveOperation);
            return (MyEntity)result.Result;
        }

        public async Task<MyEntity> UpdateAsync(MyEntity entity)
        {
            var replaceOperation = TableOperation.Replace(entity);
            await _table.ExecuteAsync(replaceOperation);
            return entity;
        }

        public async Task DeleteAsync(string partitionKey, string rowKey)
        {
            var deleteOperation = TableOperation.Delete(new TableEntity(partitionKey, rowKey) { ETag = "*" });
            await _table.ExecuteAsync(deleteOperation);
        }
    }
}
