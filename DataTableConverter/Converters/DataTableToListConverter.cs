using DataTableConverter.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataTableConverter.Converters
{
    public class DataTableToListConverter<T> where T : new()
    {
        public DataTableToListConverter()
        {

        }

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
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> Convert(DataTable dataTable)
        {
            try
            {
                IList<T> list = new List<T>();

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
            catch (Exception)
            {
                throw;
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
