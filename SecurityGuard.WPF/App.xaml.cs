using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.WPF.Services;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityGuard.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                 .CreateDefaultBuilder()
                 .ConfigureServices((context, serviceCollection) =>
                 {
                     serviceCollection.AddSingleton<NavigationStore>();
                     serviceCollection.AddSingleton<ModalNavigationStore>();
                     serviceCollection.AddSingleton<INavigationService>(s => CreateLoginNavigationService(s));
                     serviceCollection.AddSingleton<CloseModalNavigationService>();

                     serviceCollection.AddTransient<LoginViewModel>();
                     serviceCollection.AddTransient<NavigationBarViewModel>();

                     serviceCollection.AddSingleton<MainViewModel>();
                     serviceCollection.AddSingleton<MainWindow>((services) => new MainWindow()
                     {
                         DataContext = services.GetRequiredService<MainViewModel>()
                     }) ;
                 })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private INavigationService CreateLoginNavigationService(IServiceProvider s)
        {
            return new LayoutNavigationService<LoginViewModel>(
                s.GetRequiredService<NavigationStore>(),
                () => s.GetRequiredService<LoginViewModel>());
        }
    }
}
