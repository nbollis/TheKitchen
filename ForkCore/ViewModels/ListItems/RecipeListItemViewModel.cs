using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace ForkCore
{
    public class RecipeListItemViewModel : BaseViewModel
    {
        #region Private


        private RecipeViewModel recipeViewModel;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return recipeViewModel.Name; } 
        }

        public RecipeViewModel RecipeViewModel
        {
            get { return recipeViewModel; }
            set { recipeViewModel = value; OnPropertyChanged(nameof(RecipeViewModel)); }
        }

        public bool IsSelected { get; set; }   

        #endregion

        #region Public Commands

        // not sure if used
        public ICommand OpenRecipeCommand { get; set; }

        public ICommand RecipeSelectedCommand { get; set; }
        public static event EventHandler<ListItemSelectedEventArgs>? ItemSelected;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for real time data
        /// </summary>
        /// <param name="recipe"></param>
        public RecipeListItemViewModel(RecipeViewModel recipe)
        {
            RecipeViewModel = recipe;
            IsSelected = false;

            OpenRecipeCommand = new RelayCommand(OpenRecipe);
            RecipeSelectedCommand = new RelayCommand(() => SelectRecipe());
        }

        /// <summary>
        /// Constructor for design time data
        /// </summary>
        public RecipeListItemViewModel()
        {
            RecipeViewModel = new(ModelDataInjector.GetRecipe());

            OpenRecipeCommand = new RelayCommand(OpenRecipe);
            RecipeSelectedCommand = new RelayCommand(() => SelectRecipe());
        }

        #endregion

        #region Command Methods 

        public void OpenRecipe()
        {
            IsSelected = true;
        }

        public void SelectRecipe()
        {
            IsSelected = true;
            ListItemSelectedEventArgs e = new ListItemSelectedEventArgs(RecipeViewModel.Name); 
            ItemSelected?.Invoke(this, e);
        }

        #endregion
    }

}
