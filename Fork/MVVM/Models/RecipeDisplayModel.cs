using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeDisplayModel : RecipeDisplayViewModel
    {
        public static RecipeDisplayModel Instance => new RecipeDisplayModel();

        public Ingredient Ingredient { get; set; }  
        public RecipeDisplayModel() : base(ModelDataInjector.GetRecipe())
        {
            Ingredient = Recipe.Ingredients.First();
        }

    }
}
