using System.Text.RegularExpressions;
using System.Windows;

namespace Fork
{
    /// <summary>
    /// Interaction logic for EditRecipeWindow.xaml
    /// </summary>
    public partial class EditRecipeWindowControl : Window
    {
        public EditRecipeWindowControl(RecipeDisplayViewModel dataContext)
        {
            InitializeComponent();
            EditRecipeContentBorder.DataContext = dataContext;
        }

        /// <summary>
        /// Event to check the input of the serves box 
        /// Only allows integers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
