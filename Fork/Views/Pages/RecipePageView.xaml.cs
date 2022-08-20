using System.Windows.Controls;

namespace Fork
{
    /// <summary>
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePageView : Page
    {
        public RecipePageView()
        {
            InitializeComponent();
            var viewModel = new RecipePageViewModel();
            this.DataContext = viewModel;
        }
    }
}
