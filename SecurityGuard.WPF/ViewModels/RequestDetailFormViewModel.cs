using MVVMEssentials.Commands;
using MVVMEssentials.Services;
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
    public class RequestDetailFormViewModel : ViewModelBase
    {
        private readonly RequestStore _requestStore;

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
        public RequestDetailFormViewModel(INavigationService closeRequestDetailnavigationService, RequestStore requestStore)
        {
            _requestStore = requestStore;
            CloseRequestDetailCommand = new NavigateCommand(closeRequestDetailnavigationService);
        }
    }
}
