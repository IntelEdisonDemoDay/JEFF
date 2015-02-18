using JEFF.GreenHouseController.Sensors;

namespace JEFF.GreenHouseController
{
    /// <summary>
    /// GreenHouse
    /// </summary>
    public class GreenHouse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreenHouse" /> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public GreenHouse(SmartablesBoard board)
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
        /// Gets or sets the light1.
        /// </summary>
        /// <value>
        /// The light1.
        /// </value>
        public LightSensor Light1 { get; set; }

        /// <summary>
        /// Gets or sets the water1.
        /// </summary>
        /// <value>
        /// The water1.
        /// </value>
        public WaterSensor Water1 { get; set; }

        /// <summary>
        /// Gets or sets the temp1.
        /// </summary>
        /// <value>
        /// The temp1.
        /// </value>
        public TempMoistureSensor TempMoist1 { get; set; }

        /// <summary>
        /// Gets or sets the relay1.
        /// </summary>
        /// <value>
        /// The relay1.
        /// </value>
        public Relay Relay1 { get; set; }

        /// <summary>
        /// Gets or sets the motor1.
        /// </summary>
        /// <value>
        /// The motor1.
        /// </value>
        public Motor Motor1 { get; set; }

        /// <summary>
        /// Gets or sets the RGB1.
        /// </summary>
        /// <value>
        /// The RGB1.
        /// </value>
        public Rgb Rgb1 { get; set; }

    }
}
