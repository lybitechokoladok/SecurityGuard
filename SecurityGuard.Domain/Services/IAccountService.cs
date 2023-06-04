using SecurityGuard.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Services
{
    public interface IAccountService
    {
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsunc(string username, string password);
    }
}
