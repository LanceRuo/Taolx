using System.Diagnostics;

namespace Taolx.Common.DataAccess
{
    /// <summary>
    /// A class representing a property map
    /// </summary>
    [DebuggerDisplay("Property: {PropertyName}, Column: {ColumnName}")]
    public class PropertyMap
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsKey { set; get; }

        /// <summary>
        /// 是否自动生成主键
        /// </summary>
        public bool IsStoreGeneratedIdentity { set; get; }
    }
}