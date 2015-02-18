using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JEFF.Models
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
