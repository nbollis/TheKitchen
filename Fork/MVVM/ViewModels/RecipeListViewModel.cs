using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;


/* 
 TODO: 
 sorting
 */
namespace Fork
{
    /// <summary>
    /// A view model for the overview recipe list
    /// </summary>
    public class RecipeListViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// recipe list itesm for the list
        /// </summary>
        public ObservableCollection<RecipeViewModel> RecipeList { get; set; }


        /// <summary>
        /// The selected Recipe 
        /// </summary>
        public RecipeViewModel SelectedItem { get; set; }

        #endregion

        #region Public Commands

        public ICommand RecipeSelectedCommand { get; set; }

        #endregion

        #region Constructors

        public RecipeListViewModel(ObservableCollection<RecipeViewModel> recipes)
        {
            RecipeList = new ObservableCollection<RecipeViewModel>(recipes);
            //foreach (var recipe in recipes)
            //{
            //    RecipeList.Add(new RecipeListItemViewModel(recipe));
            //}

            RecipeSelectedCommand = new DelegateCommand(param => RecipeSelected(param));
        }

        public RecipeListViewModel()
        {
            RecipeList = new ObservableCollection<RecipeViewModel>();
        }
        #endregion

        #region Command Methods

        /// <summary>
        /// Event when a Recipe is Selected
        /// </summary>
        /// <param name="obj"></param>
        public void RecipeSelected(object obj)
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsSelected = false;
            }
            SelectedItem = RecipeList.First(p => p.Name.Equals(obj));
            SelectedItem.IsSelected = true;
        }

        #endregion

    }
}
