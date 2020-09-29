using System;
using System.Collections.Generic;
using System.Text;

namespace fruitcalc.store.Interfaces
{
    public interface FruitInterface
    {
        string GetName();
        Dictionary<string, decimal> InternalCollection();
        void ParseSection(string section);
    }
}
