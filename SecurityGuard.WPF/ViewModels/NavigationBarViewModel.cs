using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Enums;
using SecurityGuard.WPF.Commands;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;

        public ICommand NavigateRequestListingCommand { get; }
        public ICommand NavigateApprovedRequestListingCommand { get; }
        public ICommand NavigateInProcessRequestListingCommand { get; }
        public ICommand NavigateStatisticsCommand { get; }
        public ICommand NavigateLoginCommand { get; }

        public bool IsGeneralDepartmentOfficer => _accountStore.CurrentUser.Role == (int)Role.GeneralDepartmentOfficer;

        public NavigationBarViewModel(
            AccountStore accountStore, 
            INavigationService requestListingNavigationService,
            INavigationService approvedRequestListingNavigationService,
            INavigationService inProcessRequestListingNavigationService,
            INavigationService statisticsnavigationService,
            INavigationService loginNavigationService)
        {
            _accountStore = accountStore;
            NavigateRequestListingCommand = new NavigateCommand(requestListingNavigationService);
            NavigateApprovedRequestListingCommand = new NavigateCommand(approvedRequestListingNavigationService);
            NavigateInProcessRequestListingCommand = new NavigateCommand(inProcessRequestListingNavigationService);
            NavigateStatisticsCommand = new NavigateCommand(statisticsnavigationService);
            NavigateLoginCommand = new LogoutCommand(accountStore, loginNavigationService);
        }
    }
}
