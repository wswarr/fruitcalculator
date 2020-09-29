﻿using System;
using System.Collections.Generic;
using System.Text;

namespace fruitcalc.store
{
    public class FruitCollection : FruitData
    {
        public string Name
        {
            get
            {
                return infoName;
            }
        }
        public FruitCollection(string infoName) : base(infoName)
        {
        }
      
    }
}
