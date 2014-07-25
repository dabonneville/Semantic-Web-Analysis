using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterConsoleApp
{
    public class TwitterOkuyucu
    {
        public static void ReadTweets(Dictionary<String, String> users, int count)
        {
            TwitterService service = new TwitterService(
                "FnoRDMpWvcZtwdd0GSOXQ", "FklaCse7MKLuRadNf3850LYwPpY5PDwBH2s94jRxs", "94564297-TlexpM0soy5xUx2DeNx3GWmkVW6Bo82Ln8xbiixzD", "TVxvhZi1SjKWylumeNZC4nnouVbdr6UOwUjrHLLjdh6JJ");
            Dictionary<String, List<TwitterStatus>> tweets = new Dictionary<string, List<TwitterStatus>>();
            foreach (var user in users)
            {
                tweets.Add(user.Value,
                    service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
                    {
                        ScreenName = user.Key,
                        Count = count,
                        ExcludeReplies = true,
                        IncludeRts = false,
                    }).ToList());
            }
            ExcelFileForTwitter.PrepareExcelFile(tweets);
        }





        //public static void ReadTweetss(Dictionary<String, String> users)
        //{
        //    TwitterService service = new TwitterService(
        //        "FnoRDMpWvcZtwdd0GSOXQ", "FklaCse7MKLuRadNf3850LYwPpY5PDwBH2s94jRxs", "94564297-TlexpM0soy5xUx2DeNx3GWmkVW6Bo82Ln8xbiixzD", "TVxvhZi1SjKWylumeNZC4nnouVbdr6UOwUjrHLLjdh6JJ");
        //    Dictionary<String, List<TwitterStatus>> tweets = new Dictionary<string, List<TwitterStatus>>();
        //    //int iterationLimit = 0;
        //    //if (count > 200)
        //    //    iterationLimit = count / 200;  // Her defasında max 200 tweet çekebildiğim için döngü sayısını bu şekilde buluyorum

        //    foreach (var user in users)
        //    {
        //        tweets.Add(user.Value,
        //            service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
        //            {
        //                ScreenName = user.Key,
        //                Count = 200,
        //                ExcludeReplies = true,
        //                IncludeRts = false,
        //            }).ToList());
        //    }
        //    ExcelFileForTwitter.PrepareExcelFile(tweets);
        //}



        //public static void ReadTweet(String screenName)
        //{
        //    TwitterService service = new TwitterService("FnoRDMpWvcZtwdd0GSOXQ", "FklaCse7MKLuRadNf3850LYwPpY5PDwBH2s94jRxs", "94564297-TlexpM0soy5xUx2DeNx3GWmkVW6Bo82Ln8xbiixzD", "TVxvhZi1SjKWylumeNZC4nnouVbdr6UOwUjrHLLjdh6JJ");
        //    var homeTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions 
        //    {
        //        ScreenName = screenName,
        //        Count = 200,
        //        ExcludeReplies = true,
        //        IncludeRts = false,
        //        SinceId = 123953591978426369
                
        //    });
        //    ExcelFileForTwitter.PrepareExcelFile(homeTweets);
        //}
    }
}
