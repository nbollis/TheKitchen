using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class MealPrep
    {
        public List<Recipe> Recipes { get; set; }
        public TimeSpan TimeToCook { get; set; }
        public string Notes { get; set; }

        public MealPrep()
        {
            Recipes = new List<Recipe>();
            TimeToCook = new();
        }

        public void AddRecipeToMealPrep(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void RemoveRecipeFromMealPrep(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        public ShoppingList GenerateShoppingList()
        {
            return new ShoppingList(Recipes);
        }
    }
}
