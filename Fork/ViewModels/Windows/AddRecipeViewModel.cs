using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    public class AddRecipeViewModel : BaseViewModel, IFileDragDropTarget, IListContainer
    {
        #region Private Properties

        private ObservableCollection<RecipeViewModel> recipes;
        private RecipeListViewModel recipeListViewModel;
        private RecipeViewModel recipeViewModel;
             
        #endregion

        #region Public Properties

        public int BufferThickness { get; set; } = 20;

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get { return recipes; }
            set { recipes = value; OnPropertyChanged(nameof(Recipes)); }
        }

        public RecipeListViewModel RecipeListViewModel
        {
            get { return recipeListViewModel; }
            set { recipeListViewModel = value; OnPropertyChanged(nameof(RecipeListViewModel)); }
        }

        public RecipeViewModel RecipeViewModel
        {
            get {  return recipeViewModel; }
            set { recipeViewModel = value; OnPropertyChanged(nameof(RecipeViewModel)); }
        }

        /// <summary>
        /// List of recipes to add to the main list on exiting
        /// </summary>
        public List<RecipeViewModel> RecipesToAdd { get; set; }


        #endregion

        #region Commands

        public ICommand AddFilesCommand { get; set; }
        public ICommand AddRecipeCommand { get; set; }
        public ICommand RemoveRecipeCommand { get; set; }
        public ICommand AddAllRecipesCommand { get; set; }

        #endregion

        #region Constructor

        public AddRecipeViewModel()
        {
            RecipeListViewModel = new RecipeListViewModel();
            Recipes = new();
            RecipesToAdd = new();

            AddFilesCommand = new RelayCommand(() => AddFiles());
            AddRecipeCommand = new RelayCommand(() => AddRecipe());
            RemoveRecipeCommand = new RelayCommand(() => RemoveRecipe());
            AddAllRecipesCommand = new RelayCommand(() => AddAllRecipes());
        }

        #endregion

        #region Private Helpers

        private void AddFiles(string[] filepaths = null)
        {
            if (filepaths == null)
            {
                string filterString = string.Join(";", ForkGlobalData.AcceptedRecipeUploadFormats.Select(p => "*" + p));
                Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Food Pictures(" + filterString + ")|" + filterString,
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    Multiselect = true
                };
                if (openFileDialog1.ShowDialog() == true)
                {
                    filepaths = openFileDialog1.FileNames;
                }
            }

            if (filepaths == null)
                return;
            else
                OnFileDrop(filepaths);
        }

        private void AddRecipe()
        {
            RecipeViewModel recipe = Recipes.First(p => p.Name.Equals(RecipeViewModel.Name));
            RecipesToAdd.Add(recipe);
        }

        private void RemoveRecipe()
        {
            RecipeViewModel recipe = Recipes.First(p => p.Name.Equals(RecipeViewModel.Name));
            RecipeListViewModel.RecipeList.Remove( RecipeListViewModel.RecipeList.First(p => p.Name.Equals(RecipeViewModel.Name)));
            Recipes.Remove(recipe);
            RecipeViewModel = null;
            OnPropertyChanged(nameof(RecipeViewModel));
            OnPropertyChanged(nameof(Recipes));
        }

        private void AddAllRecipes()
        {
            RecipesToAdd.AddRange(Recipes.ToList());
        }

        #endregion

        public void OnFileDrop(string[] filepaths)
        {
            List<string> errors = new();
            foreach (var path in filepaths)
            {
                RecipeParser.TryParseRecipe(path, out Recipe recipe, out errors);
                if (recipe != null)
                {
                    RecipeViewModel newRecipe = new(recipe);
                    RecipeListViewModel.RecipeList.Add(new RecipeViewModel(recipe));
                    Recipes.Add(newRecipe);
                }
            }

            if (errors.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in errors) sb.AppendLine(error);
                MessageBox.Show(sb.ToString());
            }
            OnPropertyChanged(nameof(RecipeListViewModel));
        }

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
    }
}
