using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ForkDataHandling;
using TheKitchen;
using static Fork.DI;

namespace Fork
{
    /// <summary>
    /// main view model for the application
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Members

        #endregion

        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Recipes;

        public BaseViewModel CurrentPageViewModel { get; set; }
        public ObservableCollection<RecipeViewModel> AllRecipeViewModels { get; set; }


        #endregion

        #region Commands

        public ICommand OpenRecipesCommand { get; set; }
        public ICommand OpenMealPrepCommand { get; set; }
        public ICommand OpenGroceryListCommand { get; set; }
        public ICommand OpenIngredientsCommand { get; set; }
        public ICommand OpenTechniquesCommand { get; set; }

        #endregion

        #region Constructor

        public ApplicationViewModel()
        {
            // value initialization
            CurrentPage = ApplicationPage.Recipes;
            AllRecipeViewModels = new ObservableCollection<RecipeViewModel>();
            foreach (var recipe in ForkGlobalData.AllRecipes)
            {
                AllRecipeViewModels.Add(new RecipeViewModel(recipe));
            }

            // command assignment
            OpenRecipesCommand = new RelayCommand(OpenRecipes);
            OpenMealPrepCommand = new RelayCommand(OpenMealPrep);
            OpenGroceryListCommand = new RelayCommand(OpenGroceryList);
            OpenIngredientsCommand = new RelayCommand(OpenIngredients);
            OpenTechniquesCommand = new RelayCommand(OpenTechniques);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Takes the user to the Recipes Page
        /// </summary>
        private void OpenRecipes()
        {
            ViewModelApplication.GoToPage(ApplicationPage.Recipes, new RecipesPageViewModel());
        }

        /// <summary>
        /// Takes the user to the MealPrep Page
        /// </summary>
        private void OpenMealPrep()
        {
            ViewModelApplication.GoToPage(ApplicationPage.MealPrep, new MealPrepPageViewModel());
        }

        /// <summary>
        /// Takes the user to the GroceryList Page
        /// </summary>
        private void OpenGroceryList()
        {
            ViewModelApplication.GoToPage(ApplicationPage.GroceryList, new GroceryListPageViewModel());
        }

        /// <summary>
        /// Takes the user to the Ingredients Page
        /// </summary>
        private void OpenIngredients()
        {
            ViewModelApplication.GoToPage(ApplicationPage.Ingredients, new IngredientsPageViewModel());
        }

        /// <summary>
        /// Takes the user to the Techniques Page
        /// </summary>
        private void OpenTechniques()
        {
            ViewModelApplication.GoToPage(ApplicationPage.Techniques, new TechniquesPageViewModel());
        }

        /// <summary>
        /// Switch that Executes the relevent local closing methods based upon the current page displayed
        /// </summary>
        /// <param name="vm"></param>
        public void OnClosing(object obj)
        {
            switch (CurrentPage)
            {
                case ApplicationPage.Recipes:
                    ((RecipesPageViewModel)obj).OnClosing();
                    break;
            }
        }

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

        }

        #endregion
    }
}
