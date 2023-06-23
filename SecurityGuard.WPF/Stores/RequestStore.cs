using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGuard.WPF.Commands;

namespace SecurityGuard.WPF.Stores
{
    public class RequestStore
    {
        private readonly IRequestRepository _requestRepository;
        private readonly List<Request> _requests;
        public IEnumerable<Request> Requests => _requests;


        public event Action RequestsLoaded;
        public event Action ApprovedRequestLoaded;
        public event Action InProcessRequestLoaded;
        public event Action <Request> RequestsSelected;
        public RequestStore(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;

            _requests = new List<Request>();
        }

        public async Task LoadAll()
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            _requests.Clear();
            _requests.AddRange(requests);

            RequestsLoaded?.Invoke();
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

            ApprovedRequestLoaded?.Invoke();
        }

        public async Task LoadAllCurrent()
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            var currentRequests = requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.InProcess);

            _requests.Clear();
            _requests.AddRange(currentRequests);

            InProcessRequestLoaded.Invoke();
        }

        public async Task<bool> UpdateRequestState(int requestDetailId, int newState) 
        {
            bool isUpdated = await _requestRepository.UpdateStateAsync(requestDetailId, newState);

            await LoadAllNew();

            if(isUpdated)
                return true;
            else
                return false;
        }

    }
}
