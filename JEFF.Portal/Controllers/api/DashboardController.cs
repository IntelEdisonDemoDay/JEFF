using JEFF.DataAccess;
using JEFF.Models;
using JEFF.Portal.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace JEFF.Portal.Controllers.api
{
    /// <summary>
    /// DashboardController
    /// </summary>
    public class DashboardController : ApiController
    {
        MongoContext _context;
        MongoSensorReadingsRepository _readingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        public DashboardController()
        {
            _context = new MongoContext();
            _readingsRepository = new MongoSensorReadingsRepository(_context);
        }

        /// <summary>
        /// Gets the sensors data.
        /// </summary>
        /// <param name="sesnorId">The sesnor identifier.</param>
        /// <returns></returns>
        [ActionName("SensorsData")]
        public DashboardChartVM GetSensorsData(string sensorId)
        {
            DateTime minDate = DateTime.UtcNow.AddMinutes(-5);
            DashboardChartSeriesVM series = CreateSeries(_readingsRepository.GetBySensorId(sensorId, minDate));

            DashboardChartVM model = new DashboardChartVM();
            model.Series.Add(series);

            return model;
        }

        /// <summary>
        /// Creates the series.
        /// </summary>
        /// <param name="readings">The readings.</param>
        /// <returns></returns>
        DashboardChartSeriesVM CreateSeries(IEnumerable<SensorReading> readings)
        {
            DashboardChartSeriesVM series = new DashboardChartSeriesVM();
            foreach (SensorReading reading in readings)
            {
                series.Items.Add(new DateTimeSeriesItemVM
                {
                    Year = reading.TimeStamp.Year,
                    JSMonth = reading.TimeStamp.Month - 1, // JS months are 0 to 11
                    Day = reading.TimeStamp.Day,
                    Hours = reading.TimeStamp.Hour,
                    Minutes = reading.TimeStamp.Minute,
                    Seconds = reading.TimeStamp.Second,
                    Value = reading.Value
                });
            }

            return series;
        }
    }
}
