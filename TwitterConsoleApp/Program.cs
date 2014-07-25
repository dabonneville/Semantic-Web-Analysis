using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Drawing;
using System.Media;
using System.Text.RegularExpressions;
using net.zemberek.erisim;
using net.zemberek.yapi;
using net.zemberek.tr.yapi;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using TwitterConsoleApp.PDFAnalizi;


namespace TwitterConsoleApp
{
    class Program
    {
        public static void Mainnnnnnnnnnn(string[] args)
        {
            String filename = "Genel Ekonomi Temel Kavramlar.pdf";
            String author = "EKONOMİ BAKANLIĞI";
            String date = "06-04-2012";
            PdfOkuyucu.ReadPDF(filename, author, date);
        }

        public static void Mainnn(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            PerformanceFileCreator pfc = new PerformanceFileCreator();
            String rssDosyasi = "deneme.xlsx";
            String rssDosyasiw0 = "RSSfromUser.xlsx";
            String tweetDosyasi = "NonZero_TweetsFromUsers.xlsx";
            String tweetDosyasiw0 = "TweetsFromUsers.xlsx";
            List<String> files = new List<string>()
            {
                rssDosyasi, rssDosyasiw0, tweetDosyasi, tweetDosyasiw0
            };

            pfc.PrepareExcelFile(files);
            s.Stop();
            Console.WriteLine(s.Elapsed.ToString());
            Console.WriteLine("Press Any Key");
            Console.Read();
        }

        public static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            Dictionary<String, String> users = new Dictionary<string, string>() 
            {
                {"mahfiegilmez",  "Mahfi Eğilmez" }
                //{"gungoruras",    "Güngör Uras" },
                //{"CemkilicDr",    "Cem Kılıç" },
                //{"SelimAtalayNY", "Selim Atalay" },
            };
            TwitterOkuyucu.ReadTweets(users, 1000000);
            s.Stop();
            Console.WriteLine(s.Elapsed.ToString());
            Console.WriteLine("Press Any Key");
            Console.Read();
        }

        //public static void Main(string[] args)
        //{
        //    Stopwatch s = new Stopwatch();
        //    s.Start();
        //    TwitterOkuyucu.ReadTweet("mahfiegilmez");
        //    s.Stop();
        //    Console.WriteLine(s.Elapsed.ToString());
        //    Console.WriteLine("Press Any Key");
        //    Console.Read();
        //}


        //static void Main(string[] args)
        //{
        //    Stopwatch s = new Stopwatch();
        //    s.Start();
        //    List<String> UsernameList = new List<string>()
        //    {
        //        "mahfiegilmez",
        //        "gungoruras",
        //        "CemkilicDr",
        //        "SelimAtalayNY",
        //    };
        //    List<MemoryStream> streamList = new List<MemoryStream>();
        //    foreach (var URL in UsernameList)
        //    {
        //        streamList.Add(HTMLOkuyucu.ReadHTML(URL));
        //    }
        //    //ExcelFileCreator.PrepareExcelFile(streamList);
        //    s.Stop();
        //    Console.WriteLine(s.Elapsed.ToString());
        //    Console.WriteLine("Press Any Key");
        //    Console.Read();
        //}

