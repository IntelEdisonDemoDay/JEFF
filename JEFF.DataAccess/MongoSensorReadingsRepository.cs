using JEFF.Models;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;

namespace JEFF.DataAccess
{
    /// <summary>
    /// MongoSensorReadingsRepository
    /// </summary>
    public class MongoSensorReadingsRepository : MongoBaseRepository<SensorReading>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoSensorReadingsRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MongoSensorReadingsRepository(MongoContext context) :
            base(context.SensorReadings)
        { }

        /// <summary>
        /// Gets the by sensor identifier.
        /// </summary>
        /// <param name="sensorId">The sensor identifier.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <returns></returns>
        public virtual IEnumerable<SensorReading> GetBySensorId(string sensorId, DateTime minDate)
        {
            var query = Query.And(Query<SensorReading>.EQ(m => m.SensorId, sensorId),
              Query<SensorReading>.GT(m => m.TimeStamp, minDate));

            return Collection.Find(query);
        }
    }
}
