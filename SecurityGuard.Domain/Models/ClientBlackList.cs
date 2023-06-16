using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class ClientBlackList
    {
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } 
        public Client Client { get; set; }
    }
}
