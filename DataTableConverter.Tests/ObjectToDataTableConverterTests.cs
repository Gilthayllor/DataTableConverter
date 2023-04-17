using DataTableConverter.Converters;
using DataTableConverter.Exceptions;
using DataTableConverter.Tests.Entities;
using DataTableConverter.Tests.Helpers;
using System.Data;

namespace DataTableConverter.Tests.Net
{
    [TestClass]
    public class ObjectToDataTableConverterTests
    {
        [TestMethod]
        public void ConvertValidList_ReturnsNewDataTable()
        {
            // Arrange
            var converter = new ObjectToDataTableConverter<Person>();

            List<Person> list = new List<Person>()
            {
                new Person(1, "John", "Doe", 30),
                new Person(2, "Jane", null, 25),
                new Person(3, "Bob", "Smith", 45),
                new Person(4, "Alice", "Johnson", 50),
                new Person(5, "Tom", null, 28)
            };

            // Act
            var result = converter.Convert(list);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Rows.Count);
            Assert.AreEqual("PersonId", result.Columns[0].ColumnName);
            Assert.AreEqual("FirstName", result.Columns[1].ColumnName);
            Assert.AreEqual("LastName", result.Columns[2].ColumnName);
            Assert.AreEqual("Age", result.Columns[3].ColumnName);

            // Log Result
            System.Diagnostics.Debug.WriteLine("DataTable contents:");
            System.Diagnostics.Debug.WriteLine(result.ConvertToString());
        }

        [TestMethod]
        public void Convert_ListWithExcludedColumns_ExcludesColumnsFromDataTable()
        {
            // Arrange
            var converter = new ObjectToDataTableConverter<Customer>();

            List<Customer> list = new List<Customer>()
            {
                new Customer(1, "John"),
                new Customer(2, "Jane")
            };

            // Act
            var result = converter.Convert(list);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Columns["Name"]);
            Assert.IsNull(result.Columns["Id"]);

            // Log Result
            System.Diagnostics.Debug.WriteLine("DataTable contents:");
            System.Diagnostics.Debug.WriteLine(result.ConvertToString());
        }

        [TestMethod]
        public void Convert_OneItem()
        {
            // Arrange
            var converter = new ObjectToDataTableConverter<Person>();

            var person = new Person(1, "Albert", "Wesker", 55);

            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("OtherColumn", typeof(string));
            dt.Columns.Add("Age", typeof(string));

            // Act
            var result = converter.Convert(dt, person);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(Convert.ToString(result["FirstName"])));
            Assert.IsTrue(string.IsNullOrEmpty(Convert.ToString(result["OtherColumn"])));
            Assert.IsFalse(string.IsNullOrEmpty(Convert.ToString(result["Age"])));

            // Log Result
            System.Diagnostics.Debug.WriteLine("DataTable contents:");
            System.Diagnostics.Debug.WriteLine(result.ConvertToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NoMatchingColumnException))]
        public void TestNoMatchingColumnExceptionIsThrown()
        {
            // Arrange
            var converter = new ObjectToDataTableConverter<Person>();
            var dataTable = new DataTable();

            var person = new Person();

            // Act
            converter.Convert(dataTable, person);

            // Act
            var result = converter.Convert(dataTable, person);

            // Assert - ExpectedException attribute will handle the assertion
        }
    }
}