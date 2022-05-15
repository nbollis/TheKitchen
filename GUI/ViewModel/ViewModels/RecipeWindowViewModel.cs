using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace GUI.ViewModel.Models
{
    public class RecipeWindowViewModel : BaseViewModel
    {
        private List<Recipe> _Recipes;
        public List<Recipe> Recipes
        {
            get {  return _Recipes; }
            set 
            { 
                _Recipes = value; 
                OnPropertyChanged(nameof(Recipes));
            }

        }

        #region Constructor

        public RecipeWindowViewModel()
        {
            _Recipes = new List<Recipe>();
        }
        #endregion
    }
}
