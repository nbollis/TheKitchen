using System.Windows;

namespace Fork
{
    /// <summary>
    /// Interaction logic for AddCookInstanceWindowView.xaml
    /// </summary>
    public partial class AddCookInstanceWindowView : Window
    {

        public AddCookInstanceWindowView(RecipeViewModel dataContext)
        {
            InitializeComponent();
            CommentRecipeContentBorder.DataContext = dataContext;
        }
    }
}