        public static void Mainnnnnnnnn(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            List<String> URLList = new List<string>()
            {
                #region Asaf Savaş AKAT
                "http://haber.gazetevatan.com/yolsuzlugun-bedeli/613941/4/Yazarlar/8",
                #endregion
                #region Mahfi Eğilmez
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/cari-acik-dususte", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dalgalanmaya-devam",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/piyasalar-normale-donuyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/kapidaki-sikinti",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/faizi-artirdik-simdi-ne-olacak",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/merkez-bankasi-faizi-artirdi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/zor-bir-hafta-bizi-bekliyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/ekonomide-durum-saptamasi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/gecen-haftanin-surprizi", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/2014-tl-nin-ic-ve-dis-deger-kaybiyla-basladi", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/2014-e-ne-devrediyoruz",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/pozitif-ayrismadan-negatif-ayrismaya",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/bir-iyi-bir-kotu",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/stanley-fischer-fed-baskan-yardimicligina-aday", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/buyumenin-arka-plani", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/buyume-nasil-gelir", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/tasarruflar-duserken-harcamalar-artiyor", 
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/bitcoin",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/piyasalarda-durum",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/yellen-ve-gelecegin-fed-politikasi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/draghi-ve-adi-konmamis-kur-savaslari",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/manset-enflasyon",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dunyanin-ekonomik-gorunumu-ve-turkiye",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/imf-den-kurtulamadik",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/abd-zor-durumda",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/sikintilar-bitmiyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/fed-den-once-fed-den-sonra",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dunyadaki-yerimiz",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/kusursuz-firtinaya-dogru",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/decoupling-(ayrisma)-teorisinde-son-durum",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/merkez-bankasi-ni-bekleyen-buyuk-sinav",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dunya-bir-tuhaf-oldu",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/buyume-sorunu",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/enflasyon-yukseliyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/faizin-artirilmasi-gerekiyorsa-artirilmali",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/tcmb-ne-yapacak",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/haftaya-umutlu-baslangic",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/bedava-paranin-sonu",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/altinin-gelecegi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/yine-sikintili-bir-hafta",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/fed-aciklamasi-ve-gezi-parki-olaylarinin-piyasalara-etkisi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/faiz-yukselince-kazananlar-ve-kaybedenler",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/haftaya-baslarken-kisa-bir-ozet",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/parami-nereye-yatirayim-diye-soranlara",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/tl-deger-kaybedince-ne-oluyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/faiz-dustu-reyting-artti-sirada-ne-var",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/tcmb-faizi-1-puan-indirmeliydi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/odemeler-dengesi-verileri-buyumede-hizlanma-olmadigini-gosteriyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/merkez-bankasi-nin-sterilizasyon-uygulamasi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/piyasalar-inisli-cikisli",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/kuresel-krizde-bugun-neredeyiz",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/neo-liberalizm-in-sonu",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dunya-ve-turkiye",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/ekonomide-sert-inis",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/zor-bir-hafta-geride-kaldi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/insani-gelismislikte-ekvator-un-gerisindeyiz",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/cikis-basladi-mi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dunya-ekonomisi-kayniyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/kur-savaslari-nedir",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/faizler-dusecek-mi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/bosuna-mi-endise-ettik",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/bu-hafta-gundemde-neler-var",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/merkez-bankasi-ne-yapacak",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/ruzgar-turkiye-lehine-mi-esiyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/ne-kadar-cari-acik-o-kadar-buyume",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/dr-mahfi-egilmez-2013-tahminlerini-paylasti",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/yunanistan-reytingde-bize-yetismek-uzere",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/cari-acigin-finansman-kalitesi-bozuluyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/buyumede-hayal-kirikligi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/sapkadan-altin-cikarmanin-sonuna-mi-geldik",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/mahfi-egilmez-den-rok-un-turkcesi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/imf-moody-s-ve-turkiye",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/tcmb-faiz-oranlarini-dusurebilir-mi",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/sanayi-uretimi-buyumeye-donusu-isaret-ediyor",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/enflasyon-ve-reytingde-son-durum",
                //"http://www.cnbce.com/yorum-ve-analiz/dr-mahfi-egilmez/obama-nin-uygulamalari-ve-uygulayacaklari",
                #endregion
                #region Güngör Uras
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/dokunmayin-ciceklere-yazik-olur-emeklere" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/mayis-ta-doviz-girisi-durdu" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/ihracattaki-yavaslama-ekonomiyi-farkli-yonlerden-etkileyecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/olmaz-olamaz" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/ozel-sektorun-doviz-kredileri-ve-net-doviz-acigi-buyuyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/tasarruflar-nereye-gidecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/merkez-bankasi-nin-rezervindeki-altinlar-dovizler-nasil-artiyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/2013-un-kaderi-ic-talepdeki-canlanmaya-bagli" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/lider-almanya-liderligini-surdurebilir-mi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/bankalar-ekonominin-temel-diregi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/ekonomiyi-insaatla-canli-tutmaya-calisiyoruz" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/korfez-ulkelerinden-para-bekleyisi-suruyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/paramiz-olmasa-da-mutluyuz-umutluyuz" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/turklerin-altin-sevgisi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/daha-cok-oncu-gostergeye-ihtiyacimiz-var" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/sangay-beslisi-turkiye-icin-alternatif-mi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/ozel-sektorun-dis-kredi-stoku" ,
                //"http://www.cnbce.com/yorum-ve-analiz/gungor-uras/samsung-onde-kosuyor" ,
                #endregion
                #region Cem Kılıç
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/yurtdisinda-calisanlar-nasil-emekli-olacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/mesleksizlik-devam-ediyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/ne-zaman-emekli-olabilirim" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/isgucu-maliyetleri-artiyor-ancak-calisanlarin-kaci-bu-kosullara-sahip" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/isverenler-is-sagligi-ve-guvenligi-konusunda-elini-cabuk-tutmali" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/kobi-ler-istihdam-tesviklerinden-haberdar-mi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/ilerleme-raporunda-istihdam-ve-sosyal-politika-bolumunde-elestiri-de-var-ovgu-de" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sgk-ile-anlasmasi-olmayan-ozel-hastane-de-artik-hamile-kadina-rapor-yazabilecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/calisma-meclisi-surekli-toplanabilir-hale-gelmeli" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/okul-sutu-programi-ile-cocuklar-saglikli-buyuyecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/her-ile-sosyal-hizmet-merkezi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/kadin-istihdami-engellenmeden-koruma-saglanmali" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/gebe-calisanlar-ozel-olarak-korunacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/aliaga-daki-is-kazasi-ne-gosterdi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/yardimci-saglik-personeli-istihdam-yukumlulugu-basladi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/fazla-ilave-ucret-talep-eden-ozel-hastaneye-kapatma" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/is-guvenligi-uzmani-ve-is-yeri-hekimi-istihdami-ertelendi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/yeni-asgari-ucretle-isci-ne-kadar-alacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/isyerlerinde-calisan-temsilcisi-secme-sartlari-geliyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/ucretler-yonunden-oecd-nin-en-kotusuyuz" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/buyuk-olcekli-yatirim-tesviki-hakkinda-bilinmesi-gerekenler" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/genc-issizligi-boyle-artarsa-sosyal-guvenligin-finansmani-zorlasir" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/istege-bagli-sigortali-olanlar" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/turkiye-ilo-ya-daha-fazla-finansal-destek-verecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sosyal-diyalogsuzluk" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sosyal-guvenlik-icin-yeni-bir-torba-yasa-daha" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/cocuk-bayrami-nda-cocuk-nufusu-ve-is-gucu-uzerine…" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/kidem-tazminatinizi-alirken-dikkat-edin" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sosyal-yardimlarda-artis-var" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/emeklilikte-yasa-takilanlara-erken-emeklilik-cok-zor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/hastanelerde-e-sevk-uygulamasi-basladi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/kayitdisilikla-mucadelede-yeni-donem" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/genc-issizlik-boyle-artarsa-sosyal-guvenlikte-isler-zor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/8-mart-dunya-kadinlar-gunu-yaklasti-calisan-kadinlarimizin-sorunlari-bitmek-bilmiyor…" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sosyal-guvenlikte-degisen-dengeler…" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/dogum-izni-artinca-istihdamdaki-kadin-orani-duser" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/isyerinizde-saglik-ve-guvenlik-kurulunuzu-olusturdunuz-mu" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/kamuya-esneklik-gelir-mi-is-guvencesi-kalkar-mi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/calisma-bakanligi-istatistikleri-sendikalari-bicecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/mini-reform-paketi-sosyal-guvenlik-te-neleri-degistirecek(1)" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/imkb-de-grev-yasagi-geliyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/sgk-spor-kuluplerinden-sonra-doktorlari-da-mercek-altina-aldi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/intibak-sonucunda-borclu-cikan-emekliler-de-var" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/meclis-teki-yasa-gencleri-ve-calisan-emeklileri-sevindirecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/ilo-2012-2013-ucret-raporu-aciklandi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/issizlik-kagit-ustunde-de-dusecek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/calisma-bakanligi-taseron-yasasini-tamamlamak-uzere" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/is-sagligi-ve-guvenligi-yasasi-can-yakabilir" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/yeni-sendikalar-yasasi-sendikalari-zor-durumda-birakir" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/calisma-yasaminda-2013-te-buyuk-degisim-olacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/yesil-karti-kalkti-ama-degisen-bir-sey-olmadi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/prof-dr-cem-kilic/gelir-testi-en-cok-dogu-ve-guneydogu-illerinde-yapildi" ,

                #endregion
                #region Selim Atalay
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/bernanke…adamcagizi-kimse-anlamiyor-anlatiyor-yine-anlamiyorlar" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/dunya-hala-yuvarlak-borc-hala-yuksek-kemer-sikilacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/biri-keynes-e-escinsel-dedi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/istihdam-duzelse-de-bir-kisi-issiz-kalacak" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/balonu-patlatmak-icin-levye-ile-girismek" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/ogreniyoruz-butun-merkez-bankalari-ogreniyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/gelen-agam-olur-giden-pasam…" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/fed-den-kesintisiz-destek-ve-sinirsiz-sefkat" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/euro-akdeniz-gunesinde-neden-ariza-cikariyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/hic-de-seviyeli-bir-iliski-degilmis" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/japonya-enflasyon-yaratacak-baskan-ariyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/piyasaya-taraftar-olunmaz-portfoyle-evlilik-olmaz" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/eski-bankacidan-yeni-dokunmatik--kariyer" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/2007-krizinde-fed-uyudu" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/1-trilyonluk-ativer-bakiiim-yazi-mi-tura-mi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/colde-kum-fed-de-para" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/bm-kredi-notunda--cevre--faktoru-istiyor" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/gokten-mali-ucurum-a-3-elma-dusecek" ,
                ////"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/nerede-bu-wall-street-pusulasi" ,
                //"http://www.cnbce.com/yorum-ve-analiz/selim-atalay/zaferden-sonra-gercekler" ,
                #endregion
                #region Hıncal Uluç Sabah
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/03/05/haremde-beethovenden-halka-beethovene" ,
                //"http://www.sabah.com.tr/Spor/Yazarlar/uluc/2014/03/04/allah-gostermesin-fenerbahce" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/03/04/kirimdan-dunya-savasi-cikar-mi" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/03/02/kentsel-donusum-mu-rantsal-donusum-mu" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/03/01/bir-ben-mi-evet-bir-sen" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/28/galatasarayin-adi-ve-futbolun-tadi" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/27/gun-adalet-bakaninin-gunu" ,
                //"http://www.sabah.com.tr/Spor/Yazarlar/uluc/2014/02/26/chelsea-affetmez" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/26/osmanli-cini-atolyesini-yok-ediyorlar" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/22/gazetecilik-evriliyor-mu-devriliyor-mu" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/21/yasasin-bogazici-universitesi" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/20/bedava-yasiyoruz-bedava" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/19/politika-yapmak-sanati" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/18/sevsinler-ana-muhalefeti" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/16/tecelliden-abuzittine-mektuplar" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/15/is-sanatta-bir-unutulmaz-gece" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/14/14-subat-sevenlerin-gunudur" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/13/yazdan-kalma-gecede-ruya" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/12/chpnin-bir-oyu-eksildi" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/11/gazeteciligimden-utaniyorum" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/05/kanuninin-yapamadigini-piyanist-yapiyor" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/04/sezarin-hakki-sezara" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/02/tecelliden-abuzittine-mektuplar" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/02/01/bir-tatsiz-hafta-sonu-daha" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/31/yeniden-yargilanma-olmazsa-olmaz" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/30/klasik-kedi-populer-kedilere-karsi" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/29/sorumsuzlar-ulkesinde-bedava-olmek" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/28/bu-ulkenin-sahibi-yok-mu" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/26/tecelliden-abuzittine-mektuplar" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/25/sonunda-bakirkoyluk-oldum2" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/24/sonunda-bakirkoyluk-oldum" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/23/siyaset-degil-vatanseverlik-gunu" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/22/safiye-ayla-ve-zeki-mureni-kacirdim" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/21/bu-gerginlik-bitmeli-bit-me-li" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/19/tecelliden-abuzittine-mektuplar" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/18/benim-anadolum-benim-ankaram" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/17/muzigin-mucizeleri-ve-frangulis" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/16/opera-binasi-olmayan-ulkede-opera-harikalari" ,
                //"http://www.sabah.com.tr/Yazarlar/uluc/2014/01/12/tecelliden-abuzittine-mektuplar" ,
                #endregion


                //"http://www.ntvmsnbc.com/id/24927361/device/rss/rss.xml",
                //"http://www.sabah.com.tr/rss/Ekonomi.xml"

            };
            List<MemoryStream> streamList = new List<MemoryStream>();
            foreach (var URL in URLList)
            {
                streamList.Add(HTMLOkuyucu.ReadHTML(URL));
            }
            //ExcelFileCreator.PrepareExcelFile(streamList);
            ExcelFileCreator.PrepareExcelFile(streamList);
            s.Stop();
            Console.WriteLine(s.Elapsed.ToString());
            Console.WriteLine("Press Any Key");
            Console.Read();
        }




