using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    public class ExcelFileCreator
    {
        
        #region Table Contents
        static Dictionary<String, int> tableTitles = new Dictionary<String, int>()
        {
            {"Referans Etiketleme"               , 1}
            , {"Konu ID"                         , 2}
            , {"Konu"                            , 3}
            , {"Haber ID"                        , 4}
            , {"Kaynak Uzman"                    , 5}
            , {"Tarih/Saat"                      , 6}
            , {"Haber Başlığı"                   , 7}
            , {"Haber Metni"                     , 8}
            , {"Haberin Kaynağı/Websitesi"       , 9}
            , {"Sayısal Veri"                   , 10}
            , {"Ham Doğal Dil İşleme"           , 11}
            , {"Olumsuz Fiil"                   , 12}
            , {"Ayrıştırılmış Doğal Dil İşleme" , 13}
            , {"Örtüşüm Oranı"                  , 14}
            , {"Etki Oranı"                     , 15}
            , {"Benzerlik Oranı"                , 16}
            , {"True/False"                     , 17}
            , {"Ekonomi"                        , 18}
            , {"Borsa"                          , 19}
            , {"Döviz"                          , 20}
            , {"Altın"                          , 21}
            , {"Petrol"                         , 22}
            , {"Precision"                      , 23}
            , {"Recall"                         , 24}
        };
        #endregion
        #region Regexes
        static Regex nonAlphaCharacters = new Regex(@"[^\w\s]*");
        #endregion
        public static void PrepareExcelFile(List<MemoryStream> streams)
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
            #region Excel dosyası için toplam değerler
            Dictionary<String, int> toplamkonuSayisi = new Dictionary<string, int>()
            {
                {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            #endregion
            String haberMetni;
            String[] haberKelimeleri;
            String[] haberParagraflari;
            String kelime;
            for (int i = 0; i < streams.Count; i++)
            {
                #region Konu miktarları
                int kelimeSayisi = 0;
                int ortusenKelimeSayisi = 0;
                int ortusenKonuSayisi = 0;
                #region Borsa, Döviz, Altın gibi kelimelerin ne kadar geçtiğini burada tutacağım
                Dictionary<String, int> konuSayisi = new Dictionary<string, int>()
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
                haberMetni = ReadStream(streams[i]);
                haberParagraflari = haberMetni.Split('\n');
                #region Tarih/Saat, Haber Başlığı, Haber Kaynağı yazılıyor
                
                worksheet.Cell(i + 2, tableTitles["Tarih/Saat"]).Value = haberParagraflari[1];
                worksheet.Cell(i + 2, tableTitles["Haber Başlığı"]).Value = haberParagraflari[2];
                worksheet.Cell(i + 2, tableTitles["Haberin Kaynağı/Websitesi"]).Value = haberParagraflari[0];
                worksheet.Cell(i + 2, tableTitles["Haber ID"]).Value = i;
                
                #endregion             
                #region Haber metni yazılıyor
                
                for (int j = 3; j < haberParagraflari.Length; j++)
                {
                    worksheet.Cell(i+2, tableTitles["Haber Metni"]).Value += haberParagraflari[j];
                }

                #endregion

                for (int j = 2; j < haberParagraflari.Length; j++)
                {
                    haberKelimeleri = haberParagraflari[j].Split(' ');
                    kelimeSayisi += haberKelimeleri.Length;
                    foreach (var hk in haberKelimeleri)
                    {
                        kelime = nonAlphaCharacters.Replace(hk, "");
                        
                        if (kelime == "") continue;
                        worksheet.Cell(i + 2, tableTitles["Ham Doğal Dil İşleme"]).Value += kelime + ",";
                        Dictionary<String, List<String>> kelimeler = analyzer.AnalizYap(kelime, false);

                        foreach (var olumsuzKelime in kelimeler["Olumsuz fiil"])
                        {
                            worksheet.Cell(i + 2, tableTitles["Olumsuz fiil"]).Value += olumsuzKelime + ",";
                        }

                        if (kelimeler["Konu ID"].Count != 0)
                        {

                            worksheet.Cell(i + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value =
                                worksheet.Cell(i + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value +
                                "," + kelime;
                            foreach (var konu in kelimeler["Konu ID"])
                            {
                                konuSayisi[konu]++;
                                ortusenKonuSayisi++;
                            }
                            ortusenKelimeSayisi++;
                        }
                        kelimeSayisi++;
                    }
                }
                double ortusumOrani = (Convert.ToDouble(ortusenKelimeSayisi) / (kelimeSayisi));
                foreach (var konu in konuSayisi)
                {
                    konuOranlari[konu.Key] = Convert.ToDouble(konuSayisi[konu.Key]) / ortusenKonuSayisi;
                }
                #region En büyük olan grubun oranı 
                double maxOran = 0;
                String maxKey = "";
                for (int j = 0; j < konuOranlari.Count; j++)
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
                //#region Eğer Ekonomi kategorisindeki hiç konu yoksa
                //if ((maxKey == "" || maxOran< 0.10) && !double.IsNaN(konuOranlari["1.0"]))
                //{
                //    maxKey = "1.0";
                //    worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
                //    worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
                //}
                //#endregion
                #region Ekonomi konusu değilse
                if (ortusumOrani<0.01)
                {
                    worksheet.Cell(i + 2, tableTitles["Konu"]).Value = 0;
                    worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = 0;
                }
                #endregion

                worksheet.Cell(i+2, tableTitles["Örtüşüm Oranı"]).Value = string.Format("{0:0.00}", ortusumOrani);
                worksheet.Cell(i + 2, tableTitles["Ekonomi"]).Value = string.Format("{0:0.00}", konuOranlari["1.0"]);
                worksheet.Cell(i + 2, tableTitles["Borsa"]).Value = string.Format("{0:0.00}", konuOranlari["1.1"]);
                worksheet.Cell(i + 2, tableTitles["Döviz"]).Value = string.Format("{0:0.00}", konuOranlari["1.2"]);
                worksheet.Cell(i + 2, tableTitles["Altın"]).Value = string.Format("{0:0.00}", konuOranlari["1.3"]);
                worksheet.Cell(i + 2, tableTitles["Petrol"]).Value = string.Format("{0:0.00}", konuOranlari["1.4"]);

            }
            workbook.SaveAs("RSSSHincal.xlsx");
        }

        public static String ReadStream(MemoryStream stream)
        {
            stream.Position = 0;
            var strReader = new StreamReader(stream, Encoding.Unicode);
            var haberMetni = strReader.ReadToEnd();
            stream.Close();
            strReader.Close();
            return haberMetni;

        }

        //public static void PrepareExcelFile(List<MemoryStream> streams)
        //{
        //    ZemberekAnaliz analyzer = new ZemberekAnaliz();
        //    var workbook = new XLWorkbook();
        //    var worksheet = workbook.Worksheets.Add("Sample Sheet");
        //    #region Sütun isimleri yazdırılıyor
        //    foreach (var title in tableTitles)
        //    {
        //        worksheet.Cell(1, title.Value).Value = title.Key;
        //    }
        //    #endregion
        //    String haberMetni;
        //    String[] haberKelimeleri;
        //    String[] haberParagraflari;
        //    String kelime;
        //    for (int i = 0; i < streams.Count; i++)
        //    {
        //        #region Konu miktarları
        //        int kelimeSayisi = 0;
        //        int ortusenKelimeSayisi = 0;
        //        #region Borsa, Döviz, Altın gibi kelimelerin ne kadar geçtiğini burada tutacağım
        //        Dictionary<String, int> konuSayisi = new Dictionary<string, int>()
        //        {
        //            {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
        //        };
        //        Dictionary<String, double> konuOranlari = new Dictionary<string, double>()
        //        {
        //            {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
        //        };
        //        Dictionary<String, String> idKonu = new Dictionary<string, String>()
        //        {
        //            {"1.0", "Ekonomi"}, {"1.1", "Borsa"}, {"1.2", "Döviz"}, {"1.3", "Altın"}, {"1.4", "Petrol"}
        //        };
        //        #endregion
        //        #endregion
        //        haberMetni = ReadStream(streams[i]);
        //        haberParagraflari = haberMetni.Split('\n');
        //        #region Tarih/Saat, Haber Başlığı, Haber Kaynağı yazılıyor

        //        worksheet.Cell(i + 2, tableTitles["Tarih/Saat"]).Value = haberParagraflari[1];
        //        worksheet.Cell(i + 2, tableTitles["Haber Başlığı"]).Value = haberParagraflari[2];
        //        worksheet.Cell(i + 2, tableTitles["Haberin Kaynağı/Websitesi"]).Value = haberParagraflari[0];
        //        worksheet.Cell(i + 2, tableTitles["Haber ID"]).Value = i;

        //        #endregion
        //        #region Haber metni yazılıyor

        //        for (int j = 3; j < haberParagraflari.Length; j++)
        //        {
        //            worksheet.Cell(i + 2, tableTitles["Haber Metni"]).Value += haberParagraflari[j];
        //        }

        //        #endregion

        //        for (int j = 2; j < haberParagraflari.Length; j++)
        //        {
        //            haberKelimeleri = haberParagraflari[j].Split(' ');
        //            kelimeSayisi += haberKelimeleri.Length;
        //            foreach (var hk in haberKelimeleri)
        //            {
        //                kelime = nonAlphaCharacters.Replace(hk, "");

        //                if (kelime == "") continue;
        //                worksheet.Cell(i + 2, tableTitles["Ham Doğal Dil İşleme"]).Value += kelime + ",";
        //                String kelimeninGrubu = analyzer.AnalizYap(kelime);
        //                if (kelimeninGrubu != "")
        //                {
        //                    worksheet.Cell(i + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value += kelime + ",";
        //                    konuSayisi[kelimeninGrubu]++;
        //                    ortusenKelimeSayisi++;
        //                }
        //            }
        //        }
        //        double ortusumOrani = (Convert.ToDouble(ortusenKelimeSayisi) / (kelimeSayisi));
        //        foreach (var konu in konuSayisi)
        //        {
        //            konuOranlari[konu.Key] = Convert.ToDouble(konuSayisi[konu.Key]) / ortusenKelimeSayisi;
        //        }
        //        #region En büyük olan grubun oranı
        //        double maxOran = 0;
        //        String maxKey = "";
        //        for (int j = 1; j < konuOranlari.Count; j++)
        //        {
        //            if (konuOranlari.ElementAt(j).Value > maxOran)
        //            {
        //                maxOran = konuOranlari.ElementAt(j).Value;
        //                maxKey = konuOranlari.ElementAt(j).Key;
        //                worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
        //                worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
        //            }
        //        }
        //        #endregion
        //        #region Eğer Ekonomi kategorisindeki hiç konu yoksa
        //        if ((maxKey == "" || maxOran < 0.10) && !double.IsNaN(konuOranlari["1.0"]))
        //        {
        //            maxKey = "1.0";
        //            worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
        //            worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
        //        }
        //        #endregion
        //        #region Ekonomi konusu değilse
        //        if (double.IsNaN(konuOranlari["1.0"]))
        //        {
        //            worksheet.Cell(i + 2, tableTitles["Konu"]).Value = double.NaN;
        //            worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = double.NaN;
        //        }
        //        #endregion

        //        worksheet.Cell(i + 2, tableTitles["Örtüşüm Oranı"]).Value = string.Format("{0:0.00}", ortusumOrani);
        //        worksheet.Cell(i + 2, tableTitles["Ekonomi"]).Value = string.Format("{0:0.00}", konuOranlari["1.0"]);
        //        worksheet.Cell(i + 2, tableTitles["Borsa"]).Value = string.Format("{0:0.00}", konuOranlari["1.1"]);
        //        worksheet.Cell(i + 2, tableTitles["Döviz"]).Value = string.Format("{0:0.00}", konuOranlari["1.2"]);
        //        worksheet.Cell(i + 2, tableTitles["Altın"]).Value = string.Format("{0:0.00}", konuOranlari["1.3"]);
        //        worksheet.Cell(i + 2, tableTitles["Petrol"]).Value = string.Format("{0:0.00}", konuOranlari["1.4"]);

        //    }
        //    workbook.SaveAs("RSS5.xlsx");
        //}

    }
}
