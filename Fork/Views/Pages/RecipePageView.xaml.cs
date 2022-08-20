using System.Windows.Controls;
using System.Windows.Markup;

namespace Fork
{
    /// <summary>
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePageView : BasePage
    {
        public RecipePageView(RecipesPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
