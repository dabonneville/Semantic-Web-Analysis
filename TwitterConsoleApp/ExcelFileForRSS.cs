using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    public class ExcelFileForRSS
    {
        #region Table Contents
        static Dictionary<String, int> tableTitles = new Dictionary<String, int>()
        {
            {"Konu ID", 1}, {"Konu", 2}, {"Kategori", 3}, {"Tarih/Saat", 4}, {"Haber Başlığı", 5}, {"Haber Metni", 6}, 
            {"Haberin Kaynağı/Websitesi", 7},  {"Sayısal Veri", 8}, {"Ham Doğal Dil İşleme", 9}, 
            {"Ayrıştırılmış Doğal Dil İşleme", 10}, {"Örtüşüm Oranı(uygun kelimeler/tüm kelimeler)", 11}, {"Etki Oranı", 12}, {"Benzerlik Oranı", 13}, 
            {"True/False", 14}, {"Ekonomi", 15}, {"Borsa", 16}, {"Döviz", 17}, {"Altın", 18}, {"Petrol", 19}, {"Precision", 20},
            {"Recall", 21}
        };
        #endregion
        #region Regexes
        static Regex nonAlphaCharacters = new Regex(@"[^\w\s\-]*");
        #endregion
        public static void PrepareExcelFile(List<RSS> rssList)
        {
            ZemberekAnaliz analyzer = new ZemberekAnaliz();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            #region Sütun isimleri yazdırılıyor
            foreach (var title in tableTitles)
            {
                worksheet.Cell(1, title.Value).Value = title.Key;
            }
            #endregion
            String[] haberKelimeleri;
            #region Excel dosyası için toplam değerler
            Dictionary<String, int> toplamkonuSayisi = new Dictionary<string, int>()
            {
                {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            #endregion
            
            for (int i = 0; i < rssList.Count; i++)
            {
                #region Konu miktarları
                int kelimeSayisi = 0;
                int ortusenKelimeSayisi = 0;
                #region Borsa, Döviz, Altın gibi kelimelerin ne kadar geçtiğini burada tutacağım
                Dictionary<String, int> RSSBasinaKonuSayisi = new Dictionary<string, int>()
                {
                    {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
                };
                Dictionary<String, double> konuOranlari = new Dictionary<string, double>()
                {
                    {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
                };
                Dictionary<String, String> idKonu = new Dictionary<string, String>()
                {
                    {"1.0", "Ekonomi"}, {"1.1", "Borsa"}, {"1.2", "Döviz"}, {"1.3", "Altın"}, {"1.4", "Petrol"}
                };
                #endregion
                #endregion
                worksheet.Cell(i + 2, tableTitles["Tarih/Saat"]).Value = rssList[i].PubDate;
                worksheet.Cell(i + 2, tableTitles["Haber Başlığı"]).Value = rssList[i].Title;
                worksheet.Cell(i + 2, tableTitles["Haberin Kaynağı/Websitesi"]).Value = rssList[i].Guid;
                worksheet.Cell(i + 2, tableTitles["Kategori"]).Value = rssList[i].Category;
                worksheet.Cell(i + 2, tableTitles["Haber Metni"]).Value += rssList[i].Description;


                haberKelimeleri = rssList[i].Description.Split(' ');
                foreach (var kelime in haberKelimeleri)
                {
                    if (kelime == "") continue;
                    kelimeSayisi++;
                    String tempKelime = HTMLOkuyucu.RemoveOtherCharacters(kelime);
                    worksheet.Cell(i + 2, tableTitles["Ham Doğal Dil İşleme"]).Value += tempKelime + ","; // Nokta ve virgüllerden ayrıştırıyorum
                    String kelimeninGrubu = analyzer.AnalizYap(kelime);
                    if (kelimeninGrubu != "")
                    {
                        worksheet.Cell(i + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value += tempKelime + ",";
                        RSSBasinaKonuSayisi[kelimeninGrubu]++;
                        ortusenKelimeSayisi++;
                    }
                }
                double ortusumOrani = (Convert.ToDouble(ortusenKelimeSayisi) / (kelimeSayisi));
                foreach (var konu in RSSBasinaKonuSayisi)
                {
                    konuOranlari[konu.Key] = Convert.ToDouble(RSSBasinaKonuSayisi[konu.Key]) / ortusenKelimeSayisi;
                }

                #region En büyük olan grubun oranı
                double maxOran = 0;
                String maxKey = "";
                for (int j = 1; j < konuOranlari.Count; j++)
                {
                    if (konuOranlari.ElementAt(j).Value > maxOran)
                    {
                        maxOran = konuOranlari.ElementAt(j).Value;
                        maxKey = konuOranlari.ElementAt(j).Key;
                        worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
                        worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
                        toplamkonuSayisi[maxKey]++;
                    }
                }
                #endregion
                #region Eğer Ekonomi kategorisindeki hiç konu yoksa
                if ((maxKey == "" || maxOran < 0.10) && !double.IsNaN(konuOranlari["1.0"]))
                {
                    maxKey = "1.0";
                    worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
                    worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
                    toplamkonuSayisi[maxKey]++;
                }
                #endregion
                #region Ekonomi konusu değilse
                if (double.IsNaN(konuOranlari["1.0"]))
                {
                    worksheet.Cell(i + 2, tableTitles["Konu"]).Value = double.NaN;
                    worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = double.NaN;
                }
                #endregion



                worksheet.Cell(i + 2, tableTitles["Örtüşüm Oranı(uygun kelimeler/tüm kelimeler)"]).Value = string.Format("{0:0.00}", ortusumOrani);
                worksheet.Cell(i + 2, tableTitles["Ekonomi"]).Value = string.Format("{0:0.00}", konuOranlari["1.0"]);
                worksheet.Cell(i + 2, tableTitles["Borsa"]).Value = string.Format("{0:0.00}", konuOranlari["1.1"]);
                worksheet.Cell(i + 2, tableTitles["Döviz"]).Value = string.Format("{0:0.00}", konuOranlari["1.2"]);
                worksheet.Cell(i + 2, tableTitles["Altın"]).Value = string.Format("{0:0.00}", konuOranlari["1.3"]);
                worksheet.Cell(i + 2, tableTitles["Petrol"]).Value = string.Format("{0:0.00}", konuOranlari["1.4"]);

                #region TP TN FP FN yazdırma

                if (i == (rssList.Count - 1))
                {
                    #region Borsa için
                    //worksheet.Cell(i + 3, tableTitles["Konu ID"]).Value = "Borsa için:";
                    //worksheet.Cell(i + 3, tableTitles["Konu"]).Value = toplamkonuSayisi["1.1"];   //TP
                    //worksheet.Cell(i + 3, tableTitles["Kategori"]).Value = rssList.Count - toplamkonuSayisi["1.1"]; //TN
                    //worksheet.Cell(i + 4, tableTitles["Konu ID"]).Value = "Döviz için:";
                    //worksheet.Cell(i + 4, tableTitles["Konu"]).Value = toplamkonuSayisi["1.2"];   //TP
                    //worksheet.Cell(i + 4, tableTitles["Kategori"]).Value = rssList.Count - toplamkonuSayisi["1.2"]; //TN
                    //worksheet.Cell(i + 5, tableTitles["Konu ID"]).Value = "Altın için:";
                    //worksheet.Cell(i + 5, tableTitles["Konu"]).Value = toplamkonuSayisi["1.3"];   //TP
                    //worksheet.Cell(i + 5, tableTitles["Kategori"]).Value = rssList.Count - toplamkonuSayisi["1.3"]; //TN
                    //worksheet.Cell(i + 6, tableTitles["Konu ID"]).Value = "Petrol için:";
                    //worksheet.Cell(i + 6, tableTitles["Konu"]).Value = toplamkonuSayisi["1.4"];   //TP
                    //worksheet.Cell(i + 6, tableTitles["Kategori"]).Value = rssList.Count - toplamkonuSayisi["1.4"]; //TN
                    #endregion
                    
                }
            //        {"Konu ID", 1}, {"Konu", 2}, {"Kategori", 3}, {"Tarih/Saat", 4}, {"Haber Başlığı", 5}, {"Haber Metni", 6}, 
            //{"Haberin Kaynağı/Websitesi", 7},  {"Sayısal Veri", 8}, {"Ham Doğal Dil İşleme", 9}, 
                #endregion
            }
            workbook.SaveAs("RSS.xlsx");
        }
    }
}
