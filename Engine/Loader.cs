using ForkIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Engine
{
    public static class Loader
    {
        public static List<Recipe> LoadAllRecipes()
        {
            string folderPath = Path.Combine(Recipe.GetRecipeFolderPath());
            return RecipeParser.ParseRecipes(Directory.GetFiles(folderPath));
        }
    }
}
