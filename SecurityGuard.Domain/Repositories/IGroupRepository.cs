using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<MembersGroup> GetGroupByIdAsync(int groupId);
        Task<IEnumerable<GroupMember>> GetAllGroupMemberListAsync(int groupId);
    }
}
