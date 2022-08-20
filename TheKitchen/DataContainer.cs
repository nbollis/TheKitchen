using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkIO;

namespace TheKitchen
{
    public static class DataContainer
    {
        public static List<Recipe> AllRecipes { get; set; }

        public static Dictionary<string, Recipe> AllRecipesDict { get; set; }

        static DataContainer()
        {
            LoadAllRecipes();
        }

        private static void LoadAllRecipes()
        {
            AllRecipes = RecipeParser.ParseRecipes(Recipe.GetRecipeFolderPath(), out List<string> errors);
            AllRecipesDict = AllRecipes.ToDictionary(p => p.Name, p => p);
        }
    }
}
