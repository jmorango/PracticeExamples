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
            
            // TODO find out how to used the rectangle filter , ITextExtractionStrategy . 
            // Adjust the numbers on the rectangle, a little more on the upper x,y 
            var rect = new System.util.RectangleJ(18,36,125,620);
            RenderFilter filter = new RegionTextRenderFilter(rect);
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader("l6.pdf"))
            {
                for (int i =1 ; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader,i, new FilteredTextRenderListener(new LocationTextExtractionStrategy(),filter)));
                    
                }
            }
            
            
            string output = text.ToString();
             int index = 0;
            
            while (index < text.Length - 1)
            {
                char ch = text[index];
                char nextChar = text[index + 1];

                
                if(ch=='\n' && Char.IsDigit(nextChar)||ch == 'X')
                {
                    output += ' ';
                    index++;
                    continue;
                }
                if (ch =='\n' || (ch == ' ' && nextChar==' ')||ch == '\t') 
                {
                    index++;
                    continue;
                }
                if (Char.IsDigit(ch) || (ch ==' ' && Char.IsDigit(nextChar))  || ch =='/' ) 
                {
                   output += ch;
                   
                }
                index++;
            } 
            using (System.IO.StreamWriter fileOut = new System.IO.StreamWriter(@"C:\Users\jmoran\source\repos\LottoPdfReading\output.txt"))
            {
                
                fileOut.WriteLine(output);
                foreach(string number in output.Split(' '))
                {
                    fileOut.Write(number);
                }
                
                fileOut.Close();
            }
            
        }
    }

   
}
