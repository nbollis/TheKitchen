using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class RecipeListModel : RecipeListViewModel
    {
        /// <summary>
        /// Creates an instance of Recipe List Design Model
        /// </summary>
        public static RecipeListModel Instance => new RecipeListModel();

        public RecipeListModel() : base()
        {
            Recipes = new List<RecipeListItemViewModel>
            {
                new RecipeListItemViewModel(),
                new RecipeListItemViewModel(),
                new RecipeListItemViewModel()
            };
        }

    }
}
