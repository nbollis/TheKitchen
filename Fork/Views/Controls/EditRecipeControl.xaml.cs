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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fork
{
    /// <summary>
    /// Interaction logic for EditRecipeControl.xaml
    /// </summary>
    public partial class EditRecipeControl : UserControl
    {
        public EditRecipeControl()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ForkGlobalSettings.CheckIfValueIsInteger(e.Text);
        }
    }
}
