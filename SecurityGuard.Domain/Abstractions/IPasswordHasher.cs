using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Abstractions
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool HashesMatch(string passwordHash, string providedPassword);
    }
}
