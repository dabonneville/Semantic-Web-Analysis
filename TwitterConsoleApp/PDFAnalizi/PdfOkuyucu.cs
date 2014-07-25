using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConsoleApp.PDFAnalizi
{
    class PdfOkuyucu
    {
        
        public static void ReadPDF(String FileName, String AuthorName, String Date)
        {
            Dictionary<int, String> pageContents = new Dictionary<int, string>();
            PdfReader pdfReader = new PdfReader(FileName);
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page,strategy);

                currentText = Encoding.UTF8.GetString(
                    UTF8Encoding.Convert(
                    Encoding.UTF8, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                pageContents.Add(page, currentText);
            }
            pdfReader.Close();
            ExcelFileForPDF.PrepareExcelFile(pageContents, FileName, AuthorName, Date);
        }
    }
}
