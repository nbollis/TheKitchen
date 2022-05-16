using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    /// <summary>
    /// A view model for the overview recipe list
    /// </summary>
    public class RecipeListViewModel : BaseViewModel
    {
        /// <summary>
        /// recipe list itesm for the list
        /// </summary>
        public List<RecipeListItemViewModel> Recipes { get; set; }
    }
}
