using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using JEFF.Models;
using System;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// LightSensor
    /// </summary>
    public class LightSensor : InputPort<LightResponseDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightSensor"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public LightSensor(SmartablesBoard board)
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
            LightResponseDto light = await ReadChannel();
            return new SensorReading[] { new SensorReading
            {
                BoardId = this.Board.BoardId,
                SensorId = this.Name,
                TimeStamp = DateTime.UtcNow,
                Value = light.analog,
                IsValid = light.IsValid
            }};
        }
    }
}