        //static void Main(string[] args) // Twitter verileri için
        //{
        //    TwitterService service = new TwitterService("FnoRDMpWvcZtwdd0GSOXQ", "FklaCse7MKLuRadNf3850LYwPpY5PDwBH2s94jRxs", "94564297-TlexpM0soy5xUx2DeNx3GWmkVW6Bo82Ln8xbiixzD", "TVxvhZi1SjKWylumeNZC4nnouVbdr6UOwUjrHLLjdh6JJ");
        //    String screenName = "mahfiegilmez";
        //    Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        //    //var gelen = servis.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { ScreenName = "mahfiegilmez", Count = 2000 });
        //    //var gelen = servis.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 12 });
        //    var homeTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = screenName, Count = 200 });
        //    using (StreamWriter writer = new StreamWriter("tweets.txt"))
        //    {

        //        foreach (var item in homeTweets)
        //        {
        //            String tweetDetails = item.CreatedDate.Day + "/" + item.CreatedDate.Month + "/" + item.CreatedDate.Year + ":" + item.Text;
        //            Console.WriteLine(tweetDetails);
        //            writer.WriteLine(tweetDetails);
        //        }
        //        //var tweet2 = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = screenName, Count = 200, MaxId = homeTweets.Last().Id });
        //        //foreach (var item in tweet2)
        //        //{
        //        //    String tweetDetails = item.CreatedDate.Day + "/" + item.CreatedDate.Month + "/" + item.CreatedDate.Year + ":" + item.Text;
        //        //    Console.WriteLine(tweetDetails);
        //        //    writer.WriteLine(tweetDetails);
        //        //}
        //        Stopwatch stopwatch = new Stopwatch();
        //        stopwatch.Start();
        //        CreateExcelFile(homeTweets, zemberek);
        //        stopwatch.Stop();
        //        Console.WriteLine(stopwatch.Elapsed);
        //        Console.ReadKey();
        //    }
        //}

