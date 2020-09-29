using System;
using System.Collections.Generic;
using System.Text;

namespace fruitcalc.store
{
    public static class FruitDataFactory
    {
        public static Interfaces.FruitDataInterface GetFruitDataInstance(string infoName)
        {
            /*
             * Today each fruit data object is doing the same thing.  
             * If we need a new class that adheres to FruitInterface we can implement it and return it here
             */
            switch (infoName.ToLower())
            {
                case "pricing":
                    return new FruitData("pricing");
                case "promotions":
                    return new FruitData("promotions");
                case "basket":
                    return new FruitData("basket");
                default:
                    throw new Exception(string.Concat("Fruit information type not available: ", infoName));

            }
        }
    }
}
