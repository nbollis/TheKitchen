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
                case ApplicationPage.Home:
                    var list = new RecipeListControl();
                    list.DataContext = new RecipeListViewModel(new RecipePageViewModel());
                    return list;

                case ApplicationPage.Recipe:
                    return new RecipePageControl();

                case ApplicationPage.SelectionPage:
                    return new SelectionPageControl();

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
