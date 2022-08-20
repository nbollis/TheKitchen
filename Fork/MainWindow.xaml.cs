using System.Windows;

namespace Fork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationViewModel ApplicationViewModel => new ApplicationViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowViewModel(this);

        }

        private void AppWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //ApplicationViewModel.OnClosing(((RecipePageView)MainFrame.Content).DataContext);
        }
    }
}
