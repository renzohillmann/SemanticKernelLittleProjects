using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using SimpleFeedReader;

namespace SK3_planning
{

    public class NewsPlugin
    {
        [KernelFunction("get_news")]
        [Description("Get the latest news from the New York Times.")]
        [return: Description("A list of current news stories.")]

        public async Task<List<FeedItem>> GetNews(Kernel kernel, string category) //kernel is the plugin object
        {
            var reader = new FeedReader(); // create a new feed reader object using the nugget package functionality
            var feedItems = await reader.RetrieveFeedAsync($"https://rss.nytimes.com/services/xml/rss/nyt/{category}.xml"); // get the feed from the NYT news rss feed
            return feedItems.Take(5).ToList(); // take the first 5 items and convert to list
        }



    }
}

