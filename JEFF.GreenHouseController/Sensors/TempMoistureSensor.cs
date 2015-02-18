using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using JEFF.Models;
using System;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// TempMoistureSensor
    /// </summary>
    public class TempMoistureSensor : InputPort<TempMoistureResponseDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempMoistureSensor"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public TempMoistureSensor(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public async override Task<SensorReading[]> Read()
        {
            TempMoistureResponseDto reading = await ReadChannel();
            SensorReading temp = new SensorReading
            {
                BoardId = this.Board.BoardId,
                SensorId = this.Name + " - Temperature",
                TimeStamp = DateTime.UtcNow,
                Value = reading.temp,
                IsValid = reading.IsValid
            };
            SensorReading hum = new SensorReading
            {
                BoardId = this.Board.BoardId,
                SensorId = this.Name + " - Humidity",
                TimeStamp = DateTime.UtcNow,
                Value = reading.hum,
                IsValid = reading.IsValid
            };

            return new SensorReading[] { temp, hum };
        }
    }
}
