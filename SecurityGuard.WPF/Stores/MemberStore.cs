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
        private readonly IMemberRepository _memberRepository;
        private List<GroupMember> _members;

        public IEnumerable<GroupMember> Members => _members;

        public event Action GroupMembersLoaded;
        public MemberStore(IMemberRepository groupRepository)
        {
            _memberRepository = groupRepository;
            _members = new List<GroupMember>();
        }

        public async Task LoadGroupById(int? id) 
        {
            var groupMembers = await _memberRepository.GetAllGroupMemberListAsync(id);

            _members.Clear();
            _members.AddRange(groupMembers);

            GroupMembersLoaded?.Invoke();
        }
    }
}
