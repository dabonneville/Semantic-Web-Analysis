using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace TwitterConsoleApp
{
    class HTMLOkuyucu
    {
        public static Dictionary<String, String> charsets = new Dictionary<string, string>()
        {

        };
        public static MemoryStream ReadHTML(String url)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;  // Console'da Unicode karakterler yazmak için
            HttpDownloader downloader = new HttpDownloader(url, null, null);
            HtmlDocument myHtml = new HtmlDocument();
            #region Sayfa eğer gelmezse tekrar getir
            do
            {
                myHtml.LoadHtml(GetURLData(url));
                Console.WriteLine(url + " ulaşılamıyor");
            } while (myHtml == null);
            #endregion     
            #region Diskten HTML sayfasını okuyacaksak
            // myHtml.Load("Merkez Bankası faizi artırdı - Dr.Mahfi Eğilmez - Yorum ve Analiz - cnbce.com.htm", Encoding.UTF8);
            #endregion
            
            #region Aşağıdaki iki kod satırında gelen url'e göre bir class'ı alarak onun AnalyzeRSS fonksiyonunu çalıştırıyorum

            String analyzerClass = HTMLAnalyzerSelector.GetAnalyzerClass(url);
            MethodInfo analysisMethod = Type.GetType(analyzerClass).GetMethod("AnalyzeHTML");
            MemoryStream memStream = (MemoryStream)analysisMethod.Invoke(null, new object[] { myHtml, url });
            
            #endregion
            return memStream;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            str = ReplaceCodeCharacters(str);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '\r' && str[i + 1] == '\r')
                {
                    continue;
                }
                else if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') ||
                    c == '.' || c == '_' || c == ' ' || c == 'ş' || c == 'ç' || c == 'ö' || c == 'ü'
                    || c == 'ı' || c == 'İ' || c == 'ğ' || c == 'Ğ' || c == 'Ş' || c == 'Ç' || c == 'Ö'
                    || c == 'Ü' || c == '(' || c == ')' || c == '\\' || c == '/' || c == '\'' || c == ',' || c == '%'
                    || c == '-' || c == '"' || c == '\n' || c=='\'')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string ReplaceCodeCharacters(String str)
        {
            str = str.Replace("nbsp", "");
            str = str.Replace("Uuml", "Ü");
            str = str.Replace("uuml", "ü");
            str = str.Replace("Ouml", "Ö");
            str = str.Replace("ouml", "ö");
            str = str.Replace("ccedil", "ç");
            str = str.Replace("&#231;", "ç");
            str = str.Replace("&#199;", "Ç");
            str = str.Replace("&#252;", "ü");
            str = str.Replace("&#220;", "Ü");
            str = str.Replace("&#214;", "Ö");
            str = str.Replace("&#246;", "ö");
            str = str.Replace("&#39;", "'");
            str = str.Replace("Ccedil", "Ç");
            str = str.Replace("ndash", "–");
            str = str.Replace("ldquo", "\"");
            str = str.Replace("rdquo", "\"");
            str = str.Replace("rsquo", "'");
            str = str.Replace("acirc", "â");
            str = str.Replace("   ", "");
            return str;
        }

        public static string RemoveOtherCharacters(String str)
        {
            str = str.Replace(".", "").Replace(",", "");
            return str;
        }

        public static string GetURLData(string URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                request.UserAgent = "Omurcek";
                request.Timeout = 4000;
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding siteEncoding = Encoding.UTF8;
                String[] slashes = URL.Split('/');
                #region Encoding kısmı aslında html sayfasındaki charset kısmı okunarak bulunabilir

                if ((slashes[2] == "www.sabah.com.tr" && slashes[3] != "Spor") || slashes[2] == "haber.gazetevatan.com")
                    siteEncoding = Encoding.GetEncoding("windows-1254");
                
                #endregion
                
                StreamReader reader = new StreamReader(stream, siteEncoding);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
                return null;
            }

        }
        
    }
}
