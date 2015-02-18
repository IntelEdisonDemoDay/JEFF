using System;

namespace JEFF.Portal
{
    /// <summary>
    /// HtmlExtensions
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Resolves the server URL.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="forceHttps">if set to <c>true</c> [force HTTPS].</param>
        /// <returns></returns>
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
    }
}
