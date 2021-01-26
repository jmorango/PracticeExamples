using System;

namespace StockPricesV2
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxProfit = 0;
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            // if the array is larger than 1635 most of the time the single loop method is aquicker.
            // For arrays smaller than 1635 the method with the 2 for loops is quicker most times. 
            int[] stockPrices = new int[5000];

            //Initializing the array with 1000 random values
            Random numbers = new Random();
            for(int i = 0; i < stockPrices.Length; i++)
            {
                stockPrices[i] = numbers.Next(10000);
            }
            
            //capturing time before and after function execution (with while loop)
            start = DateTime.Now; 
            maxProfit = newGetMaxProfit(stockPrices);
            end = DateTime.Now;
            

            //output first result
            Console.WriteLine("MaxProfit: " + maxProfit);
            Console.WriteLine("New version maxProfit result with single loop time: " + end.Subtract(start).TotalSeconds + " seconds");

            // capturing time before and after the 2 for loop function and output result. 
            start = DateTime.Now;
            maxProfit = GetMaxProfit(stockPrices);
            end = DateTime.Now;
            Console.WriteLine("Max Profit " + maxProfit);
            Console.WriteLine("Old version maxProfit result with double loop time: " + end.Subtract(start).TotalSeconds + " seconds");

        }
        public static int newGetMaxProfit(int[] stockPrices)
        {
            int buyIndex = 0; 
            int maxProfit = 0; 
            int indexGap = 1;
            int index = buyIndex + indexGap;
            
            while(index < stockPrices.Length)
            {
                if (stockPrices[buyIndex]> stockPrices[index])
                {
                    buyIndex = buyIndex + indexGap;
                    indexGap = 1;
                }
                else
                {
                    int diff = 0;
                    diff = stockPrices[index] - stockPrices[buyIndex];
                    indexGap++;

                    if (diff > maxProfit)
                    {
                        maxProfit = diff;
                    }

                }
                index = buyIndex + indexGap;
            }
            return maxProfit;
        }
        public static int GetMaxProfit(int[] values)
        {
            int tempProfit = 0;
            int maxProfit = 0;
            for (int i = 0; i < values.Length; i++ )
            {
                tempProfit = getProfit(i,values);
                if (tempProfit > maxProfit) maxProfit = tempProfit;
            }
            return maxProfit;
        }
        static int getProfit(int index, int[] vals)
        {
            int max = 0;
            for(int i = index; i < vals.Length; i++)
            {
                int temp = 0 ;
                temp = vals[i] - vals[index];
                if (temp > max) max = temp;
            }
            return max;
        }
    }
}
