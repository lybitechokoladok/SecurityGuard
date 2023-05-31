using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Services
{
    public class LoginLayoutNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public LoginLayoutNavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LoginLayoutViewModel(_createViewModel());
        }
    }
}
