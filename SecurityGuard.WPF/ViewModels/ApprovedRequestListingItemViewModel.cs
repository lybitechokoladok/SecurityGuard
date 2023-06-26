using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class ApprovedRequestListingItemViewModel
    {
        public Request Request { get; private set; }
        public int ClientNumber => Request.Clients.Id;
        public int RequestNumber => Request.Id;
        public string Type => Request.Type.Description;
        public string RequestType => Request.Type.Description;

        public string FullName => Request.ToString();

        public DateTime ArrivalDate => Request.ArrivalDate;

        public ICommand StartRequestCommand { get; }
        public ApprovedRequestListingItemViewModel(Request request, ICommand startRequestCommand)
        {
            Request = request;
            StartRequestCommand = startRequestCommand;
        }
    }
}
