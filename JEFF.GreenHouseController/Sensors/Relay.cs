using JEFF.Dto.Smartables.Request;
using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// Relay
    /// </summary>
    public class Relay : OutputPort<RelayRequestDto, PortUpdateResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Relay"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public Relay(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public new bool Value { get; set; }

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public override async Task<PortUpdateResponse> Write()
        {
            return await WriteChannel(new RelayRequestDto
            {
                dig = Value ? 1 : 0
            });
        }
    }
}
