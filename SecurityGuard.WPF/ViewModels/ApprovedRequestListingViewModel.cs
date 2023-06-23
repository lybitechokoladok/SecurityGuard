using MVVMEssentials.ViewModels;
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
    public class ApprovedRequestListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ApprovedRequestListingItemViewModel> _requestListingItemViewModels;
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly RequestStore _requestStore;

        public ICollectionView ApprovedRequestCollectionView { get; }
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
                ApprovedRequestCollectionView.SortDescriptions.Clear();

                if (value == 0)
                    ApprovedRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
                else
                    ApprovedRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.ArrivalDate), ListSortDirection.Descending));
            }
        }

        private int _typeFilterIndex;
        public int TypeFilterIndex
        {
            get { return _typeFilterIndex; }
            set
            {
                _typeFilterIndex = value;
                OnPropertyChanged(nameof(TypeFilterIndex));
                ApprovedRequestCollectionView.SortDescriptions.Clear();

                if (value == 0)
                    ApprovedRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.Type), ListSortDirection.Ascending));
                else
                    ApprovedRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.Type), ListSortDirection.Descending));
            }
        }

        private ApprovedRequestListingItemViewModel _selectedRequest;

        public ApprovedRequestListingItemViewModel SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));

                _selectedRequestStore.SelectedRequest = _selectedRequest?.Request;
            }
        }

        private string _requestFilter = string.Empty;

        public ICommand StartRequestCommand { get; }
        public ICommand LoadApprovedRequestCommand { get; }
        public ApprovedRequestListingViewModel(
            RequestStore requestStore,
            SelectedRequestStore selectedRequestStore)
        {
            _requestListingItemViewModels = new ObservableCollection<ApprovedRequestListingItemViewModel>();
            _requestStore = requestStore;
            _selectedRequestStore = selectedRequestStore;

            ApprovedRequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);
            ApprovedRequestCollectionView.SortDescriptions.Add(
                new SortDescription(nameof(ApprovedRequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
            ApprovedRequestCollectionView.SortDescriptions.Add(
                new SortDescription(nameof(ApprovedRequestListingItemViewModel.Type), ListSortDirection.Ascending));

            StartRequestCommand = new StartRequestCommand(requestStore, this);
            LoadApprovedRequestCommand = new LoadApprovedRequestCommand(requestStore);
            LoadApprovedRequestCommand.Execute(null);

            ApprovedRequestCollectionView.Filter = FilterRequest;

            _requestStore.ApprovedRequestLoaded += OnRequestLoaded;
            _selectedRequestStore = selectedRequestStore;
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
            ApprovedRequestListingItemViewModel itemViewModel =
                new ApprovedRequestListingItemViewModel(request, StartRequestCommand);
            _requestListingItemViewModels.Add(itemViewModel);
        }
        private bool FilterRequest(object obj)
        {

            if (obj is ApprovedRequestListingItemViewModel request)
            {
                if (request.FullName.Contains(RequestFilter, StringComparison.InvariantCultureIgnoreCase))
                    HasSearchResult = true;
                else
                    HasSearchResult = false;

                return request.FullName.Contains(RequestFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        public string RequestFilter
        {
            get { return _requestFilter; }
            set
            {
                _requestFilter = value;
                OnPropertyChanged(nameof(RequestFilter));
                ApprovedRequestCollectionView.Refresh();
            }
        }
    }
}
