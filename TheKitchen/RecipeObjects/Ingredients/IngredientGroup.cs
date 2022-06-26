using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class IngredientGroup
    {
        public string GroupName { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public IngredientGroup(string name, List<Ingredient> ingredients)
        {
            GroupName = name;
            Ingredients = ingredients;
        }
    }
}
