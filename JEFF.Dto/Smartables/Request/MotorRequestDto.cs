
namespace JEFF.Dto.Smartables.Request
{
    /// <summary>
    /// MotorRequestDto
    /// </summary>
    public class MotorRequestDto : IPortUpdateDto
    {
        /// <summary>
        /// Gets or sets the PWM.
        /// </summary>
        /// <value>
        /// The PWM.
        /// </value>
        public double[] pwm { get; set; }
    }
}
