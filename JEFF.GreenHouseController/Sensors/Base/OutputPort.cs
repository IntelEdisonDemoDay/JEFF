using JEFF.Dto.Smartables.Request;
using JEFF.Dto.Smartables.Response;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController.Sensors.Base
{
    /// <summary>
    /// OutputPort
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OutputPort<T, R> : BasePort
        where T : class, IPortUpdateDto
        where R : class, IPortResponseDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputPort{T}"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public OutputPort(SmartablesBoard board)
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
                return SensorType.OUT;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        protected async Task<R> WriteChannel(T body)
        {
            return await Board.Write<T, R>(ChannelName, body);
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public abstract Task<R> Write();
    }
}
