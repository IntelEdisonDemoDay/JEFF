using JEFF.Models;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace JEFF.DataAccess
{
    /// <summary>
    /// MongoContext
    /// </summary>
    public class MongoContext
    {
        Lazy<MongoDatabase> _lazyDb = new Lazy<MongoDatabase>(() =>
        {
            MongoDatabase db;

            var connectionString = ConfigurationManager.ConnectionStrings["IOT-TEST"].ConnectionString;
            var server = new MongoClient(connectionString).GetServer();
            db = server.GetDatabase("iot-test");
            return db;
        });

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext" /> class.
        /// </summary>
        public MongoContext()
        { }

        /// <summary>
        /// Gets the db.
        /// </summary>
        /// <value>
        /// The db.
        /// </value>
        public MongoDatabase Db
        {
            get { return _lazyDb.Value; }
        }

        /// <summary>
        /// Gets the sensor readings.
        /// </summary>
        /// <value>
        /// The sensor readings.
        /// </value>
        public MongoCollection<SensorReading> SensorReadings
        {
            get { return this.Db.GetCollection<SensorReading>("sensor_reading"); }
        }
    }
}
