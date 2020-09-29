using System;
using System.Collections.Generic;
using System.Text;

namespace fruitcalc.store.Interfaces
{
    public interface FruitDataInterface
    {
        string GetName();
        Dictionary<string, decimal> InternalCollection();
        void ParseSection(string section);
    }
}
