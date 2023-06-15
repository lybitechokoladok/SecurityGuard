using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestDetailViewModel : ViewModelBase
    {
        public ICommand CloseRequestDetailCommand { get; }
        public RequestDetailViewModel(INavigationService closeRequestDetailnavigationService)
        {
            CloseRequestDetailCommand = new NavigateCommand(closeRequestDetailnavigationService);
        }
    }
}
