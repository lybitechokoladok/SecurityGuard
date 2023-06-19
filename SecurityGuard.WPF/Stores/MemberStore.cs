using Microsoft.Identity.Client;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class MemberStore
    {
        private readonly IMemberRepository _groupRepository;
        private List<GroupMember> _groups;

        public IEnumerable<GroupMember> Groups => _groups;

        public event Action GroupLoaded;
        public MemberStore(IMemberRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _groups = new List<GroupMember>();
        }

        public async Task LoadGroupByid(int id) 
        {
            var groupMembers = await _groupRepository.GetAllGroupMemberListAsync(id);

            _groups.Clear();
            _groups.AddRange(groupMembers);

            GroupLoaded?.Invoke();
        }
    }
}
