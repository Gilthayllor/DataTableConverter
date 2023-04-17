using System.Data;
using System.Text;

namespace DataTableConverter.Tests.Helpers
{
    internal static class TestHelper
    {
        internal static string ConvertToString(this DataTable dataTable)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn column in dataTable.Columns)
            {
                sb.AppendFormat("{0}\t", column.ColumnName);
            }
            sb.AppendLine();

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    sb.AppendFormat("{0}\t", row[column]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string ConvertToString(this DataRow row)
        {
            var sb = new StringBuilder();
            foreach (DataColumn column in row.Table.Columns)
            {
                sb.AppendFormat("{0}: {1}; ", column.ColumnName, row[column]);
            }

            return sb.ToString().TrimEnd(' ', ';');
        }
    }
}
