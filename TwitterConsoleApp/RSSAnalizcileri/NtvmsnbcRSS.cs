using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TwitterConsoleApp.RSSAnalizcileri
{
    public class NtvmsnbcRSS
    {
        public static void AnalyzeHTML(HtmlDocument RSSPage, String URL)
        {
            HtmlNode.ElementsFlags.Remove(HtmlElementFlag.CData.ToString());
            var items = RSSPage.DocumentNode.SelectNodes("//rss//channel//item");
            List<RSS> rssList = new List<RSS>();

            foreach (var item in items)
            {

                rssList.Add(new RSS()
                {
                    Title = item.ChildNodes[0].InnerText,
                    Description = item.ChildNodes[1].InnerText.Replace("<![CDATA[<p>", "").Replace("]]>", ""),
                    Link = item.ChildNodes[2].InnerText,
                    Text = item.ChildNodes[3].InnerText,
                    Category = item.ChildNodes[6].InnerText,
                    PubDate = item.ChildNodes[5].InnerText,
                    Guid = item.ChildNodes[7].InnerText,
                });
            }
            ExcelFileForRSS.PrepareExcelFile(rssList);
        }
    }
}
