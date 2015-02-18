using JEFF.Dto.Smartables.Request;
using JEFF.Dto.Smartables.Response;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController
{
    /// <summary>
    /// SmartablesBoard
    /// </summary>
    public class SmartablesBoard
    {
        string _rootUrl;
        string _apiKey;
        string _apiSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartablesBoard" /> class.
        /// </summary>
        /// <param name="rootUrl">The root URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        /// <param name="boardId">The board identifier.</param>
        public SmartablesBoard(string rootUrl, string apiKey, string apiSecret, string boardId)
        {
            _rootUrl = rootUrl;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            BoardId = boardId;
        }

        /// <summary>
        /// Gets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public string BoardId { get; private set; }

        /// <summary>
        /// Reads the specified channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public async Task<T> Read<T>(string channel) where T : class, IPortResponseDto
        {
            string combinedUrl = string.Format("{0}/read/{1}/{2}/{3}", _rootUrl, _apiKey, BoardId, channel);
            HttpWebRequest request = WebRequest.Create(new Uri(combinedUrl)) as HttpWebRequest;

            return await MakeRequest<T>(request);
        }

        /// <summary>
        /// Writes the specified channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="value">The value.</param>
        public async Task<R> Write<T, R>(string channel, T body)
            where R : class, IPortResponseDto
            where T : class, IPortUpdateDto
        {
            string combinedUrl = string.Format("{0}/write/{1}/{2}/{3}", _rootUrl, _apiKey, BoardId, channel);
            HttpWebRequest request = WebRequest.Create(new Uri(combinedUrl)) as HttpWebRequest;
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Headers["X-APISecret"] = _apiSecret;

            if (body != null)
            {
                using (Stream dataStream = await request.GetRequestStreamAsync())
                using (JsonTextWriter w = new JsonTextWriter(new StreamWriter(dataStream)))
                {
                    JsonSerializer s = new JsonSerializer();
                    s.Serialize(w, body);
                }
            }

            return await MakeRequest<R>(request);
        }

        /// <summary>
        /// Makes the request.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<R> MakeRequest<R>(HttpWebRequest request) where R : class, IPortResponseDto
        {
            string received;
            using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(responseStream))
                    {
                        JsonTextReader r = new JsonTextReader(sr);
                        JsonSerializer s = new JsonSerializer();
                        received = (s.Deserialize(r) ?? string.Empty).ToString();

                        Trace.TraceInformation(request.RequestUri + " returned: '" + received + "'");
                    }
                }
            }

            return (typeof(R) == typeof(String)) ? received as R : JsonConvert.DeserializeObject(received, typeof(R)) as R;
        }
    }
}
