using DataTableConverter.Exceptions;
using DataTableConverter.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataTableConverter.Converters
{
    /// <summary>
    /// Converts a <see cref="DataTable"/> to a list of objects or a single object of a given type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of object to be created from the DataTable.</typeparam>
    public class DataTableToObjectConverter<T> where T : new()
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DataTableToObjectConverter{T}"/> class.
        /// </summary>
        public DataTableToObjectConverter() { }

        /// <summary>
        /// Converts a single DataRow to an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="row">The <see cref="DataRow"/> to be converted.</param>
        /// <returns>The converted instance of type <typeparamref name="T"/>.</returns>
        /// <exception cref="DataTableConvertException">Thrown when an exception is encountered during the conversion process.</exception>
        public T Convert(DataRow row)
        {
            try
            {
                var instance = (T)Activator.CreateInstance(typeof(T));

                List<DataColumn> dataColumns = row.Table.GetColumns();

                PropertyInfo[] properties = instance.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    SetPropertyInstance(property, dataColumns, row, instance, 0);
                }

                return instance;
            }
            catch (Exception e)
            {
                throw new DataTableConvertException(e);
            }
        }

        /// <summary>
        /// Converts a <see cref="DataTable"/> to a list of objects of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="dataTable">The <see cref="DataTable"/> to be converted.</param>
        /// <returns>A list of objects of type <typeparamref name="T"/> created from the <see cref="DataTable"/>.</returns>
        /// /// <exception cref="DataTableConvertException">Thrown when an exception is encountered during the conversion process.</exception>
        public IEnumerable<T> Convert(DataTable dataTable)
        {
            try
            {
                ICollection<T> list = new List<T>();

                List<DataColumn> dataColumns = dataTable.GetColumns();

                int rowIndex = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));

                    PropertyInfo[] properties = instance.GetType().GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        SetPropertyInstance(property, dataColumns, row, instance, rowIndex);
                    }

                    list.Add(instance);

                    rowIndex++;
                }

                return list;
            }
            catch (Exception e)
            {
                throw new DataTableConvertException(e);
            }
        }

        private void SetPropertyInstance(PropertyInfo property, List<DataColumn> dataColumns, DataRow row, T instance, int rowIndex)
        {
            string columnName = property.GetColumnName();

            DataColumn dataColumn = dataColumns.GetColumn(columnName);

            if (dataColumn != null)
            {
                object value = row[dataColumn];

                try
                {
                    var nullableType = Nullable.GetUnderlyingType(property.PropertyType);

                    if (nullableType != null)
                        property.SetValue(instance, System.Convert.ChangeType(value, nullableType));
                    else
                        property.SetValue(instance, System.Convert.ChangeType(value, property.PropertyType));
                }
                catch (InvalidCastException e)
                {
                    throw new Exception($"Error converting value of column {property.Name} at row {rowIndex}. Error details: {e.Message}", e);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
