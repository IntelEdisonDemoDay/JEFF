using JEFF.Dto.Smartables.Request;
using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors.Base;
using System.Drawing;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors
{
    /// <summary>
    /// Rgb
    /// </summary>
    public class Rgb : OutputPort<RgbRequestDto, PortUpdateResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rgb"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public Rgb(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <returns></returns>
        public override async Task<PortUpdateResponse> Write()
        {
            return await WriteChannel(new RgbRequestDto
            {
                rgb = new[] { 
                    ChannelId, 
                    Color.R,
                    Color.G,
                    Color.B
                }
            });
        }
    }
}
