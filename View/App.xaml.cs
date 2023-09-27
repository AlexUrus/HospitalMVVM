using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry DisplayRootRegistry;
        private HospitalViewModel _hospitalViewModel;

        public App()
        {
            DisplayRootRegistry = new DisplayRootRegistry();
            DisplayRootRegistry.RegisterWindowType<HospitalViewModel, MainWindow>();
            DisplayRootRegistry.RegisterWindowType<MessageViewModel, MessageWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _hospitalViewModel = new HospitalViewModel();

            await DisplayRootRegistry.ShowModalPresentation(_hospitalViewModel);

            Shutdown();
        }
    }
}
