using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for IngredientListItemEditable.xaml
    /// </summary>
    public partial class IngredientListItemEditableControl : UserControl
    {
        public IngredientListItemEditableControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to check the input of the serves box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("//^[0-9]+(\\.[0-9]+)?$");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
