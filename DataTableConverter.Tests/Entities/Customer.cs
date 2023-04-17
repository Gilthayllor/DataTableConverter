using DataTableConverter.Attributes;

namespace DataTableConverter.Tests.Entities
{
    public class Customer
    {
        [ExcludeFromDataTable]
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
