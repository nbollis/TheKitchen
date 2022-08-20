using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class RecipeListItemModel : RecipeListItemViewModel
    {
        /// <summary>
        /// a single instance of the design model
        /// </summary>
        public static RecipeListItemModel Instance => new RecipeListItemModel();

        public RecipeListItemModel() : base(new RecipeViewModel(ModelDataInjector.GetRecipe()))
        {
            IsSelected = false;
        }
    }
}
