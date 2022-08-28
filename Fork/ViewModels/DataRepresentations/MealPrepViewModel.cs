using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class MealPrepViewModel : BaseViewModel
    {
        #region Private Members

        private MealPrep mealPrep;
        private TimeSpan? timeToCook;
        private ObservableCollection<RecipeViewModel> recipes;

        #endregion

        #region Public Properties

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get { return mealPrep.Recipes.ToViewModels(); }
            set { mealPrep.Recipes = value.Select(p => p.Recipe).ToList(); OnPropertyChanged(nameof(Recipes)); }
        }

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public MealPrepViewModel(MealPrep mealPrepToDisplay)
        {
            mealPrep = mealPrepToDisplay;
            recipes = new ObservableCollection<RecipeViewModel>(mealPrep.Recipes.ToViewModels());
            
        }

        #endregion

        #region Helpers

        #endregion
    }
}
