using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestListingItemViewModel
    {
        public int RequestNumber { get; set; }

        public string FullName { get; set; }



        public RequestListingItemViewModel(int requestNumber, string fullName)
        {
            RequestNumber = requestNumber;
            FullName = fullName;
        }
    }
}
