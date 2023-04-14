using System;

namespace DataTableConverter.Attributes
{
    /// <summary>
    /// Indicates that a property should be excluded when converting an object to a DataTable.
    /// </summary>
    public class ExcludeFromDataTableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeFromDataTableAttribute"/> class.
        /// </summary>
        public ExcludeFromDataTableAttribute() { }
    }
}
