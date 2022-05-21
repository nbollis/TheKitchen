using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class RecipePageModel : RecipePageViewModel
    {

        public static RecipePageModel Instance => new RecipePageModel();

        public RecipePageModel()
        {
            RecipeListViewModel = new RecipeListModel();
            
        }
    }
}
