using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpORM.Mapping
{
    /// <summary>
    /// Based on SimpleCRUD
    /// </summary>
    public enum KeyType
    {
        /// <summary>
        /// The column is not a key (default)
        /// </summary>
        NotAKey = 0,

        /// <summary>
        /// The column is identity
        /// </summary>
        Identity = 1,

        /// <summary>
        /// The key is a guid
        /// </summary>
        Guid = 2,

        /// <summary>
        /// The key is assigned
        /// </summary>
        Assigned = 3
    }
}
