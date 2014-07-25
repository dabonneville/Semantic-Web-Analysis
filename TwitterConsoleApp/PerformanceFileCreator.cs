using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    class PerformanceFileCreator
    {
        Dictionary<String, int> metrics;
        Dictionary<String, int> groupID;
        Dictionary<String, int> simpleMetrics;
        public PerformanceFileCreator()
        {
            #region Table Titles
            metrics = new Dictionary<String, int>();
            metrics.Add("Metrics/Group ID"                       , 1);
            metrics.Add("TPRSS(With Zero)"                       , 2);
            metrics.Add("TPRSS(Without Zero)"                    , 3);
            metrics.Add("TPTwitter(With Zero)"                   , 4);
            metrics.Add("TPTwitter(Without Zero)"                , 5);

            metrics.Add("TNRSS(With Zero)"                       , 6);
            metrics.Add("TNRSS(Without Zero)"                    , 7);
            metrics.Add("TNTwitter(With Zero)"                   , 8);
            metrics.Add("TNTwitter(Without Zero)"                , 9);

            metrics.Add("FPRSS(With Zero)"                      , 10);
            metrics.Add("FPRSS(Without Zero)"                   , 11);
            metrics.Add("FPTwitter(With Zero)"                  , 12);
            metrics.Add("FPTwitter(Without Zero)"               , 13);

            metrics.Add("FNRSS(With Zero)"                      , 14);
            metrics.Add("FNRSS(Without Zero)"                   , 15);
            metrics.Add("FNTwitter(With Zero)"                  , 16);
            metrics.Add("FNTwitter(Without Zero)"               , 17);

            metrics.Add("PrecisionRSS(With Zero)"               , 18);
            metrics.Add("PrecisionTweet(With Zero)"             , 19);
            metrics.Add("PrecisionRSS(Without Zero)"            , 20);
            metrics.Add("PrecisionTweet(Without Zero)"          , 21);

            metrics.Add("RecallRSS(With Zero)"                  , 22);
            metrics.Add("RecallTweet(With Zero)"                , 23);
            metrics.Add("RecallRSS(Without Zero)"               , 24);
            metrics.Add("RecallTweet(Without Zero)"             , 25);

            metrics.Add("F-MeasureRSS(With Zero)"               , 26);
            metrics.Add("F-MeasureTweet(With Zero)"             , 27);
            metrics.Add("F-MeasureRSS(Without Zero)"            , 28);
            metrics.Add("F-MeasureTweet(Without Zero)"          , 29);

            metrics.Add("Balanced AccuracyRSS(With Zero)"       , 30);
            metrics.Add("Balanced AccuracyTweet(With Zero)"     , 31);
            metrics.Add("Balanced AccuracyRSS(Without Zero)"    , 32);
            metrics.Add("Balanced AccuracyTweet(Without Zero)"  , 33);
            
            groupID = new Dictionary<String, int>();
            groupID.Add("0"  , 2);
            groupID.Add("1"  , 3);
            groupID.Add("1.1", 4);
            groupID.Add("1.2", 5);
            groupID.Add("1.3", 6);
            groupID.Add("1.4", 7);

            simpleMetrics = new Dictionary<string, int>();
            simpleMetrics.Add("TP"                  , 2);
            simpleMetrics.Add("TN"                  , 3);
            simpleMetrics.Add("FP"                  , 4);
            simpleMetrics.Add("FN"                  , 5);
            simpleMetrics.Add("Precisions"          , 6);
            simpleMetrics.Add("Recalls"             , 7);
            simpleMetrics.Add("Sensitivities"       , 8);
            simpleMetrics.Add("Specifities"         , 9);
            simpleMetrics.Add("Fscores"            , 10);
            simpleMetrics.Add("BalancedAccuracies" , 11);
            #endregion
        }
        public void PrepareExcelFile(List<String> files)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            #region Metrikler yazdırılıyor
            foreach (var title in simpleMetrics)
            {
                worksheet.Cell(1, title.Value).Value = title.Key;
            }
            #endregion
            #region Group ID'ler yazdırılıyor
            foreach (var ID in groupID)
            {
                worksheet.Cell(ID.Value, 1).Value = ID.Key;
            }
            #endregion

            var data = AnalyzeRSSFile(files.First());
            #region Geri dönen veriler excel tablosuna yazdırılıyor
            foreach (var item in data)
            {
                foreach (var i in item.Value)
                {
                    worksheet.Cell(groupID[i.Key], simpleMetrics[item.Key]).Value = i.Value;
                }
            }
            #endregion
            
            workbook.SaveAs("Performance.xlsx");
        }

        public Dictionary<String, Dictionary<String, double>> AnalyzeRSSFile(String fileName)
        {
            #region TP, TN, FP, FN toplam değerleri
            Dictionary<String, double> TP = new Dictionary<string, double>()
            {
                {"0", 0}, {"1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            Dictionary<String, double> TN = new Dictionary<string, double>()
            {
                {"0", 0}, {"1", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            Dictionary<String, double> FP = new Dictionary<string, double>()
            {
                {"0", 0}, {"1", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            Dictionary<String, double> FN = new Dictionary<string, double>()
            {
                {"0", 0}, {"1", 0}, {"1.1", 0}, {"1.2", 0}, {"1.3", 0}, {"1.4", 0}
            };
            #endregion    
            Dictionary<String, double> precisions = new Dictionary<String, double>();
            Dictionary<String, double> recalls = new Dictionary<String, double>();
            Dictionary<String, double> sensitivities = new Dictionary<String, double>();
            Dictionary<String, double> specifities = new Dictionary<String, double>();
            Dictionary<String, double> fscores = new Dictionary<String, double>();
            Dictionary<String, double> balancedAccuracies = new Dictionary<String, double>();

            #region Her konudan kaç tane entry var (hesaplanan/referans)
            Dictionary<String, int> computedKonuSayisi = new Dictionary<string, int>()
            {  
                {"0", 0}      // Diğerleri  Spor, Magazin, Müzik vs.
                , {"1", 0}    // Ekonomi   ToString()'ten dolayı 1.0 demedim
                , {"1.1", 0}  // Borsa
                , {"1.2", 0}  // Döviz
                , {"1.3", 0}  // Altın
                , {"1.4", 0}  // Petrol
            };
            Dictionary<String, int> etiketlenenKonuSayisi = new Dictionary<string, int>()
            {  
                {"0", 0}      // Diğerleri  Spor, Magazin, Müzik vs.
                , {"1", 0}    // Ekonomi   ToString()'ten dolayı 1.0 demedim
                , {"1.1", 0}  // Borsa
                , {"1.2", 0}  // Döviz
                , {"1.3", 0}  // Altın
                , {"1.4", 0}  // Petrol
            };
            #endregion
            #region Hesaplanan ve Referans verileri karşılaştırması için
            List<String> hesaplananVeri = new List<string>();
            List<String> referansVeri = new List<string>();
	        #endregion
            
            int toplamEntrySayisi = 0;    // Toplam Entry sayısı
            var workbook = new XLWorkbook(fileName);
            var worksheet = workbook.Worksheets.First();

            
            #region Entry Başına Hesaplanan Konu Sayıları Hesaplanıyor
            var rngHeaders = worksheet.Range("B2:B250");        // Tahmini olarak 2 - 200. satırlardaki değerleri al
            foreach (var cell in rngHeaders.Cells())
            {
                if (cell.Value.ToString() == "") break;             // Boş ise döngüden çık
                hesaplananVeri.Add(cell.Value.ToString());          // Hesaplananları ekle
                computedKonuSayisi[cell.Value.ToString()] += 1;     // İlgili konu sayısını bir arttır
            }
            #endregion
            #region Entry Başına Etiketlenen Konu Sayıları Hesaplanıyor
            rngHeaders = worksheet.Range("A2:A250");            // Tahmini olarak 2 - 200. satırlardaki değerleri al
            foreach (var cell in rngHeaders.Cells())
            {
                if (cell.Value.ToString() == "") break;                 // Boş ise döngüden çık
                referansVeri.Add(cell.Value.ToString());                // Hesaplananları ekle
                etiketlenenKonuSayisi[cell.Value.ToString()] += 1;      // İlgili konu sayısını bir arttır
            }
            #endregion
            toplamEntrySayisi = hesaplananVeri.Count;
            #region TP ve TN'ler hesaplanıyor
            foreach (var konu in computedKonuSayisi)
            {
                TP[konu.Key] = konu.Value;         // TP her konudan ne kadar geçtiği olduğu için direkt kopyalıyorum
                TN[konu.Key] = computedKonuSayisi["0"];    // TN ise Magazin, Spor ile alakalı olanlar
            }  
            #endregion
            #region FP ve FN'ler hesaplanıyor
            for (int i = 0; i < hesaplananVeri.Count; i++)
            {
                // 0 olan grup spor magazin gibi bir grup olduğu için es geçiyorum
                if ((hesaplananVeri[i] != referansVeri[i]) 
                    && (hesaplananVeri[i] != "0") && (referansVeri[i] != "0") 
                    && (hesaplananVeri[i] != "1") && (referansVeri[i] != "1")
                    )
                {
                    FP[hesaplananVeri[i]]++;
                    FN[referansVeri[i]]++;
                }
            }
            #endregion
            #region Precision hesaplanıyor
            foreach (var tp in TP)
            {
                precisions.Add(tp.Key, tp.Value / Convert.ToDouble(tp.Value + FP[tp.Key]));
            }
            #endregion
            #region Recall hesaplanıyor
            foreach (var tp in TP)
            {
                recalls.Add(tp.Key, tp.Value / Convert.ToDouble(tp.Value + FN[tp.Key]));
            }
            #endregion
            #region F-Score hesaplanıyor
            foreach (var precision in precisions)
            {
                fscores.Add(precision.Key, 
                    (2 * precision.Value * recalls[precision.Key]) / (precision.Value + recalls[precision.Key]));
            }
            #endregion
            #region Sensivisity Hesaplanıyor
            foreach (var tp in TP)
            {
                sensitivities.Add(tp.Key, tp.Value / Convert.ToDouble(tp.Value + FN[tp.Key]));
            }
            #endregion
            #region Specificity Hesaplanıyor
            foreach (var tn in TN)
            {
                specifities.Add(tn.Key, tn.Value / Convert.ToDouble(FP[tn.Key] + tn.Value));
            }
            #endregion
            #region Balanced Accuracy hesaplanıyor
            foreach (var sensitivity in sensitivities)
            {
                balancedAccuracies.Add(sensitivity.Key, (sensitivity.Value + specifities[sensitivity.Key]) / 2);
            }
            #endregion

            Dictionary<String, Dictionary<String, double>> returnedValues = new Dictionary<string, Dictionary<string, double>>()
            {
                {"TP", TP }, {"TN", TN }, {"FP", FP }, {"FN", FN }
                , {"Precisions", precisions }, {"Recalls", recalls }, {"Sensitivities", sensitivities }
                , {"Specifities", specifities }, {"Fscores", fscores }, {"BalancedAccuracies", balancedAccuracies },
            };
            return returnedValues;
        }
    }
}
