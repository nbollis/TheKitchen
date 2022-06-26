using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    /// <summary>
    /// A class used to create dummy data for models
    /// </summary>
    public static class ModelDataInjector
    {
        


        public static Recipe GetRecipe()
        {
            string filepath = Path.Combine(@"C:\Users\nboll\source\repos\TheKitchen\TheKitchen\DataFiles\Recipes\Chicken Fajita Pasta.xml");
            Recipe recipe = XmlReaderWriter.ReadFromXmlFile<Recipe>(filepath);
            return recipe;
        }

        public static Ingredient GetIngredient()
        {
            Recipe recipe = GetRecipe();
            Ingredient ingredient = recipe.Ingredients.First();
            return ingredient;
        }
    }
}
