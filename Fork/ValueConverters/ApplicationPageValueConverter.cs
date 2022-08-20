using System;
using System.Diagnostics;
using System.Globalization;

namespace Fork
{
    /// <summary>
    /// Converts the <see cref="APplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Recipes:
                    var list = new RecipeListView();
                    list.DataContext = new RecipeListViewModel();
                    return list;

                case ApplicationPage.MealPrep:
                    list = new RecipeListView();
                    list.DataContext = new RecipeListViewModel();
                    return list;

                case ApplicationPage.GroceryList:
                    list = new RecipeListView();
                    list.DataContext = new RecipeListViewModel();
                    return list;

                case ApplicationPage.Ingredients:
                    list = new RecipeListView();
                    list.DataContext = new RecipeListViewModel();
                    return list;

                case ApplicationPage.Techniques:
                    list = new RecipeListView();
                    list.DataContext = new RecipeListViewModel();
                    return list;

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
