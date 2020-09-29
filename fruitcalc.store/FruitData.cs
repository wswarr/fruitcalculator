using System;
using System.Collections.Generic;

namespace fruitcalc.store
{
    public abstract class FruitData : Interfaces.FruitInterface
    {
        Dictionary<string, decimal> internalCollection = new Dictionary<string, decimal>();
        internal string infoName = string.Empty;
        public FruitData(string infoName)
        {
            this.infoName = infoName.ToLower();
        }

        public string GetName()
        {
            return this.infoName;
        }

        public Dictionary<string, decimal> InternalCollection()
        {
            return internalCollection;
        }

        public void ParseSection(string section)
        {
            string[] itemList = section.Split(',');

            foreach (string itemDetail in itemList)
            {
                string[] details = null;
                if (itemDetail.Contains("–"))
                {
                    details = itemDetail.Split('–');
                }
                else
                {
                    details = itemDetail.Trim().Split(' ');
                }

                decimal convertedValue = 0;
                decimal.TryParse(details[1].Replace("$", string.Empty), out convertedValue);
                internalCollection.Add(details[0].Trim().ToLower(), convertedValue);

            }
        }
    }
}
