using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Models;
using SecurityGuard.WPF.Commands;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestDetailFormViewModel : ViewModelBase
    {
        private readonly ObservableCollection<RequestGroupMemberItemViewModel> _members;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly RequestStore _requestStore;

        private readonly MemberStore _membersStore;

        public IEnumerable<RequestGroupMemberItemViewModel> Members => _members;

        public int RequestDetailId => _selectedRequestStore.SelectedRequest.RequestDetails.Id;
        public bool HasGroup { get; set; }

        private int? _groupId;
        public int? GroupId
        {
            get { return _groupId; }
            set 
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }


        private int _id;

        public int RequestId
        {
            get { return _id; }
            set 
            {
                _id = value;
                OnPropertyChanged(nameof(RequestId));
            }
        }

        private string _pasportNumber;

        public string PasportNumber
        {
            get { return _pasportNumber; }
            set
            {
                _pasportNumber = value;
                OnPropertyChanged(nameof(PasportNumber));
            }
        }

        private string _pasportSeries;

        public string PasportSeries
        {
            get { return _pasportSeries; }
            set
            {
                _pasportSeries = value;
                OnPropertyChanged(nameof(PasportSeries));
            }
        }

        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private DateTime _arrivalDate;

        public DateTime ArrivalDate
        {
            get { return _arrivalDate; }
            set 
            {
                _arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }

        public ICommand CloseRequestDetailCommand { get; }
        public ICommand UpdateRequestStateCommand { get; }
        public ICommand LoadGroupMembersCommand { get; }
        public RequestDetailFormViewModel
            (INavigationService closeRequestDetailnavigationService,
            MemberStore memberStore,
            RequestStore requestStore,
            ModalNavigationStore modalNavigationStore,
            SelectedRequestStore selectedRequestStore)
        {
            _members = new ObservableCollection<RequestGroupMemberItemViewModel>();
            _selectedRequestStore= selectedRequestStore;
            _modalNavigationStore = modalNavigationStore;
            _requestStore = requestStore;
            GroupId = selectedRequestStore.SelectedRequest.GroupId;
            HasGroup = selectedRequestStore.SelectedRequest.GroupId != null;

            _membersStore = memberStore;
            CloseRequestDetailCommand = new NavigateCommand(closeRequestDetailnavigationService);
            LoadGroupMembersCommand = new LoadGroupMembersCommand(this, _membersStore, _selectedRequestStore);
            UpdateRequestStateCommand = new UpdateRequestStateCommand(requestStore, modalNavigationStore, this);
            LoadGroupMembersCommand.Execute(null);

            _membersStore.GroupMembersLoaded += OnGroupMembersLoaded;
        }

        private void OnGroupMembersLoaded()
        {
            _members.Clear();

            foreach(GroupMember member in _membersStore.Members) 
            {
                AddMember(member);
            }

        }

        private void AddMember(GroupMember member)
        {
            RequestGroupMemberItemViewModel itemViewModel =
                new RequestGroupMemberItemViewModel(member);
            _members.Add(itemViewModel);
        }

        public override void Dispose()
        {
            _membersStore.GroupMembersLoaded -= OnGroupMembersLoaded;
            base.Dispose();
        }
    }
}
