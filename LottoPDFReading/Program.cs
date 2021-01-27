using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace LottoPDFReading
{
    class Program
    {
        static void Main(string[] args)
        {

           getTextFromPDF();
        }


        static private void getTextFromPDF()
        {
            
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader("l6.pdf"))
            {
                for (int i =1 ; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader,i));
                    
                }
            }
            
            int index = 0;
            //todo parse string , eliminate empty spaces, correct eols 
            while (index < text.Length - 1)
            {
                char ch = text[index];
                char nextChar = text[index + 1];

                if (ch =='\n' || (ch == ' ' && nextChar==' ')||ch == '\t') 
                {
                    index++;
                    continue;
                }
                if (Char.IsDigit(ch) || (ch ==' ' && Char.IsDigit(nextChar))  || ch =='/' ) 
                {
                   Console.Write(ch);
                   
                }
                index++;
            }
            
        }
    }

   
}
