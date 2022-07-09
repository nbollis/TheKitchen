using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class IngredientModel : BaseViewModel
    {
        public static IngredientModel Instance => new IngredientModel();
        public string Name { get; set; }
        public string UnitString { get; set; }
        public double Amount { get; set; }
        public string How { get; set; }

        public IngredientModel() : base()
        {
            Name = "Onion";
            UnitString = "Cups";
            Amount = 2.5;
            How = ", sliced 1/4 cm in thickness";
        }


    }
}
