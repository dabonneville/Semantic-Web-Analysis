﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    class CnbceYorumveAnaliz
    {
        static UnicodeEncoding uniEncoding = new UnicodeEncoding();    // MemoryStream'e Unicode karakterler yazmak için
        public static MemoryStream AnalyzeHTML(HtmlDocument RSSPage, String url)
        {
            String publishDate = RSSPage.DocumentNode.SelectNodes("//span[@class='publishDate']").First().InnerText.ToString();
            String header = RSSPage.DocumentNode.SelectNodes("//span[@class='bigBlue']").First().InnerText.ToString();
            var nodes = RSSPage.DocumentNode.SelectNodes("//div[@class='leftBox']//p");
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
                if (link.ChildNodes.Count > 1) continue; // p içerisinde child node'lar olsun istemiyorum çünkü karışıyor
                String nodeText = link.InnerText;
                nodeText = HTMLOkuyucu.RemoveSpecialCharacters(nodeText);   // &nbsp gibi karakterlerden temizliyorum 

                byte[] nodeTextArray = uniEncoding.GetBytes(nodeText + "\n");
                memStream.Write(nodeTextArray, 0, nodeTextArray.Length);
            }

            return memStream;
        }
    }
}
