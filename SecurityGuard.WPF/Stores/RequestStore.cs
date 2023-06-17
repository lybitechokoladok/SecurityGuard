using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class RequestStore
    {
        private readonly IRequestRepository _requestRepository;
        private readonly List<Request> _requests;
        public IEnumerable<Request> Requests => _requests;


        public event Action RequestsLoaded;
        public event Action <Request> RequestsSelected;
        public RequestStore(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;

            _requests = new List<Request>();
        }

        public async Task  LoadAllNew() 
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            var newRequests = requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.New);

            _requests.Clear();
            _requests.AddRange(newRequests);

            RequestsLoaded?.Invoke();
        }

        public async Task LoadAllApproved() 
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            var approvedRequests = requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.Approved);

            _requests.Clear();
            _requests.AddRange(approvedRequests);


        }

        public async Task LoadAllCurrent()
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            var currentRequests = requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.InProcess);

            _requests.Clear();
            _requests.AddRange(currentRequests);
        }

        public void OpenSelectedRequestDetail(Request request) 
        {
            int currentIndex = _requests.FindIndex(y => y.Id == request.Id);

            if (currentIndex != -1)
                _requests[currentIndex] = request;
            else
                _requests.Add(request);

            RequestsSelected?.Invoke(request);
        }
    }
}
