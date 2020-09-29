using System;
using System.Collections.Generic;

/*
 * 

Question:  Is "Input :" part of the actual input?

Input : Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1

Output: Total price= 55

Example 2:

Input : Oranges – $10; Apples- $5 ; Promotions: Oranges – 0.5;  Basket: Oranges - 5, Apples 1

Output: Total price= 30
*/

namespace fruitcalc
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Assuming "Input :" is not intended to be part of the input string
            if(args == null || args.Length == 0){
                args = new[] { "Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1" };
            }

            foreach(string arg in args)
            {
                parseArgs(arg);
            }

            Console.WriteLine("Press any key to close...");
            Console.Read();

        }

        static void parseArgs(string argLine)
        {
            string wholeInput = argLine;
            string[] sections = wholeInput.Split(';');
            //Take input, parse values into some list
            if(sections.Length < 3)
            {
                Console.WriteLine("Invalid parameter list.  Please provide something like: Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1");
                return;
            }

            Console.WriteLine(string.Concat("Calculating input: ", wholeInput));

            Dictionary<string, decimal> pricing = new Dictionary<string, decimal>();
            Dictionary<string, decimal> promotions = new Dictionary<string, decimal>();
            Dictionary<string, decimal> basket = new Dictionary<string, decimal>();

            foreach(string sectionPart in sections)
            {
                string section = sectionPart.Replace(Convert.ToChar(45), '–');
                section = section.Trim();
                int dataIndex = section.IndexOf(":") + 1;
                if (section.Remove(0, dataIndex).Trim().StartsWith("no", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                if (section.StartsWith("promotions", StringComparison.CurrentCultureIgnoreCase))
                {
                    parseSection(promotions, section.Remove(0, dataIndex));
                }
                else
                if(section.StartsWith("basket", StringComparison.CurrentCultureIgnoreCase))
                {
                    parseSection(basket, section.Remove(0, dataIndex));
                }
                else
                {
                    //check for "input"?  If so, add handling of : here also
                    parseSection(pricing, section);
                }
            }

            decimal total = runCalculations(pricing, promotions, basket);

            Console.WriteLine(string.Concat("Total cost for fruit: $ ", total.ToString("F")));
        }

        static decimal runCalculations
            (
                Dictionary<string, decimal> pricing,
                Dictionary<string, decimal> promotions,
                Dictionary<string, decimal> basket
            )
        {
            decimal total = 0;

            foreach(var fruit in basket)
            {
                total += (pricing[fruit.Key] * fruit.Value) *
                     (promotions.ContainsKey(fruit.Key) ? (promotions[fruit.Key]) : 1);
            }

            return total;
        }

        static void parseSection(Dictionary<string, decimal> sectionList, string section)
        {
            string[] itemList = section.Split(",");
            
            foreach(string itemDetail in itemList)
            {
                string[] details = null;
                if (itemDetail.Contains('–'))
                {
                    details = itemDetail.Split('–');
                }
                else
                {
                    details = itemDetail.Trim().Split(' ');
                }

                decimal convertedValue = 0;
                decimal.TryParse(details[1].Replace("$", string.Empty), out convertedValue);
                sectionList.Add(details[0].Trim().ToLower(), convertedValue);

            }
        }
    }
}
