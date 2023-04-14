using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableConverter.Extensions
{
    internal static class DataTableExtensions
    {
        internal static DataColumn GetColumn(this List<DataColumn> cols, string colName)
        {
            return cols.FirstOrDefault(c => c.ColumnName == colName);
        }

        internal static List<DataColumn> GetColumns(this DataTable dataTable)
        {
            return dataTable.Columns.OfType<DataColumn>().ToList();
        }

        internal static List<DataColumn> GetColumns(this DataRow row)
        {
            return row.Table.Columns.OfType<DataColumn>().ToList();
        }
    }
}
