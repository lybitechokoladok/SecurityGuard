using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class MembersGroup
    {
        public int GroupId { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
    }
}
