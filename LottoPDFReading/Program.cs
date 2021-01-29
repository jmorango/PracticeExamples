using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

//The program is not reading the same amount of rows per column needs some adjustment. 

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
            const int COLDISPLACEMENT = 20;
            const int MOVESECONDCOL   = 290;
            int column = 56;
             
            // Adjust the numbers on the rectangle lx + 20 seems to work for the first column on the pdf
            // first column rect values = 56,36,10,680 
            // second column values (76,36,10,680)
            // third column values (96,36,10,680)
            //fourth column values (116,36,10,680)
            // fifth column values (136,36,10,680)
            //sixth column values (156,36,10,680)

            // second column on the pdf starts at (346,36,10,680)
            //(366,36,10,680)
            //(386,36,10,680)
            //(406,36,10,680)
            //(426,36,10,680)
            //(446,36,10,680)
                        
            StringBuilder[] text = new StringBuilder[6];
            using (PdfReader reader = new PdfReader("l6.pdf"))
            {
                for (int i =1 ; i <= reader.NumberOfPages; i++)
                {
                    for(int columns = 0; columns < 6; columns++)
                    {
                        if (i==1) text[columns] = new StringBuilder("");
                        var rect = new System.util.RectangleJ(column + columns*COLDISPLACEMENT,36,10,680);
                        RenderFilter filter = new RegionTextRenderFilter(rect);
                        text[columns].Append(PdfTextExtractor.GetTextFromPage(reader,i, new FilteredTextRenderListener(new LocationTextExtractionStrategy(),filter)));
                        rect = new System.util.RectangleJ(column + MOVESECONDCOL + columns*COLDISPLACEMENT,36,10,680);
                        filter = new RegionTextRenderFilter(rect);
                        text[columns].Append(PdfTextExtractor.GetTextFromPage(reader,i, new FilteredTextRenderListener(new LocationTextExtractionStrategy(),filter)));
                        
                    }
                    
                }
            }
            
            
            
             int index = 0;
            //cleaning any random outputs that are not numbers and adding a space between them to split later.
            string[] cleanText = new string[6];
            for(int i = 0; i<6; i++)
            {
                string output = "";
                Console.WriteLine(text[i].Length);
                while (index < text[i].Length)
                {
                    char ch = text[i][index];
                                    
                    if (Char.IsDigit(ch) ) 
                    {
                        output += ch;
                        
                    }
                    else 
                    {
                        output += " ";
                    }
                            
                    index++;
                }
                index = 0;
                cleanText[i]=output;
                Console.WriteLine(output.Length);
               
            } 
            using (System.IO.StreamWriter fileOut = new System.IO.StreamWriter(@"C:\Users\jmoran\source\repos\LottoPdfReading\output.txt"))
            {
                // fileOut.WriteLine(output);
                int j= 0;
                int[,] columns = new int[6,cleanText[0].Length];             
                for (int i =0; i < 6; i++)
                foreach(string number in cleanText[i].ToString().Split(" "))
                {
                    if (Int32.TryParse(number,out int v))
                    {
                        if (v>0)
                        {
                            //Console.WriteLine(v);
                            //columns[i,j] = v;
                        }

                    }
                    j++; 
                }
                for(int i = 0 ; i< cleanText[0].Length; i++)
                 fileOut.WriteLine($@"{columns[0,i]} {columns[1,i]} {columns[2,i]} {columns[3,i]} {columns[4,i]} {columns[5,i]}");

                
                fileOut.Close();
            }
            
        }
    }

   
}
