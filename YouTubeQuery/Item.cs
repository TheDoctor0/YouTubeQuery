using System.Collections.Generic;

namespace YouTubeQuery 
{
    class Item 
    {
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