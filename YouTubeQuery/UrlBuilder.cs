namespace YouTubeQuery 
{
    class UrlBuilder 
    {
        private const string BaseUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet&q=";
        public const string Url = "https://www.youtube.com/watch?v=";
        public readonly string ApiUrl;

        public UrlBuilder (string query, string apiKey)
        {
            ApiUrl = BaseUrl + query + "&type=video&maxResults=5&videoCaption=any&key=" + apiKey;
        }
    }
}
