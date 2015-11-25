using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace HttpService
{
    public enum RequestType
    {
        GET,
        POST
    }

    public abstract class HttpService
    {
        protected async Task<string> ExecuteRequestAsync(string url, RequestType type, Dictionary<string, string> @params)
        {
            string result;

            using (var client = new HttpClient())
            {
                if (type == RequestType.POST)
                {
                    var content = new FormUrlEncodedContent(@params);
                    var response = await client.PostAsync(url, content);
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // append guid to prevent http requests caching 
                    StringBuilder args = new StringBuilder("?nocache=" + Guid.NewGuid() + "&");

                    // build params string
                    foreach (var pair in @params)
                    {
                        args.AppendFormat("{0}={1}&", pair.Key, pair.Value);
                    }

                    // append params to url
                    url = url + args;

                    // remove last '&' symbol and execute request
                    result = await client.GetStringAsync(url.Remove(url.Length - 1));
                }

            }

            return result;
        }
    }

    public class VideoService : HttpService
    {
        public string Host { get; set; }

        public VideoService(string url)
        {
            this.Host = url;
        }

        public async Task<string> PauseVideoAsync()
        {
            return await ExecuteRequestAsync(Host + "/pause", RequestType.GET, new Dictionary<string, string>()
            {
                {"action", "pause" }
            });
        }

        public async Task<string> PlayVideoAsync()
        {
            return await ExecuteRequestAsync(Host + "/play", RequestType.GET, new Dictionary<string, string>()
            {
                {"action", "play" }
            });
        }

        public async Task<string> ChangeVolumeAsync(double volume)
        {
            return await ExecuteRequestAsync(Host + "/set-volume", RequestType.GET, new Dictionary<string, string>()
            {
                {"action", "setVolume" },
                {"value", Convert.ToString(volume)}
            });
        }
    }
}

