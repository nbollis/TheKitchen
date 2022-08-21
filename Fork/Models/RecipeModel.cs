using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeModel : RecipeViewModel
    {
        public static RecipeModel Instance => new RecipeModel();

        public Ingredient Ingredient { get; set; }  
        public RecipeModel() : base(ModelDataInjector.GetRecipe())
        {
            Ingredient = Recipe.Ingredients.First();
            Categories.Add(new CategoryViewModel(ForkGlobalData.AllCategories.First()));
        }

    }
}
