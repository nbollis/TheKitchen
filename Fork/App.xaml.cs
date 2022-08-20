using System.Threading.Tasks;
using System.Windows;
using ForkCore;
using Dna;
using static Dna.FrameworkDI;
using static Fork.DI;
using static ForkCore.CoreDI;

namespace Fork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            await ApplicationSetupAsync();

            // Log it
            Logger.LogDebugSource("Application starting...");

            // Setup the application view model based on if we are logged in
            ViewModelApplication.GoToPage(ApplicationPage.Recipes);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddFasettoWordViewModels()
                .AddFasettoWordClientServices()
                .Build();

            // Load new settings
            //TaskManager.RunAndForget(ViewModelSettings.LoadAsync);
        }
    }
}
