using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Wox.Plugin;
using System.Windows.Controls;

namespace YouTubeQuery
{
    public class Main : IPlugin, ISettingProvider
    {
        private string _videoId;
        private Options options { get; set; }

        public void Init(PluginInitContext context)
        {
            options = new Options(context != null 
                ? Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "options.json") 
                : "options.json"
            );
            
            options.Load();
        }

        public Control CreateSettingPanel()
        {
            return new SettingsControl(options);
        }

        public List<Result> Query (Query query)
        {
            var results = new List<Result>();
            var urlBuilder = new UrlBuilder(query.Search, options.ApiKey);

            using (var webClient = new System.Net.WebClient()) {
               var response = JsonConvert.DeserializeObject<ApiResponse>(
                   webClient.DownloadString(urlBuilder.ApiUrl)
               );

               foreach (var item in response.items) {
                   item.id.TryGetValue("videoId", out _videoId);

                   results.Add(new Result() {
                       Title = item.snippet.title,
                       SubTitle = item.snippet.channelTitle,
                       IcoPath = "icon.png",
                       Action = e => {
                           System.Diagnostics.Process.Start(UrlBuilder.Url + _videoId);

                           return true;
                       }
                   });
               }
            }

            return results;
        }
    }
}
