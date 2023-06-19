using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Models;
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

        private readonly MemberStore _membersStore;

        public IEnumerable<RequestGroupMemberItemViewModel> Members => _members;

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
        public RequestDetailFormViewModel(INavigationService closeRequestDetailnavigationService, MemberStore memberStore)
        {
            _members = new ObservableCollection<RequestGroupMemberItemViewModel>();

            _membersStore = memberStore;
            CloseRequestDetailCommand = new NavigateCommand(closeRequestDetailnavigationService);

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
    }
}
