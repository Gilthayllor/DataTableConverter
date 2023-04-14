using DataTableConverter.Attributes;
using System.Reflection;

namespace DataTableConverter.Extensions
{
    internal static class PropertyInfoExtensions
    {
        internal static string GetColumnName(this PropertyInfo property)
        {
            string columnName = property.Name;

            DataTableColumnAttribute columnAttribute = property.GetCustomAttribute<DataTableColumnAttribute>();

            if (columnAttribute != null && !string.IsNullOrWhiteSpace(columnAttribute.ColumnName))
                columnName = columnAttribute.ColumnName;

            return columnName;
        }
    }
}
