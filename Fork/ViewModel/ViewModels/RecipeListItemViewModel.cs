using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    public class RecipeListItemViewModel : BaseViewModel
    {
        #region Public Properties
        public string Name { get; set; } 
        public ObservableCollection<TagViewModel> Tags { get; set; }

        //public picture Picture
        public bool IsSelected { get; set; }   

        #endregion

        #region Public Commands

        public ICommand OpenRecipeCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for real time data
        /// </summary>
        /// <param name="recipe"></param>
        public RecipeListItemViewModel(Recipe recipe)
        {
            Name = recipe.Name;
            Tags = new ObservableCollection<TagViewModel>();
            IsSelected = false;

            OpenRecipeCommand = new RelayCommand(OpenRecipe);
        }

        /// <summary>
        /// Constructor for design time data
        /// </summary>
        public RecipeListItemViewModel()
        {
            Name = "Tacos";
            Tags = new ObservableCollection<TagViewModel>();
        }

        #endregion

        #region Command Methods 

        public void OpenRecipe()
        {
            IsSelected = true;
        }

        #endregion
    }

}
