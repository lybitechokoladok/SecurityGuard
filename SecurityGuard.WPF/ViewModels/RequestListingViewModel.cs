using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<RequestListingItemViewModel> _requestListingItemViewModels;

        public ICollectionView RequestCollectionView { get; }

        private string _requestFilter = string.Empty;

        public string RequestFilter
        {
            get { return _requestFilter; }
            set 
            {
                _requestFilter = value;
                OnPropertyChanged(nameof(RequestFilter));
                RequestCollectionView.Refresh();
            }
        }


        public RequestListingViewModel()
        {
            _requestListingItemViewModels = new ObservableCollection<RequestListingItemViewModel>();
            RequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);

            RequestCollectionView.Filter = FilterRequest;

            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"123", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Nick", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Andrew", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Dima", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Nick", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Shon", DateTime.UtcNow));
            _requestListingItemViewModels.Add(new RequestListingItemViewModel(1,"Dmitry", DateTime.UtcNow));
        }

        private bool FilterRequest(object obj)
        {
            if(obj is RequestListingItemViewModel request) 
            {
                return request.FullName.Contains(RequestFilter);
            }

            return false;
        }
    }
}
