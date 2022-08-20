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
    /// Interaction logic for AddCategoryWindowControl.xaml
    /// </summary>
    public partial class AddCategoryWindowControl : Window
    {
        public AddCategoryWindowControl(EditCategoriesViewModel dataContext)
        {
            InitializeComponent();
            AddCategoryContentBorder.DataContext = dataContext;
        }
    }
}
