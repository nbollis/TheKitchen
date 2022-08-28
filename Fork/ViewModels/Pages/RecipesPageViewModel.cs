using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ForkDataHandling;
using TheKitchen;
using static Fork.DI;

namespace Fork
{
    public class RecipesPageViewModel : BaseViewModel, IPage, IListContainer
    {
    #region Private Properties

        private ObservableCollection<RecipeViewModel> recipes;
        private RecipeListViewModel recipeListViewModel;
        private RecipeViewModel recipeViewModel;

    #endregion

    #region Public Properties

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get { return recipes; }
            set
            {
                recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public RecipeListViewModel RecipeListViewModel
        {
            get { return recipeListViewModel; }
            set
            {
                recipeListViewModel = value;
                OnPropertyChanged(nameof(RecipeListViewModel));
            }
        }

        public RecipeViewModel RecipeViewModel
        {
            get { return recipeViewModel; }
            set
            {
                recipeViewModel = value;
                OnPropertyChanged(nameof(RecipeViewModel));
            }
        }
        public ApplicationPage ApplicationPage => ApplicationPage.Recipes;

        public static int BufferThickness { get; set; } = 20;

    #endregion

    #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand ForwardCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangeViewCommand { get; set; }
        public ICommand AddRecipeCommand { get; set; }

        #endregion

    #region Constructor

        public static RecipesPageViewModel Instance => new RecipesPageViewModel();

        public RecipesPageViewModel()
        {
            // Initialize Fields
            recipes = ForkGlobalData.AllRecipes.ToViewModels();
            recipeListViewModel = new RecipeListViewModel(recipes);
            recipeViewModel = recipes.First();

            // Declare Commands
            BackCommand = new RelayCommand(() => GoBack());
            ForwardCommand = new RelayCommand(() => GoForward());
            SearchCommand = new RelayCommand(() => Search());
            ChangeViewCommand = new RelayCommand(() => ChangeView());
            AddRecipeCommand = new RelayCommand(() => AddRecipe());
            RecipeViewModel.ItemSelected += ListItemSelected;
        }

    #endregion

    #region Command Methods
        private void GoBack()
        {
            int breakpoint = 0;
        }

        private void GoForward()
        {

        }

        private void Search()
        {

        }

        private void ChangeView()
        {

        }

        private void AddRecipe()
        {
            AddRecipeViewModel addRecipeViewModel= new();
            AddRecipeWindowView addRecipeWindowView = new(addRecipeViewModel);
            WindowViewModel windowViewModel = new(addRecipeWindowView); 
            addRecipeWindowView.DataContext = windowViewModel;

            RecipeViewModel.ItemSelected -= ListItemSelected;
            RecipeViewModel.ItemSelected += addRecipeViewModel.ListItemSelected;
            addRecipeWindowView.ShowDialog();

            if (addRecipeViewModel.RecipesToAdd.Any())
            {
                foreach (RecipeViewModel recipe in addRecipeViewModel.RecipesToAdd)
                {
                    recipe.HasChanged = true;
                    Recipes.Add(recipe);
                    RecipeListViewModel.RecipeList.Add(recipe);
                }
            }

            RecipeViewModel.ItemSelected -= addRecipeViewModel.ListItemSelected;
            RecipeViewModel.ItemSelected += ListItemSelected;
        }

        /// <summary>
        /// Event when a Recipe is Selected
        /// </summary>
        /// <param name="obj"></param>
        public void ListItemSelected(object obj, ListItemSelectedEventArgs e)
        {
            RecipeViewModel recipe = Recipes.First(p => p.Name.Equals(e.Name));
            // make changes on the left side of the screen
            if (RecipeListViewModel.SelectedItem != null)
            {
                RecipeListViewModel.SelectedItem.IsSelected = false;
            }
            RecipeListViewModel.SelectedItem = RecipeListViewModel.RecipeList.First(p => p.Name.Equals(e.Name));
            RecipeListViewModel.SelectedItem.IsSelected = true;

            if (RecipeViewModel != null && RecipeViewModel.HasChanged)
                RecipeViewModel.SaveRecipeVMChanges();

            // make changes on the right side of the screen
            RecipeViewModel = recipe;
        }

        public void ListItemSelected(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Event that occurs when the page closes
        /// </summary>
        public void OnClosing()
        {
            if (RecipeViewModel != null && RecipeViewModel.HasChanged)
                RecipeViewModel.SaveRecipeVMChanges();

            foreach (var recipe in Recipes.Where(p => p.HasChanged))
            {
                Recipe.SaveRecipe(recipe.Recipe);
            }
        }

        
    }
}
