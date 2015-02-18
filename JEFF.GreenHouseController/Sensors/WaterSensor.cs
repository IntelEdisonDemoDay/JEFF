using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using JEFF.Models;
using System;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// WaterSensor
    /// </summary>
    public class WaterSensor : InputPort<WaterResponseDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaterSensor"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public WaterSensor(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async override Task<SensorReading[]> Read()
        {
            WaterResponseDto water = await ReadChannel();
            return new SensorReading[] { new SensorReading
            {
                BoardId = this.Board.BoardId,
                SensorId = this.Name,
                TimeStamp = DateTime.UtcNow,
                Value = water.dig,
                IsValid = water.IsValid
            }};
        }
    }
}
