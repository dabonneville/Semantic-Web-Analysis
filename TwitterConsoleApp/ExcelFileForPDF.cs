using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    class ExcelFileForPDF
    {
        #region Table Contents
        static Dictionary<String, int> tableTitles = new Dictionary<String, int>()
        {
            {"Referans Etiketleme"               , 1}
            , {"Konu ID"                         , 2}
            , {"Konu"                            , 3}
            , {"Sayfa No"                        , 4}
            , {"Kaynak Yazar"                    , 5}
            , {"Tarih/Saat"                      , 6}
            , {"Dosya Adı"                       , 7}
            , {"Başlıklar"                       , 8}
            , {"Sayfa Metni"                     , 9}
            , {"Haberin Kaynağı/Websitesi"      , 10}
            , {"Sayısal Veri"                   , 11}
            , {"Ham Doğal Dil İşleme"           , 12}
            , {"Olumsuz Fiil"                   , 13}
            , {"Ayrıştırılmış Doğal Dil İşleme" , 14}
            , {"Örtüşüm Oranı"                  , 15}
            , {"Etki Oranı"                     , 16}
            , {"Benzerlik Oranı"                , 17}
            , {"True/False"                     , 18}
            , {"Ekonomi"                        , 19}
            , {"Borsa"                          , 20}
            , {"Döviz"                          , 21}
            , {"Altın"                          , 22}
            , {"Petrol"                         , 23}
        };
        #endregion
        #region Regexes
        static Regex nonAlphaCharacters = new Regex(@"[^\w\s]*");
        static Regex urlRegex = new Regex(@"^(http|https)://");
        #endregion
        public static void PrepareExcelFile(Dictionary<int,String> PageContents, String FileName, String AuthorName, String Date)
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
            int rowNumber;
            String[] pageWords;
            String[] pageParagraphs;
            String kelime;
            foreach (var pageContent in PageContents)
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
                rowNumber = pageContent.Key + 1;
                worksheet.Cell(rowNumber, tableTitles["Sayfa No"]).Value = pageContent.Key;
                //worksheet.Cell(rowNumber, tableTitles["Kaynak Yazar"]).Value = AuthorName;
                //worksheet.Cell(rowNumber, tableTitles["Tarih/Saat"]).Value = Date;
                //worksheet.Cell(rowNumber, tableTitles["Dosya Adı"]).Value = FileName;
                worksheet.Cell(rowNumber, tableTitles["Sayfa Metni"]).Value = pageContent.Value;
                pageParagraphs = pageContent.Value.Split('\n');
                foreach (var paragraph in pageParagraphs)
                {
                    #region Eğer Başlık ise büyük harflerden oluşmalı ve uzunluğu en az 5 karakter olmalı
                    if (paragraph == paragraph.ToUpper() && paragraph.Length >= 5)
                        worksheet.Cell(rowNumber, tableTitles["Başlıklar"]).Value += paragraph + ",";
                    #endregion
                    #region URL varsa
                    else if (urlRegex.IsMatch(paragraph))
                    {
                        worksheet.Cell(rowNumber, tableTitles["Haberin Kaynağı/Websitesi"]).Value += paragraph + ",";
                        continue;
                    }
                    #endregion

                    pageWords = paragraph.Split(' ');
                    foreach (var word in pageWords)
                    {
                        if (pageContent.Key == 2)
                        {
                            int a = 2;
                        }
                        kelime = word;
                        #region Kelimeyi Diğer Karakterlerden temizleme
                        kelime = nonAlphaCharacters.Replace(kelime, "");
                        #endregion
                        #region Eğer kelime temizlendikten sonra herhangi bir katakter içermiyorsa diğer kelimeye geç
                        if (kelime == "") continue;
                        #endregion
                        worksheet.Cell(rowNumber, tableTitles["Ham Doğal Dil İşleme"]).Value += kelime + ",";
                        Dictionary<String, List<String>> kelimeler = analyzer.AnalizYap(kelime, false);
                        foreach (var olumsuzKelime in kelimeler["Olumsuz fiil"])
                        {
                            worksheet.Cell(rowNumber, tableTitles["Olumsuz fiil"]).Value += olumsuzKelime + ",";
                        }
                        #region Kelime Kategorilendirme
                        if (kelimeler["Konu ID"].Count != 0)
                        {

                            worksheet.Cell(rowNumber, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value =
                                worksheet.Cell(rowNumber, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value +
                                "," + kelime;
                            foreach (var konu in kelimeler["Konu ID"])
                            {
                                konuSayisi[konu]++;
                                ortusenKonuSayisi++;
                            }
                            ortusenKelimeSayisi++;
                        }
                        kelimeSayisi++;
                        #endregion
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
                        worksheet.Cell(rowNumber, tableTitles["Konu"]).Value = idKonu[maxKey];
                        worksheet.Cell(rowNumber, tableTitles["Konu ID"]).Value = maxKey;
                        toplamkonuSayisi[maxKey]++;
                    }
                }
                #endregion

                #region Ekonomi konusu değilse
                if (ortusumOrani < 0.01)
                {
                    worksheet.Cell(rowNumber, tableTitles["Konu"]).Value = 0;
                    worksheet.Cell(rowNumber, tableTitles["Konu ID"]).Value = 0;
                }
                #endregion

                worksheet.Cell(rowNumber, tableTitles["Örtüşüm Oranı"]).Value = string.Format("{0:0.00}", ortusumOrani);
                worksheet.Cell(rowNumber, tableTitles["Ekonomi"]).Value = string.Format("{0:0.00}", konuOranlari["1.0"]);
                worksheet.Cell(rowNumber, tableTitles["Borsa"]).Value = string.Format("{0:0.00}", konuOranlari["1.1"]);
                worksheet.Cell(rowNumber, tableTitles["Döviz"]).Value = string.Format("{0:0.00}", konuOranlari["1.2"]);
                worksheet.Cell(rowNumber, tableTitles["Altın"]).Value = string.Format("{0:0.00}", konuOranlari["1.3"]);
                worksheet.Cell(rowNumber, tableTitles["Petrol"]).Value = string.Format("{0:0.00}", konuOranlari["1.4"]);

            }
            workbook.SaveAs("PDF.xlsx");
        }

    }
}
