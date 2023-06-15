using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string VisitingReason { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; } 
        public DateTime CreationDate { get; set; }
        public Client Client { get; set; }
        public int? GroupId { get; set; }

        public override string ToString()
        {
            return Client.FirstName + " " + Client.LastName + " " + Client.Patronomic;
        }
    }
}
