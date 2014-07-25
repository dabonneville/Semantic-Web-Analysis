using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    public class NtvmsnbcId
    {
        static UnicodeEncoding uniEncoding = new UnicodeEncoding();    // MemoryStream'e Unicode karakterler yazmak için
        public static MemoryStream AnalyzeHTML(HtmlDocument RSSPage, String url)
        {
            String header = RSSPage.DocumentNode.SelectNodes("//h1").First().InnerText.ToString();
            String publishDate = RSSPage.DocumentNode.SelectNodes("//html//head//meta")[8].Attributes[1].Value;
            String[] publishDateContents = publishDate.Split(' ');
            publishDate = publishDateContents[1] + "-" + publishDateContents[2] + "-" + publishDateContents[3];
            var nodes = RSSPage.DocumentNode.SelectNodes("//p[@class='textBodyBlack']");
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
                
                if (link.FirstChild.Name == "a") continue;    // p içerisinde link varsa bu aradığım text değil
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
