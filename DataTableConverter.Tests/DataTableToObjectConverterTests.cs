using DataTableConverter.Converters;
using DataTableConverter.Exceptions;
using DataTableConverter.Tests.Entities;
using System.Data;

namespace DataTableConverter.Tests
{
    [TestClass]
    public class DataTableToObjectConverterTests
    {
        [TestMethod]
        public void Convert_SingleRow_ReturnsNewItem()
        {
            // Arrange
            var converter = new DataTableToObjectConverter<Person>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("Age", typeof(int));

            var row = dt.NewRow();
            row["Id"] = 1;
            row["FirstName"] = "Jhon";
            row["Age"] = 10;

            dt.Rows.Add(row);

            // Act
            var person = converter.Convert(row);

            // Assert
            Assert.IsNotNull(person);
            Assert.AreEqual(person.FirstName, "Jhon");
            Assert.IsTrue(string.IsNullOrEmpty(person.LastName));
            Assert.AreEqual(person.Age, 10);
        }

        [TestMethod]
        public void Convert_DataTable_ReturnsNewList()
        {
            // Arrange
            var converter = new DataTableToObjectConverter<Person>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Age", typeof(int));

            var row = dt.NewRow();
            row["Id"] = 1;
            row["FirstName"] = "Jhon";
            row["Age"] = 32;

            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Id"] = 1;
            row["FirstName"] = "Caroline";
            row["LastName"] = "Meyer";
            row["Age"] = 60;

            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Id"] = 1;
            row["FirstName"] = "Paul";
            row["Age"] = 18;

            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Id"] = 1;
            row["FirstName"] = "Albert";
            row["LastName"] = "Wesker";
            row["Age"] = 45;

            dt.Rows.Add(row);

            // Act
            var people = converter.Convert(dt);

            // Assert
            Assert.IsNotNull(people);
            Assert.AreEqual(people.Count(), 4);
            Assert.IsTrue(!people.Any(x => x.Id > 0));
        }

        [TestMethod]
        [ExpectedException(typeof(DataTableConvertException))]
        public void TestDataTableConvertExceptionIsThrown()
        {
            // Arrange
            var converter = new DataTableToObjectConverter<Person>();

            DataTable dt = new DataTable();
            dt.Columns.Add("PersonId", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("Age", typeof(int));

            var row = dt.NewRow();
            row["PersonId"] = "id1";
            row["FirstName"] = "Jhon";
            row["Age"] = 10;

            dt.Rows.Add(row);

            // Act
            var person = converter.Convert(row);

            // Assert - ExpectedException attribute will handle the assertion
        }
    }
}
