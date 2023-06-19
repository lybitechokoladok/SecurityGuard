﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class MembersGroup
    {
        public int GroupId { get; set; }
        public int GroupNumber { get; set; }
        public List<GroupMember> GroupMembers { get; set; }
    }
}
