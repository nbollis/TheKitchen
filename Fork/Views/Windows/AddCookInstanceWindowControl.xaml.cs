using System.Windows;

namespace Fork
{
    /// <summary>
    /// Interaction logic for AddCookInstanceWindowControl.xaml
    /// </summary>
    public partial class AddCookInstanceWindowControl : Window
    {

        public AddCookInstanceWindowControl(RecipeViewModel dataContext)
        {
            InitializeComponent();
            CommentRecipeContentBorder.DataContext = dataContext;
        }
    }
}
