using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class ClientStore
    {
        private readonly IClientRepository _clientRepository;
        private List<Client> _clients;


        public IEnumerable<Client> Client => _clients;

        public event Action ClientsIsBanned;
        public ClientStore(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _clients = new List<Client>(); 
        }

        public async Task<bool> IsClientsBlackList(int clientId) 
        {
            return await _clientRepository.FindClientBlackListByIdAsync(clientId);
        }
    }
}
