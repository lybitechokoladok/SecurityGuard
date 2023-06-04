using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Enums
{
    public enum RequestState
    {
        New = 0,
        Approved = 1,
        InProcess = 2,
        Closed = 3
    }
}
