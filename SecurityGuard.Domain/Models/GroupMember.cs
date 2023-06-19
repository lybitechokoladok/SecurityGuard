using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronomic { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public int GroupId { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Patronomic;
        }
    }
}
