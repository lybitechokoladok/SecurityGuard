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
    public class GroupStore
    {
        private readonly IGroupRepository _groupRepository;
        private List<MembersGroup> _groups;

        public IEnumerable<MembersGroup> Groups => _groups;

        public event Action GroupLoaded;
        public GroupStore(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _groups = new List<MembersGroup>();
        }

        public async Task LoadGroupByid(int id) 
        {
            MembersGroup group = await _groupRepository.GetGroupByIdAsync(id);

            _groups.Clear();
            _groups.Add(group);

            GroupLoaded?.Invoke();
        }
    }
}
