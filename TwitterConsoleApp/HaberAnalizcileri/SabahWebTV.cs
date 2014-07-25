using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    class SabahWebTV
    {
        static UnicodeEncoding uniEncoding = new UnicodeEncoding();    // MemoryStream'e Unicode karakterler yazmak için
        public static MemoryStream AnalyzeHTML(HtmlDocument RSSPage, String url)
        {
            var publishDate = RSSPage.DocumentNode.SelectNodes("//div[@class='bRight']//em")[1].InnerText;
            var nodes = RSSPage.DocumentNode.SelectNodes("//span[@class='txt']");
            String header = RSSPage.DocumentNode.SelectNodes("//strong[@class='txt']").First().InnerText;
            MemoryStream memStream = new MemoryStream();
            byte[] urlArray = uniEncoding.GetBytes(url + '\n');
            memStream.Write(urlArray, 0, urlArray.Length);

            byte[] publishDateArray = uniEncoding.GetBytes(publishDate + "\n");
            memStream.Write(publishDateArray, 0, publishDateArray.Length);

            header = HTMLOkuyucu.ReplaceCodeCharacters(header);
            byte[] headerArray = uniEncoding.GetBytes(header + "\n");
            memStream.Write(headerArray, 0, headerArray.Length);

            foreach (HtmlNode link in nodes)
            {
                //if (link.ChildNodes.Count > 1) continue;      // p içerisinde child node'lar olsun istemiyorum çünkü karışıyor
                //if (link.FirstChild.Name == "a") continue;    // p içerisinde link varsa bu aradığım text değil
                String nodeText = link.InnerText;
                nodeText = HTMLOkuyucu.RemoveSpecialCharacters(nodeText);   // &nbsp gibi karakterlerden temizliyorum 

                byte[] nodeTextArray = uniEncoding.GetBytes(nodeText + "\n");
                memStream.Write(nodeTextArray, 0, nodeTextArray.Length);
                Console.WriteLine(nodeText);
            }

            return memStream;
        }
    }
}
