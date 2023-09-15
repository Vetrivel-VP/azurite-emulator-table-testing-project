using Microsoft.Azure.Cosmos.Table;

namespace SampleEmulatorTry.Model
{
    public class PersonEntity  : TableEntity
    {
        public PersonEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public PersonEntity() { } // Default constructor is required for TableEntity

        public string Age { get; set; }
    }
}
