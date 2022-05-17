using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class RecipePageViewModel : BaseViewModel
    {
        private List<Recipe> _Recipes;

        #region Public Properties

        public List<Recipe> Recipes
        {
            get { return _Recipes; }
            set
            {
                _Recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }

        }

        public static int BufferThickness { get; set; } = 20;

        #endregion

        #region Constructor

        public RecipePageViewModel()
        {
            _Recipes = new List<Recipe>();
        }

        #endregion
    }
}
