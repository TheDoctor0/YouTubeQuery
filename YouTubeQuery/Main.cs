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
        private string _thumbnailsPath;

        public void Init(PluginInitContext context)
        {
            if (context != null) {
                options = new Options(Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "options.json"));
                _thumbnailsPath = Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "thumbnails");

                if (!Directory.Exists(_thumbnailsPath)) {
                    Directory.CreateDirectory(_thumbnailsPath);
                }

                var files = Directory.GetFiles(_thumbnailsPath, "*.jpg");

                foreach (var file in files) {
                    File.Delete(file);
                }
            } else {
                options = new Options("options.json");
            }
            
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

               foreach (var item in response.items) {
                   string videoId;
                   item.id.TryGetValue("videoId", out videoId);
                   
                   var publishedAt = DateTime.Parse(item.snippet.publishedAt).ToString("yyyy-MM-dd");
                   var icoPath = "icon.png";

                   if (options.Thumbnails && !string.IsNullOrEmpty(_thumbnailsPath)) {
                       icoPath = Path.Combine(_thumbnailsPath, videoId + ".jpg");

                       if (!File.Exists(icoPath)) {
                           webClient.DownloadFile(new Uri("https://i.ytimg.com/vi/" + videoId + "/default.jpg"), icoPath);
                       }
                   }

                   results.Add(new Result() {
                       Title = item.snippet.title,
                       SubTitle = item.snippet.channelTitle + " (" + publishedAt + ")",
                       IcoPath = icoPath,
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
