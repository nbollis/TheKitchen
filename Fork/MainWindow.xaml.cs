using System.Windows;

namespace Fork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private RecipeLogic RecipeLogic;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowViewModel(this);

        }

        private void AppWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((WindowViewModel)this.DataContext).OnClosing(((RecipePageView)MainFrame.Content).DataContext);
        }
    }
}
