using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    public class HTMLAnalyzerSelector
    {
        public static String GetAnalyzerClass(String url)
        {
            String[] slashes = url.Split('/');
            if (slashes[2] == "www.cnbce.com")
            {
                if (slashes[3] == "haberler") return "TwitterConsoleApp.CnbceHaberler";
                else if (slashes[3] == "yorum-ve-analiz") return "TwitterConsoleApp.CnbceYorumveAnaliz";
            }
            else if (slashes[2] == "www.ntvmsnbc.com")
            {
                //if (slashes[7] == "rss.xml") return "TwitterConsoleApp.RSSAnalizcileri.NtvmsnbcRSS";
                if (slashes[3] == "id") return "TwitterConsoleApp.NtvmsnbcId";
            }
            else if (slashes[2] == "haber.gazetevatan.com")
            {
                return "TwitterConsoleApp.VatanYazarlar";
            }
            else if (slashes[2] == "www.sabah.com.tr")
            {
                //if (slashes[7] == "rss.xml") return "TwitterConsoleApp.RSSAnalizcileri.SabahRSS";
                if (slashes[3] == "sinema")
                    return "TwitterConsoleApp.SabahSinema";
                else if (slashes[3] == "webtv")
                    return "TwitterConsoleApp.SabahWebTV";
                else if (slashes[3] == "Yazarlar")
                    return "TwitterConsoleApp.SabahYazarlar";
                else if (slashes[3] == "Spor")
                    return "TwitterConsoleApp.SabahYazarlar";
                else
                    return "TwitterConsoleApp.Sabah";
            }
            return null;
        }
    }
}
