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

            FruitDataInterface pricing = FruitDataFactory.GetFruitDataInstance("pricing");
            FruitDataInterface promotions = FruitDataFactory.GetFruitDataInstance("promotions");
            FruitDataInterface basket = FruitDataFactory.GetFruitDataInstance("basket");

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
                    promotions.ParseSection(section.Remove(0, dataIndex));
                }
                else
                if (section.StartsWith("basket", StringComparison.CurrentCultureIgnoreCase))
                {
                    basket.ParseSection(section.Remove(0, dataIndex));
                }
                else
                {
                    //check for "input"?  If so, add handling of : here also
                    pricing.ParseSection(section);
                }
            }

            decimal total = 
                runCalculations(pricing, promotions, basket);

            Console.WriteLine(string.Concat("Total cost for fruit: $ ", total.ToString("F")));
        }



        private decimal runCalculations
            (
                FruitDataInterface pricing,
                FruitDataInterface promotions,
                FruitDataInterface basket
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
