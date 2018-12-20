using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Wox.Plugin;
using System.Windows.Controls;

namespace YouTubeQuery
{
    public class Main : IPlugin, ISettingProvider
    {
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

            using (var webClient = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 }) {
               var response = JsonConvert.DeserializeObject<ApiResponse>(
                   webClient.DownloadString(urlBuilder.ApiUrl)
               );

               foreach (var item in response.items)
               {
                   string videoId;
                   item.id.TryGetValue("videoId", out videoId);
                   var publishedAt = DateTime.Parse(item.snippet.publishedAt).ToString("yyyy-MM-dd");

                   results.Add(new Result() {
                       Title = item.snippet.title,
                       SubTitle = item.snippet.channelTitle + " (" + publishedAt + ")",
                       IcoPath = "icon.png",
                       Action = e => {
                           System.Diagnostics.Process.Start(UrlBuilder.Url + videoId);

                           return true;
                       }
                   });
               }
            }

            return results;
        }
    }
}
