using ForkIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    public class RecipePageViewModel : BaseViewModel, IPage, IListContainer
    {
    #region Private Properties

        private ObservableCollection<RecipeViewModel> _Recipes;
        private RecipeListViewModel _RecipeListViewModel;
        private RecipeViewModel _RecipeViewModel;

    #endregion

    #region Public Properties

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get { return _Recipes; }
            set
            {
                _Recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public RecipeListViewModel RecipeListViewModel
        {
            get { return _RecipeListViewModel; }
            set
            {
                _RecipeListViewModel = value;
                OnPropertyChanged(nameof(RecipeListViewModel));
            }
        }

        public RecipeViewModel RecipeViewModel
        {
            get { return _RecipeViewModel; }
            set
            {
                _RecipeViewModel = value;
                OnPropertyChanged(nameof(RecipeViewModel));
            }
        }

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

        public static RecipePageViewModel Instance => new RecipePageViewModel();
        public RecipePageViewModel()
        {
            // Initialize Fields
            var recipes = new ObservableCollection<Recipe>(RecipeParser.ParseRecipes(Recipe.GetRecipeFolderPath(), out List<string> errors));
            _Recipes = new();
            foreach (var recipe in recipes)
            {
                _Recipes.Add(new RecipeViewModel(recipe));
            }
            
            _RecipeListViewModel = new RecipeListViewModel(_Recipes);


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
