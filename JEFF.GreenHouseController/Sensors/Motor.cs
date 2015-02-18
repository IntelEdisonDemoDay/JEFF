using JEFF.Dto.Smartables.Request;
using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using System;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// Motor
    /// </summary>
    public class Motor : OutputPort<MotorRequestDto, PortUpdateResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Motor"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public Motor(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        public int Angle { get; set; }

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <returns></returns>
        public override async Task<PortUpdateResponse> Write()
        {
            // 10 = 0
            // 50 = 180
            Angle = Math.Max(0, Math.Min(Angle, 180));
            var value = (40.0 * (Angle / 180.0)) + 10.0;

            return await WriteChannel(new MotorRequestDto
            {
                pwm = new[] { 200, value }
            });
        }
    }
}
