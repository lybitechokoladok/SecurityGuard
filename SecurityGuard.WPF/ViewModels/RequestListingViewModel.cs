using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<RequestListingItemViewModel> _requestListingItemViewModels;

        public IEnumerable<RequestListingItemViewModel> RequestListingViewModels => _requestListingItemViewModels;

        public RequestListingViewModel()
        {
            _requestListingItemViewModels = new ObservableCollection<RequestListingItemViewModel>();

            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"123", DateTime.UtcNow));
        }
    }
}
