using DataTableConverter.Attributes;

namespace DataTableConverter.Tests.Entities
{
    public class Person
    {
        [DataTableColumn("PersonId")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }

        [ExcludeFromDataTable]
        public int ExcludeColumn { get; set; }

        public Person(int id, string firstName, string? lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Person()
        {
            FirstName = "";
            LastName = "";
        }
    }
}
