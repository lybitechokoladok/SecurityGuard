using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class GroupMemberBlackList
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime DateAdded { get; set; } 
        public GroupMember GroupMember { get; set; }
    }
}
