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
        public int Type { get; set; }
        public string VisitingReason { get; set; } = string.Empty;
        public DateTime ArrivaDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Client ClientId { get; set; }
        public MembersGroup? GroupId { get; set; }

        public override string ToString()
        {
            return ClientId.FirstName + " " + ClientId.LastName + " " + ClientId.Patronomic;
        }
    }
}
