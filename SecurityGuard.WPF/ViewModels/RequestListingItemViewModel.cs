using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestListingItemViewModel
    {
        public Request Request { get; private set; }
        public int RequestNumber => Request.Id; 

        public string RequestType => Request.Type;

        public string FullName => Request.ToString();

        public DateTime ArrivalDate => Request.ArrivaDate;

        public RequestListingItemViewModel(Request request)
        {
            Request = request;
        }
    }
}
