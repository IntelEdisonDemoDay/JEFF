
namespace JEFF.Dto.Smartables.Response
{
    /// <summary>
    /// TempMoistureResponseDto
    /// </summary>
    public class TempMoistureResponseDto : SmartableBaseResponse, IPortResponseDto
    {
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temporary.
        /// </value>
        public float temp { get; set; }

        /// <summary>
        /// Gets or sets the hum.
        /// </summary>
        /// <value>
        /// The hum.
        /// </value>
        public float hum { get; set; }
    }
}
