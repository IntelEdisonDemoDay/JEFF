
namespace JEFF.GreenHouseController.Sensors.Base
{
    /// <summary>
    /// BasePort
    /// </summary>
    public class BasePort
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePort"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public BasePort(SmartablesBoard board)
        {
            Board = board;
        }

        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        /// <value>
        /// The board.
        /// </value>
        public SmartablesBoard Board { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>
        /// The name of the channel.
        /// </value>
        public string ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SensorType Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }


    }

    /// <summary>
    /// SensorType
    /// </summary>
    public enum SensorType
    {
        IN,
        OUT
    }
}
