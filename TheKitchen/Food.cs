using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    class Food : Item
    {
        // public NutritionData NutritionData;
        // public CostData CostData;
        // public InventoryItemData;

        public Food(string name, double quantity, string unit) : base(name)
        {
            Quantity.Add(quantity);
            Unit.Add(unit);
        }
    }
}
