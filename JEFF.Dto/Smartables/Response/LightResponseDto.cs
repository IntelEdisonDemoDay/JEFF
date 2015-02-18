
namespace JEFF.Dto.Smartables.Response
{
    /// <summary>
    /// LightResponseDto
    /// </summary>
    public class LightResponseDto : SmartableBaseResponse, IPortResponseDto
    {
        /// <summary>
        /// Gets or sets the analog.
        /// </summary>
        /// <value>
        /// The analog.
        /// </value>
        public float analog { get; set; }
    }
}
