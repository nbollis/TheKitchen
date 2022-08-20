using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fork
{
    /// <summary>
    /// Interaction logic for AddRecipeWindowView.xaml
    /// </summary>
    public partial class AddRecipeWindowView : Window
    {
        public AddRecipeWindowView(AddRecipeViewModel addRecipeViewModel)
        {
            InitializeComponent();
            AddRecipeContentBorder.DataContext = addRecipeViewModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ForkGlobalData.CheckIfValueIsInteger(e.Text);
        }
    }
}
