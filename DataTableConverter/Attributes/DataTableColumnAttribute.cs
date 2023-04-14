using System;

namespace DataTableConverter.Attributes
{
    /// <summary>
    /// Attribute used to specify the name of the column in the <see cref="System.Data.DataTable"/> that corresponds to the decorated property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataTableColumnAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the column in the <see cref="System.Data.DataTable"/> that corresponds to the decorated property.
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableColumnAttribute"/> class with the specified column name.
        /// </summary>
        /// <param name="columnName">The name of the column in the DataTable that corresponds to the decorated property.</param>
        public DataTableColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }

    }
}
