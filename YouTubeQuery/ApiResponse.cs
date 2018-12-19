using System.Collections.Generic;

namespace YouTubeQuery 
{
    class ApiResponse 
    {
        public string kind 
        {
            get; set;
        }

        public string etag 
        {
            get; set;
        }

        public string nextPageToken 
        {
            get; set;
        }

        public string regionCode 
        {
            get; set;
        }

        public Dictionary<string, decimal> pageInfo 
        {
            get; set;
        }

        public List<Item> items 
        {
            get; set;
        }
    }
}
