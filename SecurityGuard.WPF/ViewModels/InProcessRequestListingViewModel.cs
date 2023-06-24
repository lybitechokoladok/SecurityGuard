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
    public class InProcessRequestListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<InProcessRequestListingItemViewModel> _requestListingItemViewModels;
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly RequestStore _requestStore;

        public ICollectionView InProcessRequestCollectionView { get; }
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
                InProcessRequestCollectionView.SortDescriptions.Clear();

                if (value == 0)
                    InProcessRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
                else
                    InProcessRequestCollectionView.SortDescriptions.Add(
                        new SortDescription(nameof(ApprovedRequestListingItemViewModel.ArrivalDate), ListSortDirection.Descending));
            }
        }

        private InProcessRequestListingItemViewModel _selectedRequest;

        public InProcessRequestListingItemViewModel SelectedRequest
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
        public ICommand LoadInProcessRequestCommand { get; }
        public InProcessRequestListingViewModel(
            RequestStore requestStore,
            SelectedRequestStore selectedRequestStore)
        {
            _requestListingItemViewModels = new ObservableCollection<InProcessRequestListingItemViewModel>();
            _requestStore = requestStore;
            _selectedRequestStore = selectedRequestStore;

            InProcessRequestCollectionView = CollectionViewSource.GetDefaultView(_requestListingItemViewModels);
            InProcessRequestCollectionView.SortDescriptions.Add(
                new SortDescription(nameof(InProcessRequestListingItemViewModel.ArrivalDate), ListSortDirection.Ascending));
            InProcessRequestCollectionView.SortDescriptions.Add(
                new SortDescription(nameof(InProcessRequestListingItemViewModel.Type), ListSortDirection.Ascending));

            StartRequestCommand = new EndRequestCommand(requestStore, this);
            LoadInProcessRequestCommand = new LoadInProcessRequestCommand(requestStore);
            LoadInProcessRequestCommand.Execute(null);

            InProcessRequestCollectionView.Filter = FilterRequest;

            _requestStore.InProcessRequestLoaded += OnRequestLoaded;
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
            InProcessRequestListingItemViewModel itemViewModel =
                new InProcessRequestListingItemViewModel(request, StartRequestCommand);
            _requestListingItemViewModels.Add(itemViewModel);
        }
        private bool FilterRequest(object obj)
        {

            if (obj is InProcessRequestListingItemViewModel request)
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
                InProcessRequestCollectionView.Refresh();
            }
        }

        public override void Dispose()
        {
            _requestStore.InProcessRequestLoaded -= OnRequestLoaded;
            base.Dispose();
        }
    }
}