        public static void CreateExcelFile(IEnumerable<TwitterStatus> tweets, Zemberek zemberek)
        {
            Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
            { 
                {"ekonomi", "1.0"}, {"tüketici", "1.0"}, {"para", "1.0"}, {"faiz", "1.0"}, {"maliye", "1.0"}
                , {"sermaye", "1.0"}, {"borç", "1.0"}, {"ihracat", "1.0"}, {"iş", "1.0"}, {"cari", "1.0"}
                , {"finans", "1.0"}, {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"banka", "1.0"}, {"TCMB", "1.0"}
                , {"kriz", "1.0"}, {"iktisat", "1.0"}, {"hazine", "1.0"}, {"enflasyon", "1.0"}, {"iktisadi", "1.0"}
                //Borsa ile ilgili olanlar
                , {"BISTIOO", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"}, {"IMKB100", "1.1"}, {"endeks", "1.1"}
                , {"tahvil", "1.1"}, {"risk", "1.1"}
                // Döviz ile ilgili olanlar
                , {"dolar", "1.2"}, {"euro", "1.2"}, {"Sterlin", "1.2"}, {"Kronu", "1.2"}, {"Frangı", "1.2"}
                , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"}
                // Altın ile ilgili olanlar
                , {"altın", "1.3"}, {"ayar", "1.3"}, {"ons", "1.3"}
                // Petrol ile ilgililer
                , {"petrol", "1.4"}, {"varil", "1.4"}, {"brent petrol", "1.4"}, {"brent", "1.4"}, {"ham petrol", "1.4"}
            };
            List<String> muzikKokListesi = new List<String> { "müzik", "plak", "şarkı", "konser", "albüm", "klip"};
            
            String file = "newdoc.xls";                            // Excel dosyasının adı
            Workbook workbook = new Workbook();                    // Excel sayfasının ekleneceği kitap
            Worksheet worksheet = new Worksheet("First Sheet");    // Excel sayfasının adı
            String[] words;                                        // Tweet içerisinde geçen kelimeleri barındırır
            String wordType;                                       // Kelimenin OZNE, YUKLEM, FIIL gibi bir grupta olduğu tutulur 
            String oldCellContent;                                 // O anki excel hücresinde önceden yazılmış bir içerik varsa onu barındırır
            Regex rgx = new Regex(@"[^\w\s\-]*");                  // Kelimelerden non-alphanumerik karakterleri çıkarmak için
            List<TwitterStatus> tweetList = tweets.ToList();       // IENumerable'dan Liste'ye dönüştürmek için

            worksheet.Cells[0, 0] = new Cell("Konu ID");
            worksheet.Cells[0, 1] = new Cell("Konu");
            worksheet.Cells[0, 2] = new Cell("Tweet ID");
            worksheet.Cells[0, 3] = new Cell("Tweet");
            worksheet.Cells[0, 4] = new Cell("User");
            worksheet.Cells[0, 5] = new Cell("Tarih/Saat");
            worksheet.Cells[0, 6] = new Cell("Sayısal Veri");
            worksheet.Cells[0, 7] = new Cell("URL'ler");
            worksheet.Cells[0, 8] = new Cell("Media");
            worksheet.Cells[0, 9] = new Cell("RT Sayısı");
            worksheet.Cells[0, 10] = new Cell("Fav Sayısı");
            worksheet.Cells[0, 11] = new Cell("Dili");
            worksheet.Cells[0, 12] = new Cell("Hashtag'ler");
            worksheet.Cells[0, 13] = new Cell("Kimden Retweetlenmiş");
            worksheet.Cells[0, 14] = new Cell("Smileys");
            worksheet.Cells[0, 15] = new Cell("Kelime-kelime Ayrıştırma");
            worksheet.Cells[0, 16] = new Cell("Öğelere ayırma");
            worksheet.Cells[0, 17] = new Cell("Olumsuz fiil");
            worksheet.Cells[0, 18] = new Cell("Konunun temel aldığı anahtar kelime");

            for (int i = 0; i < tweets.Count(); i++)
            {
                worksheet.Cells[i + 1, 0] = new Cell(i);                                                 // Konu ID
                worksheet.Cells[i + 1, 2] = new Cell(tweetList[i].Id.ToString());                        // Tweet ID
                worksheet.Cells[i + 1, 3] = new Cell(tweetList[i].Text);                                 // Haber Metni
                worksheet.Cells[i + 1, 4] = new Cell(tweetList[i].Author.ScreenName.ToString());         // Tweet'i yayınlayan kişi
                worksheet.Cells[i + 1, 5] = new Cell(tweetList[i].CreatedDate.ToString());               // Tarih/Saat
                #region URL'ler
                if (tweetList[i].Entities.Urls.Count != 0)
                {
                    foreach (var item in tweetList[i].Entities.Urls)
                    {
                        if (worksheet.Cells[i + 1, 7].Value == null)
                        {
                            worksheet.Cells[i + 1, 7] = new Cell(item.ExpandedValue.ToString());
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 7].StringValue;
                            worksheet.Cells[i + 1, 7] = new Cell(oldCellContent + "," + item.ExpandedValue.ToString());
                        }
                    }

                }
                #endregion
                #region Media
                if (tweetList[i].Entities.Media.Count != 0)
                {
                    foreach (var item in tweetList[i].Entities.Urls)
                    {
                        if (worksheet.Cells[i + 1, 8].Value == null)
                        {
                            worksheet.Cells[i + 1, 8] = new Cell(item.DisplayUrl.ToString());
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 8].StringValue;
                            worksheet.Cells[i + 1, 8] = new Cell(oldCellContent + "," + item.DisplayUrl.ToString());
                        }
                    }

                }
                #endregion
                #region RT Sayısı
                worksheet.Cells[i + 1, 9] = new Cell(tweetList[i].RetweetCount.ToString());
                #endregion
                #region Fav Sayısı // Not yet implemented
                
                #endregion
                #region Dili
                    worksheet.Cells[i + 1, 11] = new Cell(tweetList[i].Language);
                #endregion
                #region Hashtag'ler
                if (tweetList[i].Entities.HashTags.Count != 0)
                {
                    foreach (var item in tweetList[i].Entities.HashTags)
                    {
                        if (worksheet.Cells[i + 1, 12].Value == null)
                        {
                            worksheet.Cells[i + 1, 12] = new Cell(item.Text.ToString());
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 12].StringValue;
                            worksheet.Cells[i + 1, 12] = new Cell(oldCellContent + "," + item.Text.ToString());
                        }
                    }

                }
                #endregion
                #region Mention'lar
                if (tweetList[i].Entities.Mentions.Count != 0)
                {
                    foreach (var item in tweetList[i].Entities.HashTags)
                    {
                        if (worksheet.Cells[i + 1, 12].Value == null)
                        {
                            worksheet.Cells[i + 1, 12] = new Cell(item.Text.ToString());
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 12].StringValue;
                            worksheet.Cells[i + 1, 12] = new Cell(oldCellContent + "," + item.Text.ToString());
                        }
                    }

                }
                #endregion
                #region Kimden Retweet
                if (tweetList[i].RetweetedStatus != null)
                {
                    worksheet.Cells[i + 1, 13] = new Cell(tweetList[i].RetweetedStatus.Author.ScreenName.ToString());
                }
                #endregion
                

                words = tweetList[i].Text.Split(' ');         // Cümleyi her boşluğa göre kelimelere ayırır
                foreach (String w in words)                     
                {
                    String word = rgx.Replace(w, "");         // Eğer non-alphanumerik karakter varsa kelimeden temizle
                    #region Empty String'ler
                    if (word == "")
                    {
                        continue;
                    }
                    #endregion
                    #region Smiley'ler
                    else if (Regex.IsMatch(word, @"(?::|;|=)(?:-)?(?:\)|\(|D|P)") && word.First() != '@')
                    {
                        worksheet.Cells[i + 1, 14] = new Cell(word);
                    }
                    #endregion
                    #region Kelime-kelime Ayrıştırma
                    if (worksheet.Cells[i + 1, 15].Value == null)
                    {
                        worksheet.Cells[i + 1, 15] = new Cell(word);
                    }
                    else
                    {
                        oldCellContent = worksheet.Cells[i + 1, 15].StringValue;
                        worksheet.Cells[i + 1, 15] = new Cell(oldCellContent + "," + word);
                    }
                    #endregion
                    #region Anahtar kelimeler
                    if (ekonomiListesi.ContainsKey(word))
                    {
                        worksheet.Cells[i + 1, 1] = new Cell(ekonomiListesi[word]);
                        if (worksheet.Cells[i + 1, 18].Value == null)
                        {
                            worksheet.Cells[i + 1, 18] = new Cell(word);
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 18].StringValue;
                            worksheet.Cells[i + 1, 18] = new Cell(oldCellContent + ", " + word);
                        }
                    }
                    #endregion
                    #region Öğelere ayırma // Uzun Süre alıyor
                    else
                    try
                    {
                        Kelime[] kelime = zemberek.kelimeCozumle("gelme");
                        Kok kelimeKoku = kelime.First().kok();
                        wordType = kelimeKoku.tip().ToString();
                        if (ekonomiListesi.ContainsKey(kelimeKoku.icerik().ToString()))
                        {
                            worksheet.Cells[i + 1, 1] = new Cell(ekonomiListesi[kelimeKoku.icerik().ToString()]);
                            if (worksheet.Cells[i + 1, 18].Value == null)
                            {
                                worksheet.Cells[i + 1, 18] = new Cell(word);
                            }
                            else
                            {
                                oldCellContent = worksheet.Cells[i + 1, 18].StringValue;
                                worksheet.Cells[i + 1, 18] = new Cell(oldCellContent + ", " + word);
                            }
                        }
                        else if (muzikKokListesi.Contains(kelimeKoku.icerik().ToString()))
                        {
                            worksheet.Cells[i + 1, 1] = new Cell("2.0");
                            if (worksheet.Cells[i + 1, 18].Value == null)
                            {
                                worksheet.Cells[i + 1, 18] = new Cell(word);
                            }
                            else
                            {
                                oldCellContent = worksheet.Cells[i + 1, 18].StringValue;
                                worksheet.Cells[i + 1, 18] = new Cell(oldCellContent + ", " + word);
                            }
                        }
                        if (wordType == "FIIL" )
                        {
                            foreach (var ek in kelime.First().ekler())
                            {
                                if (ek.ad() == "FIIL_OLUMSUZLUK_ME")
                                {
                                    worksheet.Cells[i + 1, 17] = new Cell(word);
                                    break;
                                }
                            }
                        }
                        if (worksheet.Cells[i + 1, 16].Value == null)
                        {
                            worksheet.Cells[i + 1, 16] = new Cell(wordType);
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 16].StringValue;
                            worksheet.Cells[i + 1, 16] = new Cell(oldCellContent + ", " + wordType);
                        }
                    }
                    catch (Exception)
                    {
                        if (worksheet.Cells[i + 1, 16].Value == null)
                        {
                            worksheet.Cells[i + 1, 16] = new Cell("BULUNAMADI");
                        }
                        else
                        {
                            oldCellContent = worksheet.Cells[i + 1, 16].StringValue;
                            worksheet.Cells[i + 1, 16] = new Cell(oldCellContent + ", " + "BULUNAMADI");
                        }

                    }
                    #endregion
                }
            }
            CellCollection cells = worksheet.Cells;
            workbook.Worksheets.Add(worksheet);
            workbook.Save(file);
        }
    }
}
