using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Enums;
using SecurityGuard.Domain.Models;
using SecurityGuard.WPF.Commands;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<RequestListingItemViewModel> _requestListingItemViewModels;
        private readonly RequestStore _requestStore;

        public ICollectionView RequestCollectionView { get; }

        private int _requestCount;
        public int RequestCount 
        {
            get => _requestCount;
            set 
            {
                _requestCount = value;
                OnPropertyChanged(nameof(RequestCount));
            } 
        }

        private string _requestType;

        public string RequestType
        {
            get { return _requestType; }
            set 
            {
                _requestType = value;
                OnPropertyChanged(RequestType);
                RequestCollectionView.Refresh();
            }
        }


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

        public ICommand OpenRequestDetailCommand { get; }
        public ICommand LoadRequestsCommand { get; }

        public RequestListingViewModel(RequestStore requestStore, INavigationService openRequestDetailNavigationService)
        {
            _requestStore = requestStore;
            _requestListingItemViewModels = new ObservableCollection<RequestListingItemViewModel>();
            RequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);
            RequestCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(RequestListingItemViewModel.RequestType)));

            RequestCollectionView.Filter = FilterRequest;

            OpenRequestDetailCommand = new NavigateCommand(openRequestDetailNavigationService);
            LoadRequestsCommand = new LoadRequestsCommand(this, requestStore);
            LoadRequestsCommand.Execute(null);

            _requestStore.RequestsLoaded += OnRequestLoaded;

        }


        private void OnRequestLoaded()
        {
            _requestListingItemViewModels.Clear();

            foreach (Request request in _requestStore.Requests) 
            {
                AddRequest(request);
            }

            RequestCount = _requestListingItemViewModels.Count;
        }

        private void AddRequest(Request request)
        {
            RequestListingItemViewModel itemViewModel = 
                new RequestListingItemViewModel(request, OpenRequestDetailCommand);
            _requestListingItemViewModels.Add(itemViewModel);
        }

        private bool FilterRequest(object obj)
        {
            if(obj is RequestListingItemViewModel request) 
            {
                return request.FullName.Contains(RequestFilter, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public override void Dispose()
        {
            _requestStore.RequestsLoaded -= OnRequestLoaded;

            base.Dispose();
        }
    }
}
