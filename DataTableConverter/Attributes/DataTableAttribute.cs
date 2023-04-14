using System;

namespace DataTableConverter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataTableColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public DataTableColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
