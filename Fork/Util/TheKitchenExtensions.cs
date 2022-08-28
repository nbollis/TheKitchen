using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public static class TheKitchenExtensions
    {
        public static RecipeViewModel ToViewModel(this Recipe recipe)
        {
            return new RecipeViewModel(recipe);
        }

        public static ObservableCollection<RecipeViewModel> ToViewModels(this List<Recipe> recipes)
        {
            ObservableCollection<RecipeViewModel> recipeViewModels = new();
            foreach (var recipe in recipes)
            {
                recipeViewModels.Add(ToViewModel(recipe));
            }
            return recipeViewModels;
        }

        public static MealPrepViewModel ToViewModel(this MealPrep mealPrep)
        {
            return new MealPrepViewModel(mealPrep);
        }

        public static ObservableCollection<MealPrepViewModel> ToViewModels(this List<MealPrep> mealPreps)
        {
            ObservableCollection<MealPrepViewModel> mealPrepViewModels = new();
            foreach (var mealPrep in mealPreps)
            {
                mealPrepViewModels.Add(ToViewModel(mealPrep));
            }
            return mealPrepViewModels;
        }
    }
}
