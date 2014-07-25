using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterConsoleApp
{
    class ExcelFileForTwitter
    {
        #region Table Contents
        static Dictionary<String, int> tableTitles = new Dictionary<String, int>()
        { 
            {"Referans Etiketleme"                       ,1}
            , {"Konu ID"                                 ,2}
            , {"Konu"                                    ,3}
            , {"Tweet ID"                                ,4}
            , {"Tweet"                                   ,5}
            , {"User"                                    ,6}
            , {"Kaynak Uzman"                            ,7}
            , {"Tarih/Saat"                              ,8}
            , {"Sayısal Veri"                            ,9}
            , {"URL'ler"                                ,10}
            , {"Media"                                  ,11}
            , {"Mention"                                ,12}
            , {"RT Sayısı"                              ,13}
            , {"Dili"                                   ,14}
            , {"Hashtag'ler"                            ,15} 
            , {"Kimden Retweetlenmiş"                   ,16}
            , {"Smileys"                                ,17}
            , {"Ham Doğal Dil İşleme"                   ,18}
            //, {"Öğelere ayırma"                         17}
            , {"Olumsuz fiil"                           ,19}
            , {"Ayrıştırılmış Doğal Dil İşleme"         ,20}
            , {"Örtüşüm Oranı"                          ,21}
            , {"Ekonomi"                                ,22}
            , {"Borsa"                                  ,23}
            , {"Döviz"                                  ,24}
            , {"Altın"                                  ,25}
            , {"Petrol"                                 ,26}
            
        };
        #endregion
        #region Regexes
        static Regex nonAlphaCharacters = new Regex(@"[^\w\s]*");
        static Regex smileyRegex = new Regex(@"(?::|;|=)(?:-)?(?:\)|\(|D|P)");
        #endregion

        public static void PrepareExcelFile(Dictionary<String, List<TwitterStatus>> UsersTweets)
        {
            #region Zemberek initialization
            ZemberekAnaliz analyzer = new ZemberekAnaliz();
            #endregion
            #region Excel Workbook and Workshee initialization
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            #endregion
            #region Column Names Initialization
            foreach (var title in tableTitles)
            {
                worksheet.Cell(1, title.Value).Value = title.Key;
            }
            #endregion
            #region Excel dosyası için toplam konu sayıları
            Dictionary<String, int> toplamkonuSayisi = new Dictionary<string, int>()
            {
                {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            #endregion
            #region Iterasyondan(i'den) ayrı bir değişken tutulması için
            int satirSayisi = 0;
            #endregion
            #region Duplicate Tweetleri ayırmak için öncekileri de tutuyorum
            List<List<String>> oncekiTweetKelimeleri = new List<List<string>>();
            #endregion
            
            foreach (var userTweets in UsersTweets)
            {
                List<TwitterStatus> tweetList = userTweets.Value.ToList();
                List<String> tweetKelimeleri;
                for (int i = 0; i < tweetList.Count; i++, satirSayisi++)
                {

                    #region \n gibi bir karakter varsa split etmek için bunu kaldır
                    tweetList[i].Text = tweetList[i].Text.Replace('\n', ' ');
                    #endregion                 
                    #region Tweet'i kelimelere ayırmak için
                    tweetKelimeleri = tweetList[i].Text.Split(' ').ToList();
                    #endregion
                    #region Kelimeler arasındaki -\+ gibi diğer karakterleri temizlemek için
                    for (int j = 0; j < tweetKelimeleri.Count; j++)
                    {
                        tweetKelimeleri[j] = nonAlphaCharacters.Replace(tweetKelimeleri[j], "");
                    }
                    #endregion        
                    #region Eğer Önceki Tweet ile bir duplicate varsa atla
                    if (oncekiTweetKelimeleri.Count > 0)
                    {
                        bool has = HasDuplicateRecords(oncekiTweetKelimeleri, tweetKelimeleri);
                        if (has)
                        {
                            satirSayisi--;
                            continue;
                        }
                    }
                    oncekiTweetKelimeleri.Add(tweetKelimeleri.ToList());
                    #endregion
                    
                    #region Konu miktarları
                    int kelimeSayisi = 0;
                    int ortusenKelimeSayisi = 0;
                    int ortusenKonuSayisi = 0;
                    #region Borsa, Döviz, Altın gibi kelimelerin ne kadar geçtiğini burada tutacağım
                    Dictionary<String, int> TweetBasinaKonuSayisi = new Dictionary<string, int>()
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
                    worksheet.Cell(satirSayisi + 2, tableTitles["Tweet ID"]).Value = tweetList[i].Id.ToString();
                    worksheet.Cell(satirSayisi + 2, tableTitles["Tweet"]).Value = tweetList[i].Text;
                    worksheet.Cell(satirSayisi + 2, tableTitles["User"]).Value = tweetList[i].Author.ScreenName;
                    worksheet.Cell(satirSayisi + 2, tableTitles["Kaynak Uzman"]).Value = userTweets.Key;
                    worksheet.Cell(satirSayisi + 2, tableTitles["Tarih/Saat"]).Value = tweetList[i].CreatedDate;
                    foreach (var url in tweetList[i].Entities.Urls)
                        worksheet.Cell(satirSayisi + 2, tableTitles["URL'ler"]).Value = url.Value + ",";
                    foreach (var media in tweetList[i].Entities.Media)
                        worksheet.Cell(satirSayisi + 2, tableTitles["Media"]).Value = media.Url + ",";
                    foreach (var mention in tweetList[i].Entities.Mentions)
                        worksheet.Cell(satirSayisi + 2, tableTitles["Mention"]).Value = mention.ScreenName + ",";
                    worksheet.Cell(satirSayisi + 2, tableTitles["RT Sayısı"]).Value = tweetList[i].RetweetCount;
                    worksheet.Cell(satirSayisi + 2, tableTitles["Dili"]).Value = tweetList[i].Language;
                    foreach (var hashTag in tweetList[i].Entities.HashTags)
                        worksheet.Cell(satirSayisi + 2, tableTitles["Hashtag'ler"]).Value = hashTag.Text + ",";
                    if (tweetList[i].RetweetedStatus != null)
                        worksheet.Cell(satirSayisi + 2, tableTitles["Kimden Retweetlenmiş"]).Value = tweetList[i].RetweetedStatus.Author.ScreenName;
                    
                    foreach (var tweetKelimesi in tweetKelimeleri)
                    {
                        String word = tweetKelimesi;
                        if (Regex.IsMatch(word, @"(?::|;|=)(?:-)?(?:\)|\(|D|P)") && word.First() != '@')
                            worksheet.Cell(satirSayisi + 2, tableTitles["Smileys"]).Value = word;
                        word = nonAlphaCharacters.Replace(tweetKelimesi, "");
                        if (word == "") 
                            continue;
                        worksheet.Cell(satirSayisi + 2, tableTitles["Ham Doğal Dil İşleme"]).Value += word + ",";
                        #region Özne-Tümleç-Yüklem gibi öğelere ayırma  // Uzun sürüyor
                        Dictionary<String, List<String>> kelimeler = analyzer.AnalizYap(word, false);
                        foreach (var olumsuzKelime in kelimeler["Olumsuz fiil"])
                        {
                            worksheet.Cell(satirSayisi + 2, tableTitles["Olumsuz fiil"]).Value += olumsuzKelime + ",";
                        }
                        
                        #endregion
                        if (kelimeler["Konu ID"].Count != 0 )
                        {

                            worksheet.Cell(satirSayisi + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value = 
                                worksheet.Cell(satirSayisi + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value + 
                                "," +  tweetKelimesi ;
                            foreach (var konu in kelimeler["Konu ID"])
                            {
                                TweetBasinaKonuSayisi[konu]++;
                                ortusenKonuSayisi++;
                            }
                            ortusenKelimeSayisi++;
                        }
                        kelimeSayisi++;
                    }
                    double ortusumOrani = (Convert.ToDouble(ortusenKelimeSayisi) / (kelimeSayisi));
                    foreach (var konu in TweetBasinaKonuSayisi)
                    {
                        konuOranlari[konu.Key] = Convert.ToDouble(TweetBasinaKonuSayisi[konu.Key]) / ortusenKonuSayisi;
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
                            worksheet.Cell(satirSayisi + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
                            worksheet.Cell(satirSayisi + 2, tableTitles["Konu ID"]).Value = maxKey;
                            toplamkonuSayisi[maxKey]++;
                        }
                    }
                    #endregion

                    #region Ekonomi konusu değilse İlk Kolonları 0 olarak ata
                    if (double.IsNaN(konuOranlari["1.0"]))
                    {
                        worksheet.Cell(satirSayisi + 2, tableTitles["Konu"]).Value = 0;
                        worksheet.Cell(satirSayisi + 2, tableTitles["Konu ID"]).Value = 0;
                    }
                    #endregion



                    worksheet.Cell(satirSayisi + 2, tableTitles["Örtüşüm Oranı"]).Value = string.Format("{0:0.00}", ortusumOrani);
                    worksheet.Cell(satirSayisi + 2, tableTitles["Ekonomi"]).Value = string.Format("{0:0.00}", konuOranlari["1.0"]);
                    worksheet.Cell(satirSayisi + 2, tableTitles["Borsa"]).Value = string.Format("{0:0.00}", konuOranlari["1.1"]);
                    worksheet.Cell(satirSayisi + 2, tableTitles["Döviz"]).Value = string.Format("{0:0.00}", konuOranlari["1.2"]);
                    worksheet.Cell(satirSayisi + 2, tableTitles["Altın"]).Value = string.Format("{0:0.00}", konuOranlari["1.3"]);
                    worksheet.Cell(satirSayisi + 2, tableTitles["Petrol"]).Value = string.Format("{0:0.00}", konuOranlari["1.4"]);

                }
            }
            workbook.SaveAs("TweetsFromUsers.xlsx");
        }

        public static bool HasDuplicateRecords(List<List<String>> oncekiTweetKelimeleri, List<String> simdikiTweetKelimeleri)
        {
            // Eğer önceki tweet ile şimdiki tweet arka arkaya yazılmışlarsa işimiz kolay,
            // sadece ilk kelimelerine bakarak analiz yap
            if (oncekiTweetKelimeleri.Last().First() == simdikiTweetKelimeleri.First())
            {
                return true;
            }
            // Değilse diğer tüm tweet'ler arasından analiz yap
            foreach (var oncekiTweet in oncekiTweetKelimeleri)
            {
                if (oncekiTweet[0].Equals(simdikiTweetKelimeleri[0]) && oncekiTweet[1].Equals(simdikiTweetKelimeleri[1]))
                {
                    return true;
                }
            }
            return false;
            
        }






























        //public static void PrepareExcelFile(IEnumerable<TwitterStatus> tweets)
        //{
        //    ZemberekAnaliz analyzer = new ZemberekAnaliz();
        //    var workbook = new XLWorkbook();
        //    var worksheet = workbook.Worksheets.Add("Sample Sheet");
        //    #region Sütun isimleri yazdırılıyor
        //    foreach (var title in tableTitles)
        //    {
        //        worksheet.Cell(1, title.Value).Value = title.Key;
        //        if (title.Key == "Tweet ID") 
        //            worksheet.Cell(1, title.Value).SetDataType(XLCellValues.Text);
        //    }
        //    #endregion
        //    #region Excel dosyası için toplam değerler
        //    Dictionary<String, int> toplamkonuSayisi = new Dictionary<string, int>()
        //    {
        //        {"1.0", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
        //    };
        //    #endregion

        //    String[] tweetKelimeleri;
        //    List<TwitterStatus> tweetList = tweets.ToList();
        //    for (int i = 0; i < tweetList.Count; i++)
        //    {
        //        #region Konu miktarları
        //        int kelimeSayisi = 0;
        //        int ortusenKelimeSayisi = 0;
        //        #region Borsa, Döviz, Altın gibi kelimelerin ne kadar geçtiğini burada tutacağım
        //        Dictionary<String, int> TweetBasinaKonuSayisi = new Dictionary<string, int>()
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
        //        worksheet.Cell(i + 2, tableTitles["Tweet ID"]).Value = tweetList[i].Id.ToString();
        //        worksheet.Cell(i + 2, tableTitles["Tweet"]).Value = tweetList[i].Text;
        //        worksheet.Cell(i + 2, tableTitles["User"]).Value = tweetList[i].Author.ScreenName;
        //        worksheet.Cell(i + 2, tableTitles["Tarih/Saat"]).Value = tweetList[i].CreatedDate;
        //        foreach (var url in tweetList[i].Entities.Urls)
        //            worksheet.Cell(i + 2, tableTitles["URL'ler"]).Value = url.Value + ",";
        //        foreach (var media in tweetList[i].Entities.Media)
        //            worksheet.Cell(i + 2, tableTitles["Media"]).Value = media.Url + ",";
        //        foreach (var mention in tweetList[i].Entities.Mentions)
        //            worksheet.Cell(i + 2, tableTitles["Mention"]).Value = mention.ScreenName + ",";
        //        worksheet.Cell(i + 2, tableTitles["RT Sayısı"]).Value = tweetList[i].RetweetCount;
        //        worksheet.Cell(i + 2, tableTitles["Dili"]).Value = tweetList[i].Language;
        //        foreach (var hashTag in tweetList[i].Entities.HashTags)
        //            worksheet.Cell(i + 2, tableTitles["Hashtag'ler"]).Value = hashTag.Text + ",";
        //        if(tweetList[i].RetweetedStatus != null)
        //            worksheet.Cell(i + 2, tableTitles["Kimden Retweetlenmiş"]).Value = tweetList[i].RetweetedStatus.Author.ScreenName;
        //        tweetKelimeleri = tweetList[i].Text.Split(' ');
        //        foreach (var tweetKelimesi in tweetKelimeleri)
        //        {
        //            String word = tweetKelimesi;
        //            if (Regex.IsMatch(word, @"(?::|;|=)(?:-)?(?:\)|\(|D|P)") && word.First() != '@')
        //                worksheet.Cell(i + 2, tableTitles["Smileys"]).Value = word;
        //            if (word == "") continue;
        //            word = nonAlphaCharacters.Replace(tweetKelimesi, "");
        //            worksheet.Cell(i + 2, tableTitles["Ham Doğal Dil İşleme"]).Value += word + ",";
        //            #region Özne-Tümleç-Yüklem gibi öğelere ayırma  // Uzun sürüyor
        //            Dictionary<String, List<String>> kelimeler = analyzer.OgelereAyir(word);
        //            foreach (var olumsuzKelime in kelimeler["Olumsuz fiil"])
        //            {
        //                worksheet.Cell(satirSayisi + 2, tableTitles["Olumsuz fiil"]).Value += olumsuzKelime + ",";
        //            }

        //            #endregion
        //            if (kelimeler["Konu ID"].Count != 0)
        //            {
        //                worksheet.Cell(satirSayisi + 2, tableTitles["Ayrıştırılmış Doğal Dil İşleme"]).Value += tweetKelimesi + ",";
        //                //TweetBasinaKonuSayisi[kelimeler["Konu ID"]]++;
        //                ortusenKelimeSayisi++;
        //            }
        //            kelimeSayisi++;
        //        }
        //        double ortusumOrani = (Convert.ToDouble(ortusenKelimeSayisi) / (kelimeSayisi));
        //        foreach (var konu in TweetBasinaKonuSayisi)
        //        {
        //            konuOranlari[konu.Key] = Convert.ToDouble(TweetBasinaKonuSayisi[konu.Key]) / ortusenKelimeSayisi;
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
        //                toplamkonuSayisi[maxKey]++;
        //            }
        //        }
        //        #endregion
        //        #region Eğer Ekonomi kategorisindeki hiç konu yoksa
        //        if ((maxKey == "" || maxOran < 0.10) && !double.IsNaN(konuOranlari["1.0"]))
        //        {
        //            maxKey = "1.0";
        //            worksheet.Cell(i + 2, tableTitles["Konu"]).Value = idKonu[maxKey];
        //            worksheet.Cell(i + 2, tableTitles["Konu ID"]).Value = maxKey;
        //            toplamkonuSayisi[maxKey]++;
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

        //        #region TP TN FP FN yazdırma

        //        if (i == (tweetList.Count - 1))
        //        {
        //            #region Borsa için
        //            //worksheet.Cell(i + 3, tableTitles["Konu ID"]).Value = "Borsa için:";
        //            //worksheet.Cell(i + 3, tableTitles["Konu"]).Value = toplamkonuSayisi["1.1"];   //TP
        //            //worksheet.Cell(i + 3, tableTitles["Tweet ID"]).Value = tweetList.Count - toplamkonuSayisi["1.1"]; //TN
        //            //worksheet.Cell(i + 4, tableTitles["Konu ID"]).Value = "Döviz için:";
        //            //worksheet.Cell(i + 4, tableTitles["Konu"]).Value = toplamkonuSayisi["1.2"];   //TP
        //            //worksheet.Cell(i + 4, tableTitles["Tweet ID"]).Value = tweetList.Count - toplamkonuSayisi["1.2"]; //TN
        //            //worksheet.Cell(i + 5, tableTitles["Konu ID"]).Value = "Altın için:";
        //            //worksheet.Cell(i + 5, tableTitles["Konu"]).Value = toplamkonuSayisi["1.3"];   //TP
        //            //worksheet.Cell(i + 5, tableTitles["Tweet ID"]).Value = tweetList.Count - toplamkonuSayisi["1.3"]; //TN
        //            //worksheet.Cell(i + 6, tableTitles["Konu ID"]).Value = "Petrol için:";
        //            //worksheet.Cell(i + 6, tableTitles["Konu"]).Value = toplamkonuSayisi["1.4"];   //TP
        //            //worksheet.Cell(i + 6, tableTitles["Tweet ID"]).Value = tweetList.Count - toplamkonuSayisi["1.4"]; //TN
        //            #endregion

        //        }
        //        #endregion
        //    }
        //    workbook.SaveAs("Tweet.xlsx");
        //}
    }
}
