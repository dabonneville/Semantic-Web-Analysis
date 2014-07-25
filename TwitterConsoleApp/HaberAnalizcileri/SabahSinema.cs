using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    class SabahSinema
    {
        static UnicodeEncoding uniEncoding = new UnicodeEncoding();    // MemoryStream'e Unicode karakterler yazmak için
        public static MemoryStream AnalyzeHTML(HtmlDocument RSSPage, String url)
        {
            String header = RSSPage.DocumentNode.SelectNodes("//h1").First().InnerText.ToString();
            String[] urlSlashes = url.Split('/');
            String publishDate = urlSlashes[6] + "-" + urlSlashes[5] + "-" + urlSlashes[4];
            var nodes = RSSPage.DocumentNode.SelectNodes("//div[@class='txt']");
            MemoryStream memStream = new MemoryStream();
            byte[] urlArray = uniEncoding.GetBytes(url + '\n');
            memStream.Write(urlArray, 0, urlArray.Length);

            byte[] publishDateArray = uniEncoding.GetBytes(publishDate + "\n");
            memStream.Write(publishDateArray, 0, publishDateArray.Length);

            header = HTMLOkuyucu.RemoveSpecialCharacters(header);
            byte[] headerArray = uniEncoding.GetBytes(header + "\n");
            memStream.Write(headerArray, 0, headerArray.Length);

            foreach (HtmlNode link in nodes)
            {
                String nodeText = link.InnerText;
                nodeText = HTMLOkuyucu.RemoveSpecialCharacters(nodeText);   // &nbsp gibi karakterlerden temizliyorum 

                byte[] nodeTextArray = uniEncoding.GetBytes(nodeText + "\n");
                memStream.Write(nodeTextArray, 0, nodeTextArray.Length);
                Console.WriteLine(nodeText);
            }

            return memStream;
        }

        public static string CleanTime(String time)
        {
            time = time.Replace("\n", "");
            time = time.Replace(" ", "");
            time = time.Replace("&nbsp;", " ");
            return time;
        }
    }
}
