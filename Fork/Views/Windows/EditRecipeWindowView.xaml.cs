using System.Text.RegularExpressions;
using System.Windows;
using ForkDataHandling;

namespace Fork
{
    /// <summary>
    /// Interaction logic for EditRecipeWindow.xaml
    /// </summary>
    public partial class EditRecipeWindowView : Window
    {
        public EditRecipeWindowView(RecipeViewModel dataContext)
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
            e.Handled = ForkGlobalData.CheckIfValueIsInteger(e.Text);   
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
