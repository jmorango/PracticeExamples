using System;
/*
            First, I wanna know how much money I could have made yesterday if I'd been trading Apple stocks all day.

            So I grabbed Apple's stock prices from yesterday and put them in an array called stockPrices, where:

            The indices are the time (in minutes) past trade opening time, which was 9:30am local time.
            The values are the price (in US dollars) of one share of Apple stock at that time.
            So if the stock cost $500 at 10:30am, that means stockPrices[60] = 500.

            Write an efficient method that takes stockPrices and returns the best profit I could have made from one 
            purchase and one sale of one share of Apple stock yesterday.

            For example:

            int[] stockPrices = { 10, 7, 5, 8, 11, 9 };

            // Returns 6 (buying for $5 and selling for $11)
            GetMaxProfit(stockPrices);

            C#
            No "shorting"—you need to buy before you can sell. Also, you can't buy and sell in the same time step—at
            least 1 minute has to pass.
*/
namespace StockPrices
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] stockPrices = {10, 7, 5, 8, 11, 9};
            Console.WriteLine(GetMaxProfit(stockPrices));
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
