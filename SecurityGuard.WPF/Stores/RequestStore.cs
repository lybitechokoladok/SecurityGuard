using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
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
        public RequestStore(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;

            _requests = new List<Request>();
        }

        public async Task  Load() 
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllListAsync();

            _requests.Clear();
            _requests.AddRange(requests);

            RequestsLoaded?.Invoke();
        }
    }
}
