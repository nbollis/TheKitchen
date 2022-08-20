using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class EditCategoriesModel : EditCategoriesViewModel
    {
        public static EditCategoriesModel Instance = new EditCategoriesModel(); 
        public EditCategoriesModel() : base()
        {

        }

    }
}
