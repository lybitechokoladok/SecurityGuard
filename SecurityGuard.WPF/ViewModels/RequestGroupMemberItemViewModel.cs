using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestGroupMemberItemViewModel : ViewModelBase
    {
		public GroupMember GroupMember { get; private set; }
		public string FullName => GroupMember.ToString();
		public string Phone => GroupMember.Phone;

		public RequestGroupMemberItemViewModel(GroupMember groupMember)
		{
			GroupMember= groupMember;
		}

	}
}
