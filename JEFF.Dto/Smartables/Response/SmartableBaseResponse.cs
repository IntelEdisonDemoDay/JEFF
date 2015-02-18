
namespace JEFF.Dto.Smartables.Response
{
    /// <summary>
    /// SmartableBaseResponse
    /// </summary>
    public class SmartableBaseResponse : IPortResponseDto
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string message { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return !(message ?? string.Empty).Equals("rate limit exceeded");
            }
        }
    }
}
