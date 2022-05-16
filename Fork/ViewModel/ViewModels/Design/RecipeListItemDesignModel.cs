using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class RecipeListItemDesignModel : RecipeListItemViewModel
    {
        /// <summary>
        /// a single instance of the design model
        /// </summary>
        public static RecipeListItemDesignModel Instance => new RecipeListItemDesignModel();

        public RecipeListItemDesignModel() : base()
        {
            Name = "Buttery Chicken";
            Tags.Add(new TagDesignModel());
            Tags.Add(new TagDesignModel());
            Tags.Add(new TagDesignModel());
            IsSelected = false;
            NewContentAvailable = false;
        }
    }
}
