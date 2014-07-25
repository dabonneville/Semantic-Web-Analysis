using net.zemberek.erisim;
using net.zemberek.tr.yapi;
using net.zemberek.yapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp
{
    public class ZemberekAnaliz
    {
        Zemberek zemberek;
        #region Static Properties
        public static Dictionary<String, String> konuList = new Dictionary<String, String>() 
        {
            {"ekonomi", "1.0"}, {"borsa", "1.1"}, {"döviz", "1.2"}, {"altın", "1.3"}, {"petrol", "1.4"}
        };
        #region En son Dictionary'ler 18:46 10.03.2014
        public static Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
            { 
                {"ayı", "1.0"}, {"boğa", "1.0"},  {"para", "1.0"}, {"cari", "1.0"}, {"ticaret", "1.0"}
                , {"faiz", "1.0"}, {"ihracat", "1.0"}, {"ithalat", "1.0"}
                , {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"maliyet", "1.0"}
                , {"kredi", "1.0"},  {"milyon", "1.0"}, {"milyar", "1.0"}, {"satış", "1.0"}, {"alış", "1.0"}
                , {"yatırım", "1.0"}, {"sat", "1.0"}, {"al", "1.0"}
                , {"TL", "1.0"}, {"lira", "1.0"}
                , {"likitide", "1.0"}, {"oynaklık", "1.0"}, {"TEFE", "1.0"}, {"TÜFE", "1.0"}
                , {"portföy", "1.0"}, {"resesyon", "1.0"}, {"büyüme", "1.0"}, {"varlık", "1.0"}
                , {"hedef","1.0"},  {"plan","1.0"},  {"hedge","1.0"},  {"yatırımcı","1.0"},  {"liberalizasyon","1.0"}
                , {"fiyatlama","1.0"}, {"konjonktürel","1.0"}, {"vadeli","1.0"}
                , {"pricing", "1.0"}, {"finansman","1.0"}, {"temettü","1.0"}, {"trend","1.0"},  {"bekle ve gör","1.0"}
                , {"borç tavanı" , "1.0"}, {"kırılgan","1.0"},  {"toparlanma","1.0"}
                , {"trilyon", "1.0"}, { "günlük","1.0"},  {"artış","1.0"},  {"global","1.0"}, { "yerli", "1.0"}
                , {"stok","1.0"}
                , {"kriz", "1.0"}, {"yatırım aracı", "1.0"}, {"kaldıraç", "1.0"}, {"kırılma", "1.0"}, {"rezerv", "1.0"}
                , {"veri", "1.0"}, {"zam", "1.0"}, {"istikrar", "1.0"}
                , {"enflasyon", "1.0"}
            };

        public static Dictionary<String, String> borsaListesi = new Dictionary<String, String>() 
             {
                {"BIST100", "1.1"}, {"BIST", "1.1"},{"BIST30", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"} ,{"bin", "1.1"}
                , {"IMKB100", "1.1"}, {"endeks", "1.1"}, {"tahvil", "1.1"}, {"Fed", "1.1"},{"piyasa", "1.1"}
                , {"para", "1.1"}, {"banka","1.1"}, {"kriz", "1.1"}, {"bono", "1.1"}, {"risk", "1.1"}, {"cari", "1.1"}
                , {"ayı", "1.1"}, {"boğa", "1.1"}, {"faiz", "1.1"}, {"sermaye", "1.1"}, {"finans", "1.1"},{"milyon", "1.1"}, {"milyar", "1.1"}
                , {"kredi", "1.1"},{"satış", "1.1"},{"alış", "1.1"}, {"yatırım", "1.1"},{"sat", "1.1"},{"al", "1.1"}
                , {"TL", "1.1"},{"açığa", "1.1"},{"pozisyon", "1.1"},{"broker", "1.1"},{"trade", "1.1"},{"forward", "1.1"}
                , {"short", "1.1"},{"long", "1.1"},{"vop", "1.1"},{"viop", "1.1"},{"kontrat", "1.1"}
                , {"future", "1.1"},{"kaldıraç", "1.1"},{"likitide", "1.1"},{"oynaklık", "1.1"},{"TEFE", "1.1"},{"TÜFE", "1.1"}
                , {"portföy", "1.1"},{"resesyon", "1.1"},{"büyüme", "1.1"},{"spot", "1.1"},{"analiz", "1.1"},{"türev", "1.1"}
                , {"varlık", "1.1"},{"fon", "1.1"},{"hedge", "1.1"}
                , { "trilyon", "1.1"},{ "günlük","1.1"},  {"artış","1.1"},  {"global","1.1"}, { "yerli", "1.1"}, {"konjonktürel","1.1"}, {"vadeli","1.1"}
                , { "band","1.1"},{ "bandı","1.1"}, {"kırım","1.1"}, {"kırdı","1.1"},{"kırıldı","1.1"} 
                , {"hedef","1.1"},  {"plan","1.1"}, {"yatırımcı","1.1"},  {"liberalizasyon","1.1"}, {"fiyatlama","1.1"}
                , {"pricing", "1.1"}, {"finansman","1.1"}, {"temettü","1.1"},{"trend","1.1"},  {"bekle ve gör","1.1"}
                , {"borç tavanı" , "1.1"}, {"kırılgan","1.1"},  {"toparlanma","1.1"}
                , {"esnek","1.1"}, {  "stok","1.1"}
                , {"yatırım aracı", "1.1"},{"kırılma", "1.1"},{"veri", "1.1"}
                , {"istikrar", "1.1"}
            };
        public static Dictionary<String, String> dovizListesi = new Dictionary<String, String>() 
            {
                {"döviz", "1.2"}, {"dolar", "1.2"}, {"euro", "1.2"}, {"cari", "1.2"}, {"Sterlin", "1.2"}, {"Kron", "1.2"}, {"Frang", "1.2"}
                , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"} , {"YEN", "1.2"},{"kur", "1.2"}
                , {"para", "1.2"}, {"TCMB", "1.2"}, {"hazine", "1.2"}, {"faiz", "1.2"}, {"borç", "1.2"}
                , {"deflasyon", "1.2"}, {"ticaret", "1.2"},{"milyon", "1.2"}, {"milyar", "1.2"},{"piyasa", "1.2"}
                , {"kredi", "1.2"},{"satış", "1.2"},{"alış", "1.2"}, {"yatırım", "1.2"},{"sat", "1.2"},{"al", "1.2"}
                , {"fiyat", "1.2"},{"TL", "1.2"},{"lira", "1.2"},{"banknot", "1.2"},{"çapraz", "1.2"},{"devalüasyon", "1.2"}
                , {"dolarizasyon", "1.2"},{"efektif", "1.2"},{"ihale", "1.2"},{"likidite", "1.2"},{"oynaklık", "1.2"}
                , {"müdahale", "1.2"},{"geçişkenlik", "1.2"},{"TEFE", "1.2"},{"TÜFE", "1.2"},{"portföy", "1.2"},{"repo", "1.2"}
                , {"resesyon", "1.2"},{"büyüme", "1.2"}
                , { "trilyon", "1.2"},{ "günlük","1.2"},  {"artış","1.2"},  {"global","1.2"}, { "yerli", "1.2"}, {"konjonktürel","1.2"}, {  "vadeli","1.2"}
                , { "band","1.2"},{ "bandı","1.2"}, {"kırım","1.2"}, {"kırdı","1.2"},{"kırıldı","1.2"}, {"kontrat","1.2"} 
                , {"hedef","1.2"},  {"plan","1.2"},  {"hedge","1.2"},  {"yatırımcı","1.2"},  {"liberalizasyon","1.2"}, {"fiyatlama","1.2"}
                , {"pricing", "1.2"}, {"finansman","1.2"}, {"temettü","1.2"},{"trend","1.2"},  {"bekle ve gör","1.2"}
                , {"borç tavanı" , "1.2"}, {"kırılgan","1.2"},  {"toparlanma","1.2"}
                , {  "stok","1.2"}
                , {"kriz", "1.2"},{"yatırım aracı", "1.2"},{"kaldıraç", "1.2"},{"kırılma", "1.2"},{"rezerv", "1.2"}
                , {"veri", "1.2"},{"istikrar", "1.2"}
            };
        public static Dictionary<String, String> altinListesi = new Dictionary<String, String>() 
            {
                 {"altın", "1.3"}, {"ayar", "1.3"}, {"ons", "1.3"}, {"gram", "1.3"}, {"gr", "1.3"}, {"külçe", "1.3"}
                , {"ziynet", "1.3"}, {"ayı", "1.3"}, {"boğa", "1.3"}, {"ihracat", "1.3"},{"ithalat", "1.3"}
                , {"satış", "1.3"},{"alış", "1.3"},{"fiyat", "1.3"},{"piyasa", "1.3"},{"maliyet", "1.3"}, {"TL", "1.3"}
                , {"ticaret", "1.3"},{"milyon", "1.3"}, {"milyar", "1.3"}, {"yatırım", "1.3"},{"sat", "1.3"},{"al", "1.3"}
                , {"lira", "1.3"},{"portföy", "1.3"},{"varlık", "1.3"}
                , { "trilyon", "1.3"},{ "günlük","1.3"},  {"artış","1.3"},  {"global","1.3"}, { "yerli", "1.3"}, {"konjonktürel","1.3"}, {"vadeli","1.3"}
                , {"hedef","1.3"},  {"plan","1.3"},  {"hedge","1.3"},  {"yatırımcı","1.3"},  {"liberalizasyon","1.3"}, {"fiyatlama","1.3"}
                , {"pricing", "1.3"}, {"finansman","1.3"}, {"temettü","1.3"},{"trend","1.3"},  {"bekle ve gör","1.3"}
                , {"borç tavanı" , "1.3"}, {"kırılgan","1.3"},  {"toparlanma","1.3"}
                , {"stok","1.3"}
                , {"kriz", "1.3"},{"yatırım aracı", "1.3"},{"kaldıraç", "1.3"},{"kırılma", "1.3"},{"rezerv", "1.3"}
                , {"cumhuriyet", "1.3"},{"çeyrek", "1.3"},{"ata", "1.3"},{"darphane", "1.3"},{"kuyumcu", "1.3"},{"hurda", "1.3"}
                , {"ankese", "1.3"},{"bilezik", "1.3"},{"emtia", "1.3"},{"gümüş", "1.3"},{"gremse", "1.3"}
                , {"veri", "1.3"},{"istikrar", "1.3"}
                , {"filo", "1.3"}, {"savaş", "1.3"}, {"asker", "1.3"}, {"ordu", "1.3"}, {"yaptırım", "1.3"}, {"darboğaz", "1.3"}
            };
        public static Dictionary<String, String> petrolListesi = new Dictionary<String, String>() 
            {
                {"LPG", "1.4"}, {"doğalgaz", "1.4"}, {"doğalgaza", "1.4"}, {"yakıt", "1.4"}, {"benzin", "1.4"}, {"dizel", "1.4"}
                , {"motorin", "1.4"}, {"otogaz", "1.4"}, {"petrol", "1.4"}, {"varil", "1.4"}, {"brent", "1.4"}
                , {"ham petrol", "1.4"}, {"akaryakıt", "1.4"}, {"Petkim", "1.4"}, {"petrokimya", "1.4"}
                , {"ihracat", "1.4"}, {"ithalat", "1.4"},{"ticaret", "1.4"},{"milyon", "1.4"}, {"milyar", "1.4"}
                , {"fiyat", "1.4"},{"piyasa", "1.4"},{"maliyet", "1.4"},{"varlık", "1.4"}
                , { "trilyon", "1.4"},{ "günlük","1.4"},  {"artış","1.4"},  {"global","1.4"}, { "yerli", "1.4"}, {"konjonktürel","1.4"}, {  "vadeli","1.4"}
                , {"mazot", "1.4"}, {"enerji","1.4"}, {"jeoplotik", "1.4"},{"geopolitics","1.4"}, {"bölgesel", "1.4"}
                , {"yenilenebilir", "1.4"},{"enerji arzı", "1.4"}, {"enerji sektörü", "1.4"}
                , {"hedef","1.4"},  {"plan","1.4"},  {"hedge","1.4"},  {"yatırımcı","1.4"},  {"liberalizasyon","1.4"}, {"fiyatlama","1.4"}
                , {"pricing", "1.4"}, {"finansman","1.4"}, {"temettü","1.4"},{"trend","1.4"},  {"bekle ve gör","1.4"}
                , {"borç tavanı" , "1.4"}, {"kırılgan","1.4"},  {"toparlanma","1.4"}
                , {  "stok","1.4"}
                , {"kriz", "1.4"},{"yatırım aracı", "1.4"},{"rezerv", "1.4"},{"veri", "1.4"},{"istikrar", "1.4"}
            };
        #endregion
        #region Dictionary'ler
        // Ekonomi ile ilgili olanlar
        //public static Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
        //{ 
        //    {"boğa", "1.0"},  {"para", "1.0"}, {"cari", "1.0"}, {"ticaret", "1.0"}
        //    , {"faiz", "1.0"}, {"ihracat", "1.0"},{"ithalat", "1.0"}
        //    , {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"maliyet", "1.0"}
        //    , {"kredi", "1.0"},  {"milyon", "1.0"}, {"milyar", "1.0"}, {"satış", "1.0"},{"alış", "1.0"}
        //    , {"yatırım", "1.0"}, {"sat", "1.0"},{"al", "1.0"}
        //    , {"TL", "1.0"}, {"lira", "1.0"}
        //    , {"likitide", "1.0"},{"oynaklık", "1.0"},{"TEFE", "1.0"},{"TÜFE", "1.0"}
        //    , {"portföy", "1.0"},{"resesyon", "1.0"},{"büyüme", "1.0"},{"varlık", "1.0"}
        //    , {"hedef","1.0"},  {"plan","1.0"},  {"hedge","1.0"},  {"yatırımcı","1.0"},  {"liberalizasyon","1.0"}, {"fiyatlama","1.0"}
        //    , {"pricing", "1.0"}, {"finansman","1.0"}, {"temettü","1.0"},{"trend","1.0"},  {"bekle ve gör","1.0"}
        //    , {"borç tavanı" , "1.0"}, {"kırılgan","1.0"},  {"toparlanma","1.0"}
        //    , {"trilyon", "1.0"},{ "günlük","1.0"},  {"artış","1.0"},  {"global","1.0"}, { "yerli", "1.0"}, {"konjonktürel","1.0"}, {  "vadeli","1.0"}
        //    , {"stok","1.0"}, {"enflasyon", "1.0"}
        //};
        //        //Borsa ile ilgili olanlar
        //public static Dictionary<String, String> borsaListesi = new Dictionary<String, String>() 
        //{
        //    {"BIST100", "1.1"}, {"BIST", "1.1"},{"BIST30", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"} ,{"bin", "1.1"}
        //    , {"IMKB100", "1.1"}, {"endeks", "1.1"}, {"tahvil", "1.1"}, {"Fed", "1.1"},{"piyasa", "1.1"}
        //    , {"para", "1.1"}, {"banka","1.1"}, {"kriz", "1.1"}, {"bono", "1.1"}, {"risk", "1.1"}, {"cari", "1.1"}
        //    , {"boğa", "1.1"}, {"faiz", "1.1"}, {"sermaye", "1.1"}, {"finans", "1.1"},{"milyon", "1.1"}, {"milyar", "1.1"}
        //    , {"kredi", "1.1"},{"satış", "1.1"},{"alış", "1.1"}, {"yatırım", "1.1"},{"sat", "1.1"},{"al", "1.1"}
        //    , {"TL", "1.1"},{"açığa", "1.1"},{"pozisyon", "1.1"},{"broker", "1.1"},{"trade", "1.1"},{"forward", "1.1"}
        //    , {"short", "1.1"},{"long", "1.1"},{"vop", "1.1"},{"viop", "1.1"},{"kontrat", "1.1"}
        //    , {"future", "1.1"},{"kaldıraç", "1.1"},{"likitide", "1.1"},{"oynaklık", "1.1"},{"TEFE", "1.1"},{"TÜFE", "1.1"}
        //    , {"portföy", "1.1"},{"resesyon", "1.1"},{"büyüme", "1.1"},{"spot", "1.1"},{"analiz", "1.1"},{"türev", "1.1"}
        //    , {"varlık", "1.1"},{"fon", "1.1"},{"hedge", "1.1"}
        //    , {"trilyon", "1.1"},{ "günlük","1.1"},  {"artış","1.1"},  {"global","1.1"}, { "yerli", "1.1"}, {"konjonktürel","1.1"}, {  "vadeli","1.1"}
        //    , {"band","1.1"},{ "bandı","1.1"}, {"kırım","1.1"}, {"kırdı","1.1"},{"kırıldı","1.1"} 
        //    , {"hedef","1.1"},  {"plan","1.1"}, {"yatırımcı","1.1"},  {"liberalizasyon","1.1"}, {"fiyatlama","1.1"}
        //    , {"pricing", "1.1"}, {"finansman","1.1"}, {"temettü","1.1"},{"trend","1.1"},  {"bekle ve gör","1.1"}
        //    , {"borç tavanı" , "1.1"}, {"kırılgan","1.1"},  {"toparlanma","1.1"}
        //    , {"esnek","1.1"}, {"stok","1.1"}
        //};
        //        // Döviz ile ilgili olanlar
        //public static Dictionary<String, String> dovizListesi = new Dictionary<String, String>() 
        //{
        //    {"döviz", "1.2"}, {"dolar", "1.2"}, {"euro", "1.2"}, {"cari", "1.2"}, {"Sterlin", "1.2"}, {"Kron", "1.2"}, {"Frang", "1.2"}
        //    , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"} , {"YEN", "1.2"},{"kur", "1.2"}, {"kuru", "1.2"}
        //    , {"para", "1.2"}, {"TCMB", "1.2"}, {"hazine", "1.2"}, {"faiz", "1.2"}, {"borç", "1.2"}
        //    , {"deflasyon", "1.2"}, {"ticaret", "1.2"},{"milyon", "1.2"}, {"milyar", "1.2"},{"piyasa", "1.2"}
        //    , {"kredi", "1.2"},{"satış", "1.2"},{"alış", "1.2"}, {"yatırım", "1.2"},{"sat", "1.2"},{"al", "1.2"}
        //    , {"fiyat", "1.2"},{"TL", "1.2"},{"lira", "1.2"},{"banknot", "1.2"},{"çapraz", "1.2"},{"devalüasyon", "1.2"}
        //    , {"dolarizasyon", "1.2"},{"efektif", "1.2"},{"ihale", "1.2"},{"likidite", "1.2"},{"oynaklık", "1.2"}
        //    , {"müdahale", "1.2"},{"geçişkenlik", "1.2"},{"TEFE", "1.2"},{"TÜFE", "1.2"},{"portföy", "1.2"},{"repo", "1.2"}
        //    , {"resesyon", "1.2"},{"büyüme", "1.2"}
        //    , {"trilyon", "1.2"},{ "günlük","1.2"},  {"artış","1.2"},  {"global","1.2"}, { "yerli", "1.2"}, {"konjonktürel","1.2"}, {  "vadeli","1.2"}
        //    , {"band","1.2"},{ "bandı","1.2"}, {"kırım","1.2"}, {"kırdı","1.2"},{"kırıldı","1.2"}, {"kontrat","1.2"} 
        //    , {"hedef","1.2"},  {"plan","1.2"},  {"hedge","1.2"},  {"yatırımcı","1.2"},  {"liberalizasyon","1.2"}, {"fiyatlama","1.2"}
        //    , {"pricing", "1.2"}, {"finansman","1.2"}, {"temettü","1.2"},{"trend","1.2"},  {"bekle ve gör","1.2"}
        //    , {"borç tavanı" , "1.2"}, {"kırılgan","1.2"},  {"toparlanma","1.2"}
        //    , {"stok","1.2"}
        //};

        //        // Altın ile ilgili olanlar
        //public static Dictionary<String, String> altinListesi = new Dictionary<String, String>() 
        //{
        //         {"altın", "1.3"}, {"ons", "1.3"}, {"gram", "1.3"}, {"gr", "1.3"}, {"külçe", "1.3"}
        //        , {"ayar", "1.3"}, {"ziynet", "1.3"}, {"boğa", "1.3"}, {"ihracat", "1.3"},{"ithalat", "1.3"}
        //        , {"satış", "1.3"},{"alış", "1.3"},{"fiyat", "1.3"},{"piyasa", "1.3"},{"maliyet", "1.3"}, {"TL", "1.3"}
        //        , {"ticaret", "1.3"},{"milyon", "1.3"}, {"milyar", "1.3"}, {"yatırım", "1.3"},{"sat", "1.3"},{"al", "1.3"}
        //        , {"lira", "1.3"}, {"portföy", "1.3"},{"varlık", "1.3"}
        //        , { "trilyon", "1.3"},{ "günlük","1.3"},  {"artış","1.3"},  {"global","1.3"}, { "yerli", "1.3"}, {"konjonktürel","1.3"}, {"vadeli","1.3"}
        //        , {"hedef","1.3"},  {"plan","1.3"},  {"hedge","1.3"},  {"yatırımcı","1.3"},  {"liberalizasyon","1.3"}, {"fiyatlama","1.3"}
        //        , {"pricing", "1.3"}, {"finansman","1.3"}, {"temettü","1.3"},{"trend","1.3"},  {"bekle ve gör","1.3"}
        //        , {"borç tavanı" , "1.3"}, {"kırılgan","1.3"},  {"toparlanma","1.3"}
        //        , { "stok","1.3"}
        //};
        //        // Petrol ile ilgililer
        //public static Dictionary<String, String> petrolListesi = new Dictionary<String, String>() 
        //{
        //    {"LPG", "1.4"}, {"doğalgaz", "1.4"}, {"doğalgaza", "1.4"}, {"yakıt", "1.4"}, {"benzin", "1.4"}, {"dizel", "1.4"}
        //    , {"motorin", "1.4"}, {"otogaz", "1.4"}, {"petrol", "1.4"}, {"varil", "1.4"}, {"brent", "1.4"}
        //    , {"ham petrol", "1.4"}, {"akaryakıt", "1.4"}, {"Petkim", "1.4"}, {"petrokimya", "1.4"}
        //    , {"ihracat", "1.4"}, {"ithalat", "1.4"},{"ticaret", "1.4"},{"milyon", "1.4"}, {"milyar", "1.4"}
        //    , {"fiyat", "1.4"},{"piyasa", "1.4"},{"maliyet", "1.4"},{"varlık", "1.4"}
        //    , { "trilyon", "1.4"},{ "günlük","1.4"},  {"artış","1.4"},  {"global","1.4"}, { "yerli", "1.4"}, {"konjonktürel","1.4"}, {  "vadeli","1.4"}
        //    , { "mazot", "1.4"}, {"enerji", "1.4"}, {"jeoplotik", "1.4"},{"geopolitics","1.4"}, {"bölgesel", "1.4"}
        //    , {"yenilenebilir", "1.4"},{"enerji arzı", "1.4"}, {"enerji sektörü","1.4"}
        //    , {"hedef","1.4"},  {"plan","1.4"},  {"hedge","1.4"},  {"yatırımcı","1.4"},  {"liberalizasyon","1.4"}, {"fiyatlama","1.4"}
        //    , {"pricing", "1.4"}, {"finansman","1.4"}, {"temettü","1.4"},{"trend","1.4"},  {"bekle ve gör","1.4"}
        //    , {"borç tavanı" , "1.4"}, {"kırılgan","1.4"},  {"toparlanma","1.4"}
        //    , {"stok","1.4"}
        //};
        #endregion
        #region 21.52 09 Mart 14 Dictionary
        //public static Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
        //    { 
        //        {"ayı", "1.0"}, {"boğa", "1.0"},  {"para", "1.0"}, {"cari", "1.0"}, {"ticaret", "1.0"}
        //        , {"faiz", "1.0"}, {"ihracat", "1.0"},{"ithalat", "1.0"}
        //        , {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"maliyet", "1.0"}
        //        , {"kredi", "1.0"},  {"milyon", "1.0"}, {"milyar", "1.0"}, {"satış", "1.0"},{"alış", "1.0"}
        //        , {"yatırım", "1.0"}, {"sat", "1.0"},{"al", "1.0"}
        //        , {"TL", "1.0"}, {"lira", "1.0"}
        //        , {"likitide", "1.0"},{"oynaklık", "1.0"},{"TEFE", "1.0"},{"TÜFE", "1.0"}
        //        , {"portföy", "1.0"},{"resesyon", "1.0"},{"büyüme", "1.0"},{"varlık", "1.0"}
        //        , {"hedef","1.0"},  {"plan","1.0"},  {"hedge","1.0"},  {"yatırımcı","1.0"},  {"liberalizasyon","1.0"}, {"fiyatlama","1.0"}
        //        , {"pricing", "1.0"}, {"finansman","1.0"}, {"temettü","1.0"},{"trend","1.0"},  {"bekle ve gör","1.0"}
        //        , {"borç tavanı" , "1.0"}, {"kırılgan","1.0"},  {"toparlanma","1.0"}
        //        , {"trilyon", "1.0"},{ "günlük","1.0"},  {"artış","1.0"},  {"global","1.0"}, { "yerli", "1.0"}, {"konjonktürel","1.0"}, {  "vadeli","1.0"}
        //        , {"stok","1.0"}

        //        //Borsa ile ilgili olanlar
        //        , {"BIST100", "1.1"}, {"BIST", "1.1"},{"BIST30", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"} ,{"bin", "1.1"}
        //        , {"IMKB100", "1.1"}, {"endeks", "1.1"}, {"tahvil", "1.1"}, {"Fed", "1.1"},{"piyasa", "1.1"}
        //        , {"para", "1.1"}, {"banka","1.1"}, {"kriz", "1.1"}, {"bono", "1.1"}, {"risk", "1.1"}, {"cari", "1.1"}
        //        , {"ayı", "1.1"}, {"boğa", "1.1"}, {"faiz", "1.1"}, {"sermaye", "1.1"}, {"finans", "1.1"},{"milyon", "1.1"}, {"milyar", "1.1"}
        //        , {"kredi", "1.1"},{"satış", "1.1"},{"alış", "1.1"}, {"yatırım", "1.1"},{"sat", "1.1"},{"al", "1.1"}
        //        , {"TL", "1.1"},{"açığa", "1.1"},{"pozisyon", "1.1"},{"broker", "1.1"},{"trade", "1.1"},{"forward", "1.1"}
        //        , {"short", "1.1"},{"long", "1.1"},{"vop", "1.1"},{"viop", "1.1"},{"kontrat", "1.1"}
        //        , {"future", "1.1"},{"kaldıraç", "1.1"},{"likitide", "1.1"},{"oynaklık", "1.1"},{"TEFE", "1.1"},{"TÜFE", "1.1"}
        //        , {"portföy", "1.1"},{"resesyon", "1.1"},{"büyüme", "1.1"},{"spot", "1.1"},{"analiz", "1.1"},{"türev", "1.1"}
        //        , {"varlık", "1.1"},{"fon", "1.1"},{"hedge", "1.1"}
        //        , {"trilyon", "1.1"},{ "günlük","1.1"},  {"artış","1.1"},  {"global","1.1"}, { "yerli", "1.1"}, {"konjonktürel","1.1"}, {  "vadeli","1.1"}
        //        , {"band","1.1"},{ "bandı","1.1"}, {"kırım","1.1"}, {"kırdı","1.1"},{"kırıldı","1.1"}, {"kontrat","1.1"} 
        //        , {"hedef","1.1"},  {"plan","1.1"}, {"yatırımcı","1.1"},  {"liberalizasyon","1.1"}, {"fiyatlama","1.1"}
        //        , {"pricing", "1.1"}, {"finansman","1.1"}, {"temettü","1.1"},{"trend","1.1"},  {"bekle ve gör","1.1"}
        //        , {"borç tavanı" , "1.1"}, {"kırılgan","1.1"},  {"toparlanma","1.1"}
        //        , {"esnek","1.1"}, {"stok","1.1"}

        //        // Döviz ile ilgili olanlar
        //        , {"döviz", "1.2"}, {"dolar", "1.2"}, {"euro", "1.2"}, {"cari", "1.2"}, {"Sterlin", "1.2"}, {"Kron", "1.2"}, {"Frang", "1.2"}
        //        , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"} , {"YEN", "1.2"},{"kur", "1.2"}
        //        , {"para", "1.2"}, {"TCMB", "1.2"}, {"hazine", "1.2"}, {"faiz", "1.2"}, {"borç", "1.2"}
        //        , {"enflasyon", "1.2"},{"deflasyon", "1.2"}, {"ticaret", "1.2"},{"milyon", "1.2"}, {"milyar", "1.2"},{"piyasa", "1.2"}
        //        , {"kredi", "1.2"},{"satış", "1.2"},{"alış", "1.2"}, {"yatırım", "1.2"},{"sat", "1.2"},{"al", "1.2"}
        //        , {"fiyat", "1.2"},{"TL", "1.2"},{"lira", "1.2"},{"banknot", "1.2"},{"çapraz", "1.2"},{"devalüasyon", "1.2"}
        //        , {"dolarizasyon", "1.2"},{"efektif", "1.2"},{"ihale", "1.2"},{"likidite", "1.2"},{"oynaklık", "1.2"}
        //        , {"müdahale", "1.2"},{"geçişkenlik", "1.2"},{"TEFE", "1.2"},{"TÜFE", "1.2"},{"portföy", "1.2"},{"repo", "1.2"}
        //        , {"resesyon", "1.2"},{"büyüme", "1.2"}
        //        , {"trilyon", "1.2"},{ "günlük","1.2"},  {"artış","1.2"},  {"global","1.2"}, { "yerli", "1.2"}, {"konjonktürel","1.2"}, {  "vadeli","1.2"}
        //        , {"band","1.2"},{ "bandı","1.2"}, {"kırım","1.2"}, {"kırdı","1.2"},{"kırıldı","1.2"}, {"kontrat","1n2"} 
        //        , {"hedef","1.2"},  {"plan","1.2"},  {"hedge","1.2"},  {"yatırımcı","1.2"},  {"liberalizasyon","1.2"}, {"fiyatlama","1.2"}
        //        , {"pricing", "1.2"}, {"finansman","1.2"}, {"temettü","1.2"},{"trend","1.2"},  {"bekle ve gör","1.2"}
        //        , {"borç tavanı" , "1.2"}, {"kırılgan","1.2"},  {"toparlanma","1.2"}
        //        , {"stok","1.2"}

        //        // Altın ile ilgili olanlar
        //        , {"altın", "1.3"}, {"ons", "1.3"}, {"gram", "1.3"}, {"gr", "1.3"}, {"külçe", "1.3"}
        //        , {"ayar", "1.3"}, {"ziynet", "1.3"}, {"ayı", "1.3"}, {"boğa", "1.3"}, {"ihracat", "1.3"},{"ithalat", "1.3"}
        //        , {"satış", "1.3"},{"alış", "1.3"},{"fiyat", "1.3"},{"piyasa", "1.3"},{"maliyet", "1.3"}, {"TL", "1.3"}
        //        , {"ticaret", "1.3"},{"milyon", "1.3"}, {"milyar", "1.3"}, {"yatırım", "1.3"},{"sat", "1.3"},{"al", "1.3"}
        //        , {"lira", "1.3"}, {"portföy", "1.3"},{"varlık", "1.3"}
        //        , { "trilyon", "1.3"},{ "günlük","1.3"},  {"artış","1.3"},  {"global","1.3"}, { "yerli", "1.3"}, {"konjonktürel","1.3"}, {"vadeli","1.3"}
        //        , {"hedef","1.3"},  {"plan","1.3"},  {"hedge","1.3"},  {"yatırımcı","1.3"},  {"liberalizasyon","1.3"}, {"fiyatlama","1.3"}
        //        , {"pricing", "1.3"}, {"finansman","1.3"}, {"temettü","1.3"},{"trend","1.3"},  {"bekle ve gör","1.3"}
        //        , {"borç tavanı" , "1.3"}, {"kırılgan","1.3"},  {"toparlanma","1.3"}
        //        , { "stok","1.3"}
        //        // Petrol ile ilgililer
        //        , {"LPG", "1.4"}, {"doğalgaz", "1.4"}, {"doğalgaza", "1.4"}, {"yakıt", "1.4"}, {"benzin", "1.4"}, {"dizel", "1.4"}
        //        , {"motorin", "1.4"}, {"otogaz", "1.4"}, {"petrol", "1.4"}, {"varil", "1.4"}, {"brent", "1.4"}
        //        , {"ham petrol", "1.4"}, {"akaryakıt", "1.4"}, {"Petkim", "1.4"}, {"petrokimya", "1.4"}
        //        , {"ihracat", "1.4"}, {"ithalat", "1.4"},{"ticaret", "1.4"},{"milyon", "1.4"}, {"milyar", "1.4"}
        //        , {"fiyat", "1.4"},{"piyasa", "1.4"},{"maliyet", "1.4"},{"varlık", "1.4"}
        //        , { "trilyon", "1.4"},{ "günlük","1.4"},  {"artış","1.4"},  {"global","1.4"}, { "yerli", "1.4"}, {"konjonktürel","1.4"}, {  "vadeli","1.4"}
        //        , { "mazot", "1.4"}, {"enerji", "1.4"}, {"jeoplotik", "1.4"},{"geopolitics","1.4"}, {"bölgesel", "1.4"}
        //        , {"yenilenebilir", "1.4"},{"enerji arzı", "1.4"}, {"enerji sektörü","1.4"}
        //        , {"hedef","1.4"},  {"plan","1.4"},  {"hedge","1.4"},  {"yatırımcı","1.4"},  {"liberalizasyon","1.4"}, {"fiyatlama","1.4"}
        //        , {"pricing", "1.4"}, {"finansman","1.4"}, {"temettü","1.4"},{"trend","1.4"},  {"bekle ve gör","1.4"}
        //        , {"borç tavanı" , "1.4"}, {"kırılgan","1.4"},  {"toparlanma","1.4"}
        //        , {"stok","1.4"},
        //    };

        #endregion
        #region Benim yaptığım Dictionary
        //public static Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
        //    { 
        //        {"ekonomi", "1.0"}, {"ekonomik", "1.0"},  {"tüketici", "1.0"}, {"para", "1.0"}, {"faiz", "1.0"}, {"maliye", "1.0"}
        //        , {"sermaye", "1.0"}, {"borç", "1.0"}, {"ihracat", "1.0"}, {"iş", "1.0"}, {"cari", "1.0"}
        //        , {"finans", "1.0"}, {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"banka", "1.0"}, {"TCMB", "1.0"}
        //        , {"kriz", "1.0"}, {"iktisat", "1.0"}, {"hazine", "1.0"}, {"enflasyon", "1.0"}, {"iktisadi", "1.0"}
        //        , {"bütçe", "1.0"},  {"ticari", "1.0"}, {"ticaret", "1.0"}, {"girişim", "1.0"}, {"rakam", "1.0"}, {"TL", "1.0"}
        //        , {"lira", "1.0"}, {"kredi", "1.0"}, {"bin", "1.0"}, {"milyon", "1.0"}, {"milyar", "1.0"}, {"satış", "1.0"}
        //        , {"yatırım", "1.0"}, {"servet", "1.0"}, {"zengin", "1.0"}, {"sat", "1.0"}
        //        , {"vergi", "1.0"}, {"gayrimenkul", "1.0"}, {"risk", "1.0"}
        //        //Borsa ile ilgili olanlar
        //        , {"BIST1OO", "1.1"}, {"BIST", "1.1"},{"BIST30", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"}, 
        //        {"IMKB100", "1.1"}, {"endeks", "1.1"}, {"tahvil", "1.1"}, {"mikrofinans", "1.1"}, {"Fed", "1.1"}
        //        // Döviz ile ilgili olanlar
        //        , {"döviz", "1.2"}, {"dolar", "1.2"}, {"euro", "1.2"}, {"Sterlin", "1.2"}, {"Kronu", "1.2"}, {"Frangı", "1.2"}
        //        , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"} , {"kur", "1.2"}
        //        //, {"kur", "1.2"}
        //        // Altın ile ilgili olanlar
        //        , {"altın", "1.3"}, {"ayar", "1.3"}, {"ons", "1.3"}
        //        // Petrol ile ilgililer
        //        , {"LPG", "1.4"}, {"doğalgaz", "1.4"}, {"doğalgaza", "1.4"}, {"yakıt", "1.4"}, {"benzin", "1.4"}, {"dizel", "1.4"}
        //        , {"motorin", "1.4"}, {"otogaz", "1.4"}, {"petrol", "1.4"}, {"varil", "1.4"}, {"brent", "1.4"}
        //        , {"ham petrol", "1.4"}, {"akaryakıt", "1.4"}, {"Petkim", "1.4"}, {"petrokimya", "1.4"}, 
        //    };
        #endregion
        #region 7 mart 3.29 PM Dictionary
        //public static Dictionary<String, String> ekonomiListesi = new Dictionary<String, String>() 
        //    { 
        //        {"ekonomi", "1.0"}, {"ekonomik", "1.0"},  {"tüketici", "1.0"}, {"faiz", "1.0"}, {"maliye", "1.0"}
        //        , {"sermaye", "1.0"}, {"ihracat", "1.0"}
        //        , {"finans", "1.0"}, {"fiyat", "1.0"}, {"piyasa", "1.0"}, {"maliyet", "1.0"}
        //        , {"iktisadi", "1.0"}
        //        , {"ticaret", "1.0"}, {"girişim", "1.0"}, {"rakam", "1.0"} 
        //        , {"kredi", "1.0"}, {"bin", "1.0"}, {"milyon", "1.0"}, {"milyar", "1.0"}, {"satış", "1.0"}
        //        , {"yatırım", "1.0"}, {"sat", "1.0"}
        //        , {"vergi", "1.0"}, {"gayrimenkul", "1.0"}, {"risk", "1.0"}, {"TL", "1.2"}, {"lira", "1.2"}
        //        //Borsa ile ilgili olanlar
        //        , {"BIST100", "1.1"}, {"BIST", "1.1"},{"BIST30", "1.1"}, {"borsa", "1.1"}, {"IMKB", "1.1"} 
        //        , {"IMKB100", "1.1"}, {"endeks", "1.1"}, {"tahvil", "1.1"}, {"mikrofinans", "1.1"}, {"Fed", "1.1"}
        //        , {"para", "1.1"}, {"banka","1.1"}, {"kriz", "1.1"}, {"bono", "1.1"}, {"risk", "1.1"}, {"cari", "1.1"}
        //        , {"ayı", "1.1"}, {"boğa", "1.1"}, {"faiz", "1.1"}, {"sermaye", "1.1"}, {"finans", "1.1"}
        //        // Döviz ile ilgili olanlar
        //        , {"döviz", "1.2"}, {"dolar", "1.2"}, {"euro", "1.2"}, {"cari", "1.2"}, {"Sterlin", "1.2"}, {"Kronu", "1.2"}, {"Frangı", "1.2"}
        //        , {"USD", "1.2"}, {"EU", "1.2"}, {"GBP", "1.2"}, {"NOK", "1.2"}, {"CHF", "1.2"} , {"kur", "1.2"}
        //        , {"para", "1.2"}, {"kur", "1.2"}, {"TCMB", "1.2"}, {"hazine", "1.2"}, {"faiz", "1.2"}, {"borç", "1.2"}
        //        , {"cari", "1.2"} , {"enflasyon", "1.2"}, {"ticaret", "1.2"}
        //        // Altın ile ilgili olanlar
        //        , {"altın", "1.3"}, {"ayar", "1.3"}, {"ons", "1.3"}, {"gram", "1.3"}, {"gr", "1.3"}, {"külçe", "1.3"}
        //        , {"ayar", "1.3"}, {"ziynet", "1.3"}, {"ayı", "1.3"}, {"boğa", "1.3"}, {"ihracat", "1.3"}
        //        , {"ticaret", "1.3"}
        //        // Petrol ile ilgililer
        //        , {"LPG", "1.4"}, {"doğalgaz", "1.4"}, {"doğalgaza", "1.4"}, {"yakıt", "1.4"}, {"benzin", "1.4"}, {"dizel", "1.4"}
        //        , {"motorin", "1.4"}, {"otogaz", "1.4"}, {"petrol", "1.4"}, {"varil", "1.4"}, {"brent", "1.4"}
        //        , {"ham petrol", "1.4"}, {"akaryakıt", "1.4"}, {"Petkim", "1.4"}, {"petrokimya", "1.4"}
        //        , {"ihracat", "1.4"}, {"ticaret", "1.4"}
        //    };
        #endregion
        #region Konu Dictionary'lerinin hepsinin bulunduğu liste
        public static List<Dictionary<String,String>> dictionaryList = new List<Dictionary<string,string>>()
        {
            ekonomiListesi, borsaListesi, dovizListesi, altinListesi, petrolListesi
        };
        #endregion
        #endregion
        public ZemberekAnaliz()
        {
            zemberek = new Zemberek(new TurkiyeTurkcesi());
        }

        public String AnalizYap(String word)
        {
            if (ekonomiListesi.ContainsKey(word))
            {
                return ekonomiListesi[word];
            }
            try
            {
                Kelime[] kelimeler = zemberek.kelimeCozumle(word);
                foreach (var kelime in kelimeler)
                {
                    Kok kelimeKoku = kelime.kok();
                    String icerik = kelimeKoku.icerik();
                    if (ekonomiListesi.ContainsKey(icerik))
                    {
                        return ekonomiListesi[icerik];

                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
            return "";
        }

        public Dictionary<String, List<String>> AnalizYap(String word, bool OgeAnalizi) // Olumsuz eklere de ayırmak istiyorsak OgeAnalizi = true olarak girmemiz gerekiyor
        {
            Dictionary<String, List<String>> dondurulecek = new Dictionary<String, List<String>>() 
            {
                {"Konu ID", new List<String>()},{"Olumsuz fiil", new List<String>()}
            };
            foreach (var dictionary in dictionaryList)
            {
                if (dictionary.ContainsKey(word))
                {
                    dondurulecek["Konu ID"].Add(dictionary[word]);
                }
            }
            if (dondurulecek["Konu ID"].Count != 0) // Eğer dondürülecek bir eleman bulmuşsak hiç aşağıya girme ve return et
                return dondurulecek;
            try
            {
                Kelime[] kelimeler = zemberek.kelimeCozumle(word);
                foreach (var kelime in kelimeler)
                {
                    Kok kelimeKoku = kelime.kok();
                    String icerik = kelimeKoku.icerik();
                    #region Dictionary'lerde bu kelime var mı?
                    foreach (var dictionary in dictionaryList)
                    {

                        if (dictionary.ContainsKey(icerik))
                        {
                            dondurulecek["Konu ID"].Add(dictionary[icerik]);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                return dondurulecek;
            }
            return dondurulecek;
        }




        public Dictionary<String, List<String>> OgelereAyir(String kelime)
        {
            Dictionary<String, List<String>> dondurulecek = new Dictionary<String, List<String>>() 
            {
                {"Konu ID", new List<String>()},{"Olumsuz fiil", new List<String>()}
            }; 
            try
            {
                Kelime[] kelimeler = zemberek.kelimeCozumle(kelime);
                Kelime k = kelimeler.First();
                Kok kelimeKoku = k.kok();
                String icerik = kelimeKoku.icerik();
                String wordType = kelimeKoku.tip().ToString();
                #region Dictionary'lerde bu kelime var mı?
                foreach (var dictionary in dictionaryList)
                {
                    if (dictionary.ContainsKey(kelime))
                    {
                        dondurulecek["Konu ID"].Add(dictionary[kelime]);
                    }
                }
                #endregion

                #region Öğesi
                if (wordType == "FIIL")
                {
                    foreach (var ek in k.ekler())
                    {
                        if (ek.ad() == "FIIL_OLUMSUZLUK_ME" && kelimeler.Count() == 1)
                        {
                            dondurulecek["Olumsuz fiil"].Add(kelime);
                            break;
                        }
                    }
                }
                #endregion
                return dondurulecek;
            }
            catch (Exception)
            {
                return dondurulecek;
            }
        }
    }

}
