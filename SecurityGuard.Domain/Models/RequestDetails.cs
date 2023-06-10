using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class RequestDetails
    {
        public int Id { get; set; }
        public Request Request { get; set; }
        public int RequestStateId { get; set; }
        public DateTime RequestTimesatmp { get; set; }
        public User User { get; set; }
    }
}
