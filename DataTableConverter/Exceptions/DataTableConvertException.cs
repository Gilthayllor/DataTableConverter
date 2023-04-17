using System;

namespace DataTableConverter.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs during the conversion of a <see cref="System.Data.DataTable"/> to an object or a list of objects.
    /// </summary>
    public class DataTableConvertException : Exception
    {
        public DataTableConvertException(string message) : base(message) { }

        public DataTableConvertException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public DataTableConvertException(Exception innerException) : this("An exception occurred while attempting to convert the object. See the inner exception for more details.", innerException)
        {

        }
    }
}
