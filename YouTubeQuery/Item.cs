using System.Collections.Generic;

namespace YouTubeQuery 
{
    class Item 
    {
        public string kind 
        {
            get; set;
        }

        public string etag
        {
            get; set;
        }

        public Dictionary<string, string> id 
        {
            get; set;
        }

        public Snippet snippet 
        {
            get; set;
        }
    }
}