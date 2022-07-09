using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public RecipeListModel() 
        {
            RecipeList = new ObservableCollection<RecipeListItemViewModel>
            {
                new RecipeListItemModel(),
                new RecipeListItemModel(),
                new RecipeListItemModel(),
                new RecipeListItemModel()
            };
        }

    }
}
