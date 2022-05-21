using System.Windows.Controls;

namespace Fork
{
    /// <summary>
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePageControl : Page
    {
        public RecipePageControl()
        {
            InitializeComponent();
            var viewModel = new RecipePageViewModel();
            this.DataContext = viewModel;
        }
    }
}
