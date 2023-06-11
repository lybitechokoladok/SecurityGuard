using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Repositories
{
    public interface IRequestRepository
    {
        Task<Request> GetRequestByIdAsync(int id);
        Task<IEnumerable<Request>> GetAllListAsync();
        Task<IEnumerable<Client>> GetAllRequestClientAsync();
    }
}
