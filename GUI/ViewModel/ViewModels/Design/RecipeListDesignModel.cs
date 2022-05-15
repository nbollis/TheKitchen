using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class RecipeListDesignModel : RecipeListViewModel
    {
        /// <summary>
        /// Creates an instance of Recipe List Design Model
        /// </summary>
        public static RecipeListDesignModel Instance => new RecipeListDesignModel();

        public RecipeListDesignModel()
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
