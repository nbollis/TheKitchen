using System;
using System.Linq;

namespace Fork
{
    public class CategoryModel : CategoryViewModel
    {
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static CategoryModel Instance => new CategoryModel();


        public CategoryModel() : base()
        {
            Category = ForkGlobalData.AllCategories.First();
            Name = Category.Name;
            Description = Category.Description;
            RGB = Category.RGB;
        }
    }
}
