using System;
using System.Collections.Generic;
using System.Text;
using fruitcalc.store.Interfaces;

namespace fruitcalc.store
{
    public class StoreFront
    {
        public void ParseArgs(string argLine)
        {
            string wholeInput = argLine;
            string[] sections = wholeInput.Split(';');

            if (sections.Length < 3)
            {
                Console.WriteLine("Invalid parameter list.  Please provide something like: Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1");
                return;
            }

            Console.WriteLine(string.Concat("Calculating input: ", wholeInput));

            FruitInterface pricing = new FruitCollection("pricing");
            FruitInterface promotions = new FruitCollection("promotions");
            FruitInterface basket = new FruitCollection("basket");

            foreach (string sectionPart in sections)
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
                    Console.WriteLine(string.Concat("Parsing: ", ((FruitCollection)promotions).Name));
                    promotions.ParseSection(section.Remove(0, dataIndex));
                }
                else
                if (section.StartsWith("basket", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine(string.Concat("Parsing: ", ((FruitCollection)basket).Name));
                    basket.ParseSection(section.Remove(0, dataIndex));
                }
                else
                {
                    //check for "input"?  If so, add handling of : here also
                    Console.WriteLine(string.Concat("Parsing: ", ((FruitCollection)pricing).Name));
                    pricing.ParseSection(section);
                }
            }

            decimal total = 
                runCalculations(pricing, promotions, basket);

            Console.WriteLine(string.Concat("Total cost for fruit: $ ", total.ToString("F")));
        }



        private decimal runCalculations
            (
                FruitInterface pricing,
                FruitInterface promotions,
                FruitInterface basket
            )
        {
            decimal total = 0;

            foreach (var fruit in basket.InternalCollection())
            {
                total += (pricing.InternalCollection()[fruit.Key] * fruit.Value) *
                     (promotions.InternalCollection().ContainsKey(fruit.Key) ? (promotions.InternalCollection()[fruit.Key]) : 1);
            }

            return total;
        }


    }
}
