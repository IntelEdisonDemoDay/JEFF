using JEFF.Dto.Smartables.Response;
using JEFF.Models;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors.Base
{
    /// <summary>
    /// InputPort
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class InputPort<T> : BasePort where T : class, IPortResponseDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputPort{T}"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public InputPort(SmartablesBoard board)
            : base(board)
        {
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public new SensorType Type
        {
            get
            {
                return SensorType.IN;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        protected async Task<T> ReadChannel()
        {
            return await Board.Read<T>(ChannelName);
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public abstract Task<SensorReading[]> Read();
    }
}
