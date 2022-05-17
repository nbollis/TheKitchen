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
                    return null;

                case ApplicationPage.Recipe:
                    return new RecipePage();

                case ApplicationPage.SelectionPage:
                    return new SelectionPage();

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
