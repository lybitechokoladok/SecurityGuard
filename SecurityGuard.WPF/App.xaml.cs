using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Abstractions;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Services;
using SecurityGuard.Infrastructure;
using SecurityGuard.Infrastructure.Repositories;
using SecurityGuard.WPF.Services;
using SecurityGuard.WPF.Stores;
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
                     serviceCollection.AddSingleton<RequestStore>();
                     serviceCollection.AddSingleton<MemberStore>();
                     serviceCollection.AddSingleton<ClientStore>();
                     serviceCollection.AddSingleton<SelectedRequestStore>();
                     serviceCollection.AddSingleton<INavigationService>(s => CreateLoginNavigationService(s));
                     serviceCollection.AddSingleton<CloseModalNavigationService>();
                     serviceCollection.AddSingleton<AccountStore>();

                     serviceCollection.AddSingleton<DbConnection>();
                     serviceCollection.AddScoped<IMemberRepository, MemberRepository>();
                     serviceCollection.AddScoped<IClientRepository, ClientRepository>();
                     serviceCollection.AddScoped<IUserRepository,UserRepository > ();
                     serviceCollection.AddScoped<IRequestRepository, RequestRepository>();
                     serviceCollection.AddScoped<IAccountService, AccountService>();
                     serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();

                     serviceCollection.AddTransient<LoginViewModel>((s) => new LoginViewModel(
                         CreateRequestListingNavigationService(s),
                         s.GetRequiredService<IAccountService>()));
                     serviceCollection.AddTransient<RequestListingViewModel>(s => new RequestListingViewModel(
                         s.GetRequiredService<RequestStore>(),
                         s.GetRequiredService<SelectedRequestStore>(),
                         s.GetRequiredService<ClientStore>(),
                         CreateRequestDetailNavigationService(s)));
                     serviceCollection.AddTransient<RequestTypeStatisticViewModel>(s => new RequestTypeStatisticViewModel(
                         s.GetRequiredService<RequestStore>()));
                     serviceCollection.AddTransient<RequestDetailViewModel>(s => new RequestDetailViewModel(
                         s.GetRequiredService<SelectedRequestStore>(),
                         s.GetRequiredService<MemberStore>(),
                         s.GetRequiredService<RequestStore>(),
                         s.GetRequiredService<ModalNavigationStore>(),
                         s.GetRequiredService<CloseModalNavigationService>()));
                     serviceCollection.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);

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
            return new LoginLayoutNavigationService<LoginViewModel>(
                s.GetRequiredService<NavigationStore>(),
                () => s.GetRequiredService<LoginViewModel>());
        }

        private INavigationService CreateRequestListingNavigationService(IServiceProvider s) 
        {
            return new LayoutNavigationService<RequestListingViewModel>(
                s.GetRequiredService<NavigationStore>(),
                () => s.GetRequiredService<RequestListingViewModel>(),
                () => s.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateRequestDetailNavigationService(IServiceProvider s)
        {
            return new ModalNavigationService<RequestDetailViewModel>(
                s.GetService<ModalNavigationStore>(),
                () => s.GetRequiredService<RequestDetailViewModel>());
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider arg)
        {
            return new NavigationBarViewModel(arg.GetRequiredService<AccountStore>(),
                CreateRequestListingNavigationService(arg),
                CreateStatisticsNavigationService(arg),
                CreateLoginNavigationService(arg));
        }

        private INavigationService CreateStatisticsNavigationService(IServiceProvider arg)
        {
            return new LayoutNavigationService<RequestTypeStatisticViewModel>(arg.GetRequiredService<NavigationStore>(),
                () => arg.GetRequiredService<RequestTypeStatisticViewModel>(),
                () => arg.GetRequiredService<NavigationBarViewModel>());
        }
    }
}
