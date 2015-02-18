using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JEFF.Portal.Startup))]

namespace JEFF.Portal
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
