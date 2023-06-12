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

        public ICommand LoadRequestsCommand { get; }

        public RequestListingViewModel(RequestStore requestStore)
        {
            _requestStore = requestStore;
            _requestListingItemViewModels = new ObservableCollection<RequestListingItemViewModel>();
            RequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);

            RequestCollectionView.Filter = FilterRequest;

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
        }

        private void AddRequest(Request request)
        {
            RequestListingItemViewModel itemViewModel = 
                new RequestListingItemViewModel(request);
            _requestListingItemViewModels.Add(itemViewModel);
        }

        private bool FilterRequest(object obj)
        {
            if(obj is RequestListingItemViewModel request) 
            {
                return request.FullName.Contains(RequestFilter);
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
