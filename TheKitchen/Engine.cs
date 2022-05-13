using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class Engine
    {
        public List<Item> Items;
        public List<Recipe> Recipes;

        public void SaveAll()
        {
            Recipe.SaveRecipe(Recipes);
        }

        public void LoadAll()
        {
            Recipes = Recipe.LoadRecipes();
        }
    }
}
