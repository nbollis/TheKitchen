using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class MealPrepPageModel : MealPrepPageViewModel
    {
        public static MealPrepPageModel Instance => new MealPrepPageModel();
        public MealPrepPageModel() : base()
        {
            MealPreps.Add(ModelDataInjector.GetMealPrep().ToViewModel());
            MealPreps.Add(ModelDataInjector.GetMealPrep().ToViewModel());
            MealPreps.Add(ModelDataInjector.GetMealPrep().ToViewModel());
        }
    }
}
