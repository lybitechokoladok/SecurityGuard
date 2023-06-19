using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.WPF.Stores;
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
        public RequestDetailFormViewModel RequestDetailsFormViewModel { get; set; }
        public RequestDetailViewModel(SelectedRequestStore selectedRequestStore, MemberStore memberStore, INavigationService closeNavigationService)
        {
            RequestDetailsFormViewModel = new RequestDetailFormViewModel(closeNavigationService, memberStore, selectedRequestStore)
            {
                RequestId = selectedRequestStore.SelectedRequest.Id,
                FullName = selectedRequestStore.SelectedRequest.ToString(),
                ArrivalDate = selectedRequestStore.SelectedRequest.ArrivalDate
            };
        }
    }
}
