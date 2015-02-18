
namespace JEFF.Dto.Smartables.Request
{
    /// <summary>
    /// RgbRequestDto
    /// </summary>
    public class RgbRequestDto : IPortUpdateDto
    {
        /// <summary>
        /// Gets or sets the PWM.
        /// </summary>
        /// <value>
        /// The PWM.
        /// </value>
        public int[] rgb { get; set; }
    }
}
