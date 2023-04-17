using DataTableConverter.Attributes;
using DataTableConverter.Exceptions;
using DataTableConverter.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataTableConverter.Converters
{
    /// <summary>
    /// Converts an object of type <typeparamref name="T"/> into a <see cref="DataTable"/>, matching its properties to columns in a <see cref="DataTable"/>.
    /// </summary>
    public class ObjectToDataTableConverter<T> : BaseConverter<T> where T : class
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ObjectToDataTableConverter{T}"/> class.
        /// </summary>
        public ObjectToDataTableConverter() { }

        /// <summary>
        /// Converts an object of type <typeparamref name="T"/> into a <see cref="DataRow"/>, matching its properties to columns in a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="dataTable">The <see cref="DataTable"/> to match columns to.</param>
        /// <param name="item">The object to convert into a <see cref="DataRow"/>.</param>
        /// <returns>The resulting <see cref="DataRow"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either the dataTable or item parameter is null.</exception>
        /// <exception cref="NoMatchingColumnException">Thrown when no matching columns are found in the DataTable.</exception>
        /// <exception cref="DataTableConvertException">Thrown when an exception is encountered during the conversion process.</exception>
        /// <remarks>
        /// The <typeparamref name="T"/> type must have public properties that match the column names of the <see cref="DataTable"/>.
        /// </remarks>
        public DataRow Convert(DataTable dataTable, T item)
        {
            if (dataTable == null)
                throw new ArgumentNullException("dataTable", "The dataTable parameter cannot be null.");
            if (item == null)
                throw new ArgumentNullException("item", "The item parameter cannot be null.");

            try
            {
                IEnumerable<DataColumn> columns = MatchColumnsDataTable(dataTable);

                DataRow dataRow = dataTable.NewRow();

                foreach (DataColumn column in columns)
                {
                    SetPropertyDataTable(dataRow, column, item);
                }

                return dataRow;
            }
            catch (NoMatchingColumnException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataTableConvertException(e);
            }
        }

        /// <summary>
        /// Converts a list of objects of type <typeparamref name="T"/> into a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="list">The list of objects to convert into a <see cref="DataTable"/>.</param>
        /// <returns>The resulting <see cref="DataTable"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the list parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the list parameter is empty.</exception>
        /// <exception cref="Exception">Thrown when an exception is encountered during the conversion process.</exception>
        public DataTable Convert(IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list", "The list parameter cannot be null.");
            if (!list.Any())
                throw new ArgumentException("The list parameter cannot be empty.", "list");

            try
            {
                DataTable dataTable = BuildDataTable();

                foreach (T item in list)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        SetPropertyDataTable(dataRow, column, item);
                    }

                    dataTable.Rows.Add(dataRow);
                }

                return dataTable;
            }
            catch (Exception e)
            {
                throw new DataTableConvertException(e);
            }
        }

        private DataTable BuildDataTable()
        {
            DataTable dt = new DataTable();
            _properties = new List<PropertyInfo>();

            foreach (PropertyInfo property in GetPropertiesForDataTable())
            {
                string columnName = property.GetColumnName();

                dt.Columns.Add(columnName, property.PropertyType);

                _properties.Add(property);
            }

            return dt;
        }

        private void SetPropertyDataTable(DataRow dataRow, DataColumn column, T item)
        {
            PropertyInfo property = _properties.Where(x => x.GetColumnName().Equals(column.ColumnName)).FirstOrDefault();

            dataRow[column] = item.GetType().GetProperty(property.Name).GetValue(item, null);
        }
    }
}
