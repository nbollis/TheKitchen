using ForkIO;
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

        private ObservableCollection<Recipe> recipes;
        private RecipeListViewModel recipeListViewModel;
        private RecipeViewModel recipeViewModel;
             
        #endregion

        #region Public Properties

        public int BufferThickness { get; set; } = 20;

        public ObservableCollection<Recipe> Recipes
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


        #endregion

        #region Commands

        public ICommand AddFilesCommand { get; set; }
        public ICommand AddRecipeCOmamnd { get; set; }

        #endregion

        #region Constructor

        public AddRecipeViewModel()
        {
            RecipeListViewModel = new RecipeListViewModel();
            Recipes = new();

            AddFilesCommand = new RelayCommand(() => AddFiles());
        }

        #endregion

        #region Private Helpers

        private void AddFiles(string[] filepaths = null)
        {
            if (filepaths == null)
            {
                string filterString = string.Join(";", ForkGlobalSettings.AcceptedRecipeUploadFormats.Select(p => "*" + p));
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


        #endregion

        public void OnFileDrop(string[] filepaths)
        {
            List<string> errors = new();
            foreach (var path in filepaths)
            {
                RecipeParser.TryParseRecipe(path, out Recipe recipe, out string error);
                if (recipe != null)
                {
                    RecipeListViewModel.RecipeList.Add(new RecipeListItemViewModel(recipe));
                    Recipes.Add(recipe);
                }
                if (error != null)
                {
                    errors.Add(error);
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

        public void RecipeSelected(object obj, ListItemSelectedEventArgs e)
        {
            Recipe recipe = Recipes.First(p => p.Name.Equals(e.Name));
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
            RecipeViewModel = new RecipeViewModel(recipe);
        }
    }
}
