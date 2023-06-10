using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Enums
{
    public enum RequestState
    {
        New = 1,
        Approved = 2,
        InProcess = 3,
        Closed = 4
    }
}
