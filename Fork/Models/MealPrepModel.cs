using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class MealPrepModel : MealPrepViewModel
    {
        public static MealPrepModel Instance => new MealPrepModel();
        public MealPrepModel() : base(ModelDataInjector.GetMealPrep())
        {
        }
    }
}
