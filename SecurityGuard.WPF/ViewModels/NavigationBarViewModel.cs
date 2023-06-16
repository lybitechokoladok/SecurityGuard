using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Enums;
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

        public bool IsGeneralDepartmentOfficer => _accountStore.CurrentUser.Role == (int)Role.GeneralDepartmentOfficer;

        public NavigationBarViewModel(AccountStore accountStore, INavigationService requestListingNavigationService)
        {
            _accountStore = accountStore;
            NavigateRequestListingCommand = new NavigateCommand(requestListingNavigationService);
        }
    }
}
