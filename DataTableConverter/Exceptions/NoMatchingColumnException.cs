using System;

namespace DataTableConverter.Exceptions
{
    /// <summary>
    /// Exception thrown when no matching column is found in a <see cref="System.Data.DataTable"/> while converting to an object or a list of objects.
    /// </summary>
    public class NoMatchingColumnException : Exception
    {
        public NoMatchingColumnException(string message) : base(message) { }
    }
}
