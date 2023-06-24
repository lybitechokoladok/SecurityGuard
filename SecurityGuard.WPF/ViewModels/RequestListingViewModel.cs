﻿using MVVMEssentials.Commands;
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
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly ClientStore _clientStore;
        private readonly RequestStore _requestStore;

        public ICollectionView RequestCollectionView { get; }
        private bool _hasSearchResult;

        public bool HasSearchResult
        {
            get { return _hasSearchResult; }
            set 
            {
                _hasSearchResult = value; 
                OnPropertyChanged(nameof(HasSearchResult));
            }
        }

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

        private int _dateFilterIndex;

        public int DateFilterIndex
        {
            get { return _dateFilterIndex; }
            set 
            {
                _dateFilterIndex = value;
                OnPropertyChanged(nameof(DateFilterIndex));
                RequestCollectionView.SortDescriptions.Clear();

                if (value == 0)
                    RequestCollectionView.SortDescriptions.Add(new SortDescription(nameof(RequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
                else
                    RequestCollectionView.SortDescriptions.Add(new SortDescription(nameof(RequestListingItemViewModel.ArrivalDate), ListSortDirection.Descending));
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

        private RequestListingItemViewModel _selectedRequest;

        public RequestListingItemViewModel SelectedRequest
        {
            get { return _selectedRequest; }
            set 
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));

                _selectedRequestStore.SelectedRequest = _selectedRequest?.Request;
            }
        }


        public ICommand OpenRequestDetailCommand { get; }
        public ICommand LoadRequestsCommand { get; }

        public RequestListingViewModel(RequestStore requestStore,
            SelectedRequestStore selectedRequestStore,
            ClientStore clientStore,
            INavigationService openRequestDetailNavigationService)
        {
            _requestStore = requestStore;
            _clientStore = clientStore;
            _selectedRequestStore = selectedRequestStore;
            _requestListingItemViewModels = new ObservableCollection<RequestListingItemViewModel>();
            RequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);
            RequestCollectionView.SortDescriptions.Add(new SortDescription(nameof(RequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
            RequestCollectionView.SortDescriptions.Add(new SortDescription(nameof(RequestListingItemViewModel.Type), ListSortDirection.Ascending));

            RequestCollectionView.Filter = FilterRequest;

            OpenRequestDetailCommand = new OpenRequestDetailCommand(openRequestDetailNavigationService, clientStore, this) ;
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
                if (request.FullName.Contains(RequestFilter, StringComparison.InvariantCultureIgnoreCase))
                    HasSearchResult = true;
                else
                    HasSearchResult = false;

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
