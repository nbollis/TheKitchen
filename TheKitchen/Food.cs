using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    class Food : Item
    {
        public Food(string name, double quantity, string unit) : base(name)
        {
            Quantity.Add(quantity);
            Unit.Add(unit);
        }
    }
}
