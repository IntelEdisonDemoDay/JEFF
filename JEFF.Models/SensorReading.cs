using MongoDB.Bson.Serialization.Attributes;
using System;

namespace JEFF.Models
{
    /// <summary>
    /// SensorReading
    /// </summary>
    public class SensorReading : BaseEntity
    {
        /// <summary>
        /// Gets or sets the board id.
        /// </summary>
        /// <value>
        /// The board id.
        /// </value>
        public string BoardId { get; set; }

        /// <summary>
        /// Gets or sets the sensor identifier.
        /// </summary>
        /// <value>
        /// The sensor identifier.
        /// </value>
        public string SensorId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        [BsonIgnore]
        public bool IsValid { get; set; }
    }
}
