using Microsoft.Azure.Cosmos.Table;

namespace SampleEmulatorTry.Model
{
    public class MyEntity : TableEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
