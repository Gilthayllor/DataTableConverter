using DataTableConverter.Attributes;
using DataTableConverter.Exceptions;
using DataTableConverter.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableConverter.Converters
{
    public abstract class BaseConverter<T>
    {
        protected List<PropertyInfo> _properties;
        protected BaseConverter() { }

        protected IEnumerable<DataColumn> MatchColumnsDataTable(DataTable dt)
        {
            IEnumerable<PropertyInfo> properties = GetPropertiesForDataTable();
            _properties = new List<PropertyInfo>();

            ICollection<DataColumn> columns = new List<DataColumn>();

            foreach (PropertyInfo property in properties)
            {
                string columnName = property.GetColumnName();

                DataColumn column = dt.Columns.OfType<DataColumn>().FirstOrDefault(x => x.ColumnName == columnName);

                if (column != null)
                {
                    columns.Add(column);
                    _properties.Add(property);
                }
            }

            if (!columns.Any())
                throw new NoMatchingColumnException($"Could not find any matching columns in the provided DataTable for type {typeof(T).Name}. Please make sure the DataTable has the necessary columns to match the properties of {typeof(T).Name}.");

            return columns;
        }

        protected IEnumerable<PropertyInfo> GetPropertiesForDataTable()
        {
            return typeof(T).GetProperties().Where(x => x.GetCustomAttribute<ExcludeFromDataTableAttribute>() == null);
        }
    }
}
